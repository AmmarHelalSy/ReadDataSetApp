using System.Data;
using OfficeOpenXml;

namespace ReadDataSetApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathtext.Text = openFileDialog.FileName;
                btnProcess.Enabled = true;
            }
            else
            {
                Console.WriteLine("No file selected.");
                btnProcess.Enabled = false;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaxOrder.Text, out int txtMaxOrderValue))
            {
                MessageBox.Show("ادخل قيمة ترتيب الماكس");
                return;
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Define file path and sheet names
            string filePath = filePathtext.Text;
            string dataSheetName = "data";
            string exportSheetName = "ExportSheet";

            // Validate file existence
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            DataTable dataTable = ReadExcelData(filePath, dataSheetName);
            if (dataTable == null)
            {
                Console.WriteLine($"Worksheet '{dataSheetName}' not found or failed to read data.");
                return;
            }

            DataTable resultTable = ProcessData(dataTable);

            dataGridView1.DataSource = resultTable;

            WriteToExcel(filePath, exportSheetName, resultTable);
        }
        static DataTable ReadExcelData(string filePath, string sheetName)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[sheetName];
                    if (worksheet == null) return null;

                    DataTable dataTable = new DataTable();
                    int columns = worksheet.Dimension.Columns;

                    // Read header row
                    for (int col = 1; col <= columns; col++)
                    {
                        dataTable.Columns.Add(worksheet.Cells[1, col].Text, typeof(double));
                    }

                    // Read data rows
                    int rows = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rows; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = 1; col <= columns; col++)
                        {
                            dataRow[col - 1] = worksheet.Cells[row, col].Text;
                        }
                        dataTable.Rows.Add(dataRow);
                    }

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel data: {ex.Message}");
                return null;
            }
        }

        public DataTable ProcessData(DataTable dataTable)
        {
            var groupedByDay = from row in dataTable.AsEnumerable()
                               group row by row.Field<double>("Day") into dayGroup
                               select new
                               {
                                   Day = dayGroup.Key,
                                   Data = dayGroup.CopyToDataTable()
                               };

            DataTable resultTable = CreateDataTableWithSameSchema(dataTable);

            foreach (var group in groupedByDay)
            {
                DataRow resultRow = resultTable.NewRow();
                resultRow["Day"] = group.Day;

                // Process each group
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "Day")
                    {
                        var sortedData = group.Data.AsEnumerable()
                                                   .OrderByDescending(row => row.Field<double>(column.ColumnName))
                                                   .ToList();

                        // Populate the result row based on sorted data
                        if (sortedData.Any())
                        {
                            resultRow[column.ColumnName] = sortedData[int.Parse(txtMaxOrder.Text.ToString()) - 1][column.ColumnName];
                        }
                    }
                }
                resultTable.Rows.Add(resultRow);
            }

            return resultTable;
        }

        static void WriteToExcel(string filePath, string sheetName, DataTable dataTable)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[sheetName] ?? package.Workbook.Worksheets.Add(sheetName);
                    PopulateWorksheetWithDataTable(worksheet, dataTable);
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to Excel: {ex.Message}");
            }
        }

        static void PopulateWorksheetWithDataTable(ExcelWorksheet worksheet, DataTable dataTable)
        {
            // Add column headers
            for (int col = 0; col < dataTable.Columns.Count; col++)
            {
                worksheet.Cells[1, col + 1].Value = dataTable.Columns[col].ColumnName;
            }

            // Add rows
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                }
            }
        }

        static DataTable CreateDataTableWithSameSchema(DataTable originalTable)
        {
            return originalTable.Clone();
        }

        private void txtMaxOrder_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaxOrder.Text, out int value))
            {
                if (value <= 0)
                {
                    MessageBox.Show("Please enter a number greater than 0.");
                    txtMaxOrder.Clear();
                }
            }
            else if (!string.IsNullOrEmpty(txtMaxOrder.Text))
            {
                MessageBox.Show("Please enter a valid number.");
                txtMaxOrder.Clear();
            }
        }

        private void txtMaxOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

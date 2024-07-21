using System.Data;
using System.IO.Packaging;
using System.Linq;
using OfficeOpenXml;

namespace ReadDataSetApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lblAverage.Visible = false;
            lblAvgOfAvgMaxMonth.Visible = false;
            btnProcess.Enabled = false;
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
                if (string.IsNullOrEmpty(filePathtext.Text))
                    btnProcess.Enabled = false;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;

            if (!int.TryParse(txtMaxOrder.Text, out int txtMaxOrderValue))
            {
                MessageBox.Show("ادخل قيمة ترتيب الماكس");
                return;
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


            // Define file path and sheet names
            string filePath = filePathtext.Text;
            string exportSheetName = $"Max{txtMaxOrder.Text}";


            // Validate file existence
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            DataTable dataTable = ReadExcelData(filePath);

            DataTable resultTable = ProcessData(dataTable);

            dataGridView1.DataSource = resultTable;

            WriteToExcel(filePath, exportSheetName, resultTable);

            dataGridView1.Enabled = true;
        }
        public DataTable ReadExcelData(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet Worksheet = package.Workbook.Worksheets[0];

                    var sheetName = Worksheet.Name;

                    var worksheet = package.Workbook.Worksheets[sheetName];
                    if (worksheet == null) return null;

                    DataTable dataTable = new DataTable();
                    int columns = worksheet.Dimension.Columns;

                    dataTable.Columns.Add("اليوم", typeof(double));//worksheet.Cells[1, 1].Text
                    dataTable.Columns.Add("السرعة بالميغا", typeof(double));//worksheet.Cells[1, 9].Text

                    // Read header row
                    //for (int col = 1; col <= columns; col++)
                    //{
                    //    dataTable.Columns.Add(worksheet.Cells[1, col].Text, typeof(double));
                    //}

                    // Read data rows
                    int rows = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rows; row++)
                    {
                        try
                        {

                            DataRow dataRow = dataTable.NewRow();

                            dataRow[0] = worksheet.Cells[row, 1].Text.Split("/")[1];

                            if (chkIsInput.Checked)
                                dataRow[1] = worksheet.Cells[row, 5].Text == "" ? 0.0 : worksheet.Cells[row, 5].Text.Replace("Mbit/s", "").Trim();
                            else
                                dataRow[1] = worksheet.Cells[row, 9].Text == "" ? 0.0 : worksheet.Cells[row, 9].Text.Replace("Mbit/s", "").Trim();

                            dataTable.Rows.Add(dataRow);

                        }
                        catch (IndexOutOfRangeException)
                        {
                            var text = worksheet.Cells[row, 1].Text;
                            if (text.Contains("Sums"))
                                break;

                        }
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
                               group row by row.Field<double>("اليوم") into dayGroup
                               select new
                               {
                                   Day = dayGroup.Key,
                                   Data = dayGroup.CopyToDataTable()
                               };

            DataTable resultTable = CreateDataTableWithSameSchema(dataTable);
            resultTable.Columns.Add($"متوسط {txtAverageMaxDay.Text} ماكس باليوم الواحد", typeof(double));
            double sum = 0.0;
            double sumOfAvg = 0.0;
            int count = 0;

            foreach (var group in groupedByDay)
            {
                DataRow resultRow = resultTable.NewRow();
                resultRow["اليوم"] = group.Day;

                // Process each group
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "اليوم")
                    {
                        var sortedData = group.Data.AsEnumerable()
                                                   .OrderByDescending(row => row.Field<double>(column.ColumnName))
                                                   .ToList();
                        int AverageMaxDay = 5;
                        try
                        {
                            AverageMaxDay = int.Parse(txtAverageMaxDay.Text.ToString());
                        }
                        catch (Exception)
                        {
                        }

                        var avgMaxDay = sortedData.Select(x => Convert.ToDouble(x["السرعة بالميغا"])) // Convert to double
                        .Take(AverageMaxDay)
                        .Average();

                        sumOfAvg += avgMaxDay;

                        resultRow[$"متوسط {txtAverageMaxDay.Text} ماكس باليوم الواحد"] = Math.Round(double.Parse(avgMaxDay.ToString()), 0);

                        // Populate the result row based on sorted data
                        if (sortedData.Any())
                        {
                            resultRow[column.ColumnName] = sortedData[int.Parse(txtMaxOrder.Text.ToString()) - 1][column.ColumnName];
                            sum += double.Parse(sortedData[int.Parse(txtMaxOrder.Text.ToString()) - 1][column.ColumnName].ToString());
                            count++;
                        }
                    }
                }
                resultTable.Rows.Add(resultRow);
            }

            var average = sum / count;
            var averageOfAvg = sumOfAvg / count;

            lblAverage.Text = "متوسط الماكس المطلوب: " + Math.Round(double.Parse(average.ToString()), 0);

            lblAvgOfAvgMaxMonth.Text = "متوسط المتوسطات: " + Math.Round(double.Parse(averageOfAvg.ToString()), 0);

            lblAvgOfAvgMaxMonth.Visible = true;
            lblAverage.Visible = true;
            //DataRow resultRow1 = resultTable.NewRow();
            //resultRow1["اليوم"] = -1;
            //resultRow1["السرعة بالميغا"] = average;
            //resultTable.Rows.Add(resultRow1);


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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtAverageMaxDay_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtAverageMaxDay.Text, out int value))
            {
                if (value <= 0)
                {
                    MessageBox.Show("Please enter a number greater than 0.");
                    txtAverageMaxDay.Clear();
                }
            }
            else if (!string.IsNullOrEmpty(txtAverageMaxDay.Text))
            {
                MessageBox.Show("Please enter a valid number.");
                txtAverageMaxDay.Clear();
            }
        }

        private void txtAverageMaxDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

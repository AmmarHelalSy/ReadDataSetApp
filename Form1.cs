using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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
                InitialDirectory = Properties.Settings.Default.LastFilePath ?? "C:\\",
                Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathtext.Text = openFileDialog.FileName;
                Properties.Settings.Default.LastFilePath = Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.Save();
            }
            else
            {
                if (string.IsNullOrEmpty(filePathtext.Text))
                    btnProcess.Enabled = false;
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
            //DataTable dataTable = ReadExcelData(filePath);
            DataTable dataTable = ReadHeaders(filePath);

            cbxValue1.Items.Clear();
            cbxValue1.Items.AddRange(dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray());

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

            //DataTable dataTable = ReadExcelData(filePath);
            DataTable dataTable = filePath.EndsWith(".csv") ? ReadCsvData(filePath) : ReadExcelData(filePath);


            DataTable resultTable = ProcessData(dataTable);

            dataGridView1.DataSource = resultTable;

            //WriteToExcel(filePath, exportSheetName, resultTable);
            if (!filePath.EndsWith(".csv"))
            {
                WriteToExcel(filePath, exportSheetName, resultTable);
            }

            dataGridView1.Enabled = true;
        }
        public DataTable LoadCsvToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read the column names from the first line of the file
                if (!parser.EndOfData)
                {
                    string[] columns = parser.ReadFields();
                    foreach (string column in columns)
                    {
                        dataTable.Columns.Add(column);
                    }
                }

                // Read the rest of the data
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;
        }

        public DataTable ReadCsvData(string filePath)
        {
            var dataTableCsv = LoadCsvToDataTable(filePath);
            DataTable processedDataTable = new DataTable();
            processedDataTable.Columns.Add("اليوم", typeof(double));
            processedDataTable.Columns.Add("السرعة بالميغا", typeof(double));

            if (dataTableCsv.Rows.Count < 1)
            {
                MessageBox.Show("CSV file is empty or not loaded properly.");
                return processedDataTable;
            }

            string timePattern = @"\b\d{1,2}:\d{2}:\d{2}(?:\s?[APMapm]{2})?\b";
            Regex timeRegex = new Regex(timePattern);

            int timeColumnIndex = 0;
            int valueColumnIndex = int.Parse(lblValue1.Text) - 1;

            for (int index = 2; index < dataTableCsv.Rows.Count; index++)
            {
                try
                {
                    string timeCell = dataTableCsv.Rows[index][timeColumnIndex].ToString();
                    MatchCollection matches = timeRegex.Matches(timeCell);

                    if (matches.Count == 0)
                        continue;

                    string hourString = matches[0].Value.Split(':')[0];
                    if (!int.TryParse(hourString, out int hour))
                        continue;

                    if (rdDay.Checked)
                    {
                        if (matches.Count == 2)
                            hour = int.Parse(matches[0].Value.Split(":")[0].ToString());
                        if (hour > 17 && hour < 24)
                            continue;
                    }
                    else if (rdNight.Checked)
                    {
                        if (matches.Count == 2)
                            hour = int.Parse(matches[0].Value.Split(":")[0].ToString());
                        if (hour < 18)
                            continue;
                    }

                    if (timeCell.Contains("Sums"))
                        break;

                    DataRow dataRow = processedDataTable.NewRow();
                    string[] dateParts = timeCell.Split('/');
                    if (dateParts.Length > 1 && double.TryParse(dateParts[1], out double dayValue))
                    {
                        dataRow[0] = dayValue;
                    }
                    else
                    {
                        dataRow[0] = 0.0;
                    }

                    string rawValue = dataTableCsv.Rows[index][valueColumnIndex].ToString();
                    if (! string.IsNullOrWhiteSpace(rawValue) && ! rawValue.Contains("Mbit"))
                    {
                        MessageBox.Show("اختر العمود الصحيح");
                        dataRow[1] = 0.0;
                    }
                    else
                    {
                        rawValue = rawValue.Replace("Mbit/s", "").Trim();
                        if (double.TryParse(rawValue, out double speedValue))
                        {
                            dataRow[1] = speedValue;
                        }
                        else
                        {
                            dataRow[1] = 0.0;
                        }
                    }

                    processedDataTable.Rows.Add(dataRow);
                }
                catch (IndexOutOfRangeException)
                {
                    // Optionally log the error or handle it if necessary
                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Data format error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}");
                }
            }

            return processedDataTable;
        }


    public DataTable ReadHeaders(string filePath)
        {
            if (filePath.EndsWith(".csv"))
            {
                return ReadCsvHeaders(filePath);
            }
            else if (filePath.EndsWith(".xlsx"))
            {
                return ReadExcelHeaders(filePath);
            }
            else
            {
                throw new NotSupportedException("Unsupported file type.");
            }
        }



        public DataTable ReadExcelHeaders(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    if (worksheet == null) return null;

                    DataTable headersTable = new DataTable();
                    int columns = worksheet.Dimension.Columns;

                    for (int col = 1; col <= columns; col++)
                    {
                        headersTable.Columns.Add(worksheet.Cells[1, col].Text);
                    }

                    return headersTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel data: {ex.Message}");
                return null;
            }
        }

        public DataTable ReadCsvHeaders(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) return null;

                    DataTable headersTable = new DataTable();
                    string[] headers = line.Split(',');

                    foreach (var header in headers)
                    {
                        headersTable.Columns.Add(header);
                    }

                    return headersTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV data: {ex.Message}");
                return null;
            }
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


                    // Read data rows
                    int rows = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rows; row++)
                    {
                        string time = worksheet.Cells[row, 1].Text;
                        string pattern = @"\b\d{1,2}:\d{2}:\d{2}(?:\s?[APMapm]{2})?\b";

                        // Create a Regex object
                        Regex regex = new Regex(pattern);

                        // Find matches
                        MatchCollection matches = regex.Matches(time);
                        var hour = -1;

                        if (rdDay.Checked)
                        {
                            if (matches.Count == 2)
                                hour = int.Parse(matches[0].Value.Split(":")[0].ToString());
                            if (hour > 17 && hour < 24)
                                continue;
                        }
                        else if (rdNight.Checked)
                        {
                            if (matches.Count == 2)
                                hour = int.Parse(matches[0].Value.Split(":")[0].ToString());
                            if (hour < 18)
                                continue;
                        }

                        if (time.Contains("Sums"))
                            break;
                        try
                        {

                            DataRow dataRow = dataTable.NewRow();

                            dataRow[0] = worksheet.Cells[row, 1].Text.Split("/")[1];
                            //if (!values[int.Parse(lblValue1.Text)].Contains("Mbit") && !(values[int.Parse(lblValue1.Text)].Trim() == ""))
                            //    MessageBox.Show("اختر العمود الصحيح");
                            dataRow[1] = worksheet.Cells[row, int.Parse(lblValue1.Text)].Text == "" ? 0.0 : worksheet.Cells[row, int.Parse(lblValue1.Text)].Text.Replace("Mbit/s", "").Trim();



                            dataTable.Rows.Add(dataRow);

                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                        catch(Exception)
                        {
                            MessageBox.Show("حدثت مشكلة");
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
                try
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
                                // if we have only row less than max number
                                var maxIndex = int.Parse(txtMaxOrder.Text.ToString());
                                if (maxIndex > sortedData.Count)
                                {
                                    resultRow[column.ColumnName] = 0;
                                }
                                else
                                {
                                    resultRow[column.ColumnName] = sortedData[maxIndex - 1][column.ColumnName];
                                }
                                var value = double.Parse(sortedData[int.Parse(txtMaxOrder.Text.ToString()) - 1][column.ColumnName].ToString());
                                if(value>0)
                                {
                                    sum += value;
                                    count++;
                                }
                            }
                        }
                    }
                    resultTable.Rows.Add(resultRow);
                }
                catch (Exception)
                {
                }
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

        private void cbxValue1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblValue1.Text = (cbxValue1.SelectedIndex + 1).ToString();

            btnProcess.Enabled = true;
        }
    }
}

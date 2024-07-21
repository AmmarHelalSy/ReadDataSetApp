namespace ReadDataSetApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            button1 = new Button();
            filePathtext = new TextBox();
            btnProcess = new Button();
            txtMaxOrder = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            lblAverage = new Label();
            label3 = new Label();
            txtAverageMaxDay = new TextBox();
            lblAvgOfAvgMaxMonth = new Label();
            chkIsInput = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F);
            button1.Location = new Point(12, 33);
            button1.Name = "button1";
            button1.Size = new Size(111, 37);
            button1.TabIndex = 0;
            button1.Text = "اختر ملف";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // filePathtext
            // 
            filePathtext.Enabled = false;
            filePathtext.Font = new Font("Segoe UI", 15F);
            filePathtext.Location = new Point(153, 31);
            filePathtext.Name = "filePathtext";
            filePathtext.Size = new Size(283, 34);
            filePathtext.TabIndex = 1;
            // 
            // btnProcess
            // 
            btnProcess.Enabled = false;
            btnProcess.Font = new Font("Segoe UI", 25F);
            btnProcess.Location = new Point(127, 181);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(237, 104);
            btnProcess.TabIndex = 2;
            btnProcess.Text = "احسب النتيجة";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // txtMaxOrder
            // 
            txtMaxOrder.Font = new Font("Segoe UI", 15F);
            txtMaxOrder.Location = new Point(153, 76);
            txtMaxOrder.Name = "txtMaxOrder";
            txtMaxOrder.Size = new Size(100, 34);
            txtMaxOrder.TabIndex = 3;
            txtMaxOrder.TextChanged += txtMaxOrder_TextChanged;
            txtMaxOrder.KeyPress += txtMaxOrder_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(498, 37);
            label1.Name = "label1";
            label1.Size = new Size(66, 28);
            label1.TabIndex = 4;
            label1.Text = "المسار";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(440, 79);
            label2.Name = "label2";
            label2.Size = new Size(124, 28);
            label2.TabIndex = 5;
            label2.Text = "ترتيب الماكس";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 423);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Size = new Size(535, 324);
            dataGridView1.TabIndex = 6;
            // 
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.Font = new Font("Segoe UI", 15F);
            lblAverage.Location = new Point(264, 300);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(0, 28);
            lblAverage.TabIndex = 7;
            lblAverage.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F);
            label3.Location = new Point(300, 132);
            label3.Name = "label3";
            label3.Size = new Size(264, 28);
            label3.TabIndex = 9;
            label3.Text = "متوسط كم ماكس باليوم الواحد";
            // 
            // txtAverageMaxDay
            // 
            txtAverageMaxDay.Font = new Font("Segoe UI", 15F);
            txtAverageMaxDay.Location = new Point(153, 129);
            txtAverageMaxDay.Name = "txtAverageMaxDay";
            txtAverageMaxDay.Size = new Size(100, 34);
            txtAverageMaxDay.TabIndex = 8;
            txtAverageMaxDay.TextChanged += txtAverageMaxDay_TextChanged;
            txtAverageMaxDay.KeyPress += txtAverageMaxDay_KeyPress;
            // 
            // lblAvgOfAvgMaxMonth
            // 
            lblAvgOfAvgMaxMonth.AutoSize = true;
            lblAvgOfAvgMaxMonth.Font = new Font("Segoe UI", 15F);
            lblAvgOfAvgMaxMonth.Location = new Point(264, 366);
            lblAvgOfAvgMaxMonth.Name = "lblAvgOfAvgMaxMonth";
            lblAvgOfAvgMaxMonth.Size = new Size(0, 28);
            lblAvgOfAvgMaxMonth.TabIndex = 10;
            lblAvgOfAvgMaxMonth.Visible = false;
            // 
            // chkIsInput
            // 
            chkIsInput.AutoSize = true;
            chkIsInput.Font = new Font("Segoe UI", 15F);
            chkIsInput.Location = new Point(401, 223);
            chkIsInput.Name = "chkIsInput";
            chkIsInput.Size = new Size(163, 32);
            chkIsInput.TabIndex = 12;
            chkIsInput.Text = "هل الملف دخل؟";
            chkIsInput.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 759);
            Controls.Add(chkIsInput);
            Controls.Add(lblAvgOfAvgMaxMonth);
            Controls.Add(label3);
            Controls.Add(txtAverageMaxDay);
            Controls.Add(lblAverage);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMaxOrder);
            Controls.Add(btnProcess);
            Controls.Add(filePathtext);
            Controls.Add(button1);
            Name = "Form1";
            Text = "برمجة المهندس أبو محمود شركة أمية";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox filePathtext;
        private Button btnProcess;
        private TextBox txtMaxOrder;
        private Label label1;
        private Label label2;
        private DataGridView dataGridView1;
        private Label lblAverage;
        private Label label3;
        private TextBox txtAverageMaxDay;
        private Label lblAvgOfAvgMaxMonth;
        private CheckBox chkIsInput;
    }
}

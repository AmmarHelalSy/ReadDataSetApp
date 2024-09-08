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
            cbxValue1 = new ComboBox();
            lblValue1 = new Label();
            label4 = new Label();
            rdDay = new RadioButton();
            rdTotal = new RadioButton();
            rdNight = new RadioButton();
            groupBox1 = new GroupBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F);
            button1.Location = new Point(35, 33);
            button1.Name = "button1";
            button1.Size = new Size(111, 37);
            button1.TabIndex = 0;
            button1.Text = "اختر ملف";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // filePathtext
            // 
            filePathtext.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filePathtext.Enabled = false;
            filePathtext.Font = new Font("Segoe UI", 15F);
            filePathtext.HideSelection = false;
            filePathtext.Location = new Point(151, 31);
            filePathtext.Name = "filePathtext";
            filePathtext.Size = new Size(416, 34);
            filePathtext.TabIndex = 1;
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProcess.AutoEllipsis = true;
            btnProcess.Font = new Font("Segoe UI", 25F);
            btnProcess.Location = new Point(35, 191);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(229, 74);
            btnProcess.TabIndex = 2;
            btnProcess.Text = "احسب النتيجة";
            btnProcess.UseCompatibleTextRendering = true;
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // txtMaxOrder
            // 
            txtMaxOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtMaxOrder.Font = new Font("Segoe UI", 15F);
            txtMaxOrder.Location = new Point(284, 187);
            txtMaxOrder.Name = "txtMaxOrder";
            txtMaxOrder.Size = new Size(100, 34);
            txtMaxOrder.TabIndex = 3;
            txtMaxOrder.TextChanged += txtMaxOrder_TextChanged;
            txtMaxOrder.KeyPress += txtMaxOrder_KeyPress;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(629, 37);
            label1.Name = "label1";
            label1.Size = new Size(66, 28);
            label1.TabIndex = 4;
            label1.Text = "المسار";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(571, 187);
            label2.Name = "label2";
            label2.Size = new Size(124, 28);
            label2.TabIndex = 5;
            label2.Text = "ترتيب الماكس";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(27, 411);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Size = new Size(668, 430);
            dataGridView1.TabIndex = 6;
            // 
            // lblAverage
            // 
            lblAverage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAverage.AutoSize = true;
            lblAverage.Font = new Font("Segoe UI", 15F);
            lblAverage.Location = new Point(395, 290);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(0, 28);
            lblAverage.TabIndex = 7;
            lblAverage.Visible = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F);
            label3.Location = new Point(431, 237);
            label3.Name = "label3";
            label3.Size = new Size(264, 28);
            label3.TabIndex = 9;
            label3.Text = "متوسط كم ماكس باليوم الواحد";
            // 
            // txtAverageMaxDay
            // 
            txtAverageMaxDay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtAverageMaxDay.Font = new Font("Segoe UI", 15F);
            txtAverageMaxDay.Location = new Point(284, 234);
            txtAverageMaxDay.Name = "txtAverageMaxDay";
            txtAverageMaxDay.Size = new Size(100, 34);
            txtAverageMaxDay.TabIndex = 8;
            txtAverageMaxDay.TextChanged += txtAverageMaxDay_TextChanged;
            txtAverageMaxDay.KeyPress += txtAverageMaxDay_KeyPress;
            // 
            // lblAvgOfAvgMaxMonth
            // 
            lblAvgOfAvgMaxMonth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAvgOfAvgMaxMonth.AutoSize = true;
            lblAvgOfAvgMaxMonth.Font = new Font("Segoe UI", 15F);
            lblAvgOfAvgMaxMonth.Location = new Point(395, 356);
            lblAvgOfAvgMaxMonth.Name = "lblAvgOfAvgMaxMonth";
            lblAvgOfAvgMaxMonth.Size = new Size(0, 28);
            lblAvgOfAvgMaxMonth.TabIndex = 10;
            lblAvgOfAvgMaxMonth.Visible = false;
            // 
            // cbxValue1
            // 
            cbxValue1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxValue1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbxValue1.FormattingEnabled = true;
            cbxValue1.Location = new Point(152, 82);
            cbxValue1.Name = "cbxValue1";
            cbxValue1.Size = new Size(415, 38);
            cbxValue1.TabIndex = 13;
            cbxValue1.SelectedIndexChanged += cbxValue1_SelectedIndexChanged;
            // 
            // lblValue1
            // 
            lblValue1.AutoSize = true;
            lblValue1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValue1.Location = new Point(89, 91);
            lblValue1.Name = "lblValue1";
            lblValue1.Size = new Size(0, 25);
            lblValue1.TabIndex = 15;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F);
            label4.Location = new Point(590, 86);
            label4.Name = "label4";
            label4.Size = new Size(105, 28);
            label4.TabIndex = 16;
            label4.Text = "اختر العمود";
            // 
            // rdDay
            // 
            rdDay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdDay.AutoSize = true;
            rdDay.Font = new Font("Segoe UI", 15.75F);
            rdDay.Location = new Point(20, 13);
            rdDay.Name = "rdDay";
            rdDay.Size = new Size(77, 34);
            rdDay.TabIndex = 17;
            rdDay.TabStop = true;
            rdDay.Text = "نهاري";
            rdDay.UseVisualStyleBackColor = true;
            // 
            // rdTotal
            // 
            rdTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdTotal.AutoSize = true;
            rdTotal.Font = new Font("Segoe UI", 15.75F);
            rdTotal.Location = new Point(289, 16);
            rdTotal.Name = "rdTotal";
            rdTotal.Size = new Size(121, 34);
            rdTotal.TabIndex = 18;
            rdTotal.TabStop = true;
            rdTotal.Text = "كامل اليوم";
            rdTotal.UseVisualStyleBackColor = true;
            rdTotal.CheckedChanged += rdTotal_CheckedChanged;
            // 
            // rdNight
            // 
            rdNight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdNight.AutoSize = true;
            rdNight.Font = new Font("Segoe UI", 15.75F);
            rdNight.Location = new Point(174, 16);
            rdNight.Name = "rdNight";
            rdNight.Size = new Size(70, 34);
            rdNight.TabIndex = 19;
            rdNight.TabStop = true;
            rdNight.Text = "ليلي";
            rdNight.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(rdDay);
            groupBox1.Controls.Add(rdTotal);
            groupBox1.Controls.Add(rdNight);
            groupBox1.Location = new Point(151, 126);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(416, 55);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15F);
            label5.Location = new Point(593, 142);
            label5.Name = "label5";
            label5.Size = new Size(102, 28);
            label5.TabIndex = 21;
            label5.Text = "هل السحب";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(718, 853);
            Controls.Add(label5);
            Controls.Add(groupBox1);
            Controls.Add(label4);
            Controls.Add(lblValue1);
            Controls.Add(cbxValue1);
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
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private ComboBox cbxValue1;
        private Label lblValue1;
        private Label label4;
        private RadioButton rdDay;
        private RadioButton rdTotal;
        private RadioButton rdNight;
        private GroupBox groupBox1;
        private Label label5;
    }
}

namespace Pix2Bit
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboHorizontalBits = new System.Windows.Forms.ComboBox();
            this.comboVerticalBits = new System.Windows.Forms.ComboBox();
            this.btnGenerateTable = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnInvertAll = new System.Windows.Forms.Button();
            this.btnAllLow = new System.Windows.Forms.Button();
            this.btnAllHigh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHexValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboConversionMethod = new System.Windows.Forms.ComboBox();
            this.btnByteToMatrix = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboHorizontalBits
            // 
            this.comboHorizontalBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHorizontalBits.FormattingEnabled = true;
            this.comboHorizontalBits.Location = new System.Drawing.Point(117, 48);
            this.comboHorizontalBits.Name = "comboHorizontalBits";
            this.comboHorizontalBits.Size = new System.Drawing.Size(82, 28);
            this.comboHorizontalBits.TabIndex = 0;
            // 
            // comboVerticalBits
            // 
            this.comboVerticalBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVerticalBits.FormattingEnabled = true;
            this.comboVerticalBits.Location = new System.Drawing.Point(205, 48);
            this.comboVerticalBits.Name = "comboVerticalBits";
            this.comboVerticalBits.Size = new System.Drawing.Size(82, 28);
            this.comboVerticalBits.TabIndex = 1;
            // 
            // btnGenerateTable
            // 
            this.btnGenerateTable.Location = new System.Drawing.Point(293, 48);
            this.btnGenerateTable.Name = "btnGenerateTable";
            this.btnGenerateTable.Size = new System.Drawing.Size(82, 36);
            this.btnGenerateTable.TabIndex = 2;
            this.btnGenerateTable.Text = "◀️  Draw";
            this.btnGenerateTable.UseVisualStyleBackColor = true;
            this.btnGenerateTable.Click += new System.EventHandler(this.BtnGenerateTable_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 683F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(637, 673);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnInvertAll);
            this.splitContainer1.Panel2.Controls.Add(this.btnAllLow);
            this.splitContainer1.Panel2.Controls.Add(this.btnAllHigh);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtHexValue);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.comboHorizontalBits);
            this.splitContainer1.Panel2.Controls.Add(this.btnGenerateTable);
            this.splitContainer1.Panel2.Controls.Add(this.comboVerticalBits);
            this.splitContainer1.Panel2.Controls.Add(this.comboConversionMethod);
            this.splitContainer1.Panel2.Controls.Add(this.btnByteToMatrix);
            this.splitContainer1.Size = new System.Drawing.Size(1060, 673);
            this.splitContainer1.SplitterDistance = 637;
            this.splitContainer1.TabIndex = 4;
            // 
            // btnInvertAll
            // 
            this.btnInvertAll.AutoSize = true;
            this.btnInvertAll.Location = new System.Drawing.Point(272, 323);
            this.btnInvertAll.Name = "btnInvertAll";
            this.btnInvertAll.Size = new System.Drawing.Size(124, 30);
            this.btnInvertAll.TabIndex = 10;
            this.btnInvertAll.Text = "🔀 Invert all";
            this.btnInvertAll.UseVisualStyleBackColor = true;
            this.btnInvertAll.Click += new System.EventHandler(this.BtnInvertAll_Click);
            // 
            // btnAllLow
            // 
            this.btnAllLow.AutoSize = true;
            this.btnAllLow.Location = new System.Drawing.Point(144, 323);
            this.btnAllLow.Name = "btnAllLow";
            this.btnAllLow.Size = new System.Drawing.Size(124, 30);
            this.btnAllLow.TabIndex = 9;
            this.btnAllLow.Text = "⬇️ All low";
            this.btnAllLow.UseVisualStyleBackColor = true;
            this.btnAllLow.Click += new System.EventHandler(this.BtnLowAll_Click);
            // 
            // btnAllHigh
            // 
            this.btnAllHigh.AutoSize = true;
            this.btnAllHigh.Location = new System.Drawing.Point(16, 323);
            this.btnAllHigh.Name = "btnAllHigh";
            this.btnAllHigh.Size = new System.Drawing.Size(124, 30);
            this.btnAllHigh.TabIndex = 8;
            this.btnAllHigh.Text = "⬆️ All high";
            this.btnAllHigh.UseVisualStyleBackColor = true;
            this.btnAllHigh.Click += new System.EventHandler(this.BtnHighAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mode";
            // 
            // txtHexValue
            // 
            this.txtHexValue.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHexValue.Location = new System.Drawing.Point(16, 90);
            this.txtHexValue.Multiline = true;
            this.txtHexValue.Name = "txtHexValue";
            this.txtHexValue.Size = new System.Drawing.Size(380, 191);
            this.txtHexValue.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cols,Rows";
            // 
            // comboConversionMethod
            // 
            this.comboConversionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConversionMethod.FormattingEnabled = true;
            this.comboConversionMethod.Items.AddRange(new object[] {
            "RAW conversion",
            "ks0262 conversion"});
            this.comboConversionMethod.Location = new System.Drawing.Point(117, 12);
            this.comboConversionMethod.Name = "comboConversionMethod";
            this.comboConversionMethod.Size = new System.Drawing.Size(258, 28);
            this.comboConversionMethod.TabIndex = 6;
            this.comboConversionMethod.SelectedIndexChanged += new System.EventHandler(this.ComboConversionMethod_SelectedIndexChanged);
            this.comboConversionMethod.SelectedValueChanged += new System.EventHandler(this.ComboConversionMethod_SelectedIndexChanged);
            // 
            // btnByteToMatrix
            // 
            this.btnByteToMatrix.AutoSize = true;
            this.btnByteToMatrix.Location = new System.Drawing.Point(16, 287);
            this.btnByteToMatrix.Name = "btnByteToMatrix";
            this.btnByteToMatrix.Size = new System.Drawing.Size(380, 30);
            this.btnByteToMatrix.TabIndex = 5;
            this.btnByteToMatrix.Text = "◀️ Byte array to Matrix";
            this.btnByteToMatrix.UseVisualStyleBackColor = true;
            this.btnByteToMatrix.Click += new System.EventHandler(this.BtnByteToMatrix_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 673);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Pix2Bit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboHorizontalBits;
        private System.Windows.Forms.ComboBox comboVerticalBits;
        private System.Windows.Forms.Button btnGenerateTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHexValue;
        private System.Windows.Forms.Button btnByteToMatrix;
        private System.Windows.Forms.ComboBox comboConversionMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInvertAll;
        private System.Windows.Forms.Button btnAllLow;
        private System.Windows.Forms.Button btnAllHigh;
    }
}

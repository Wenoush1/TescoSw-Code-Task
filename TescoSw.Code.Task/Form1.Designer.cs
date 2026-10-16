namespace TescoSw.Code.Task
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.dgvCars = new System.Windows.Forms.DataGridView();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colExclVat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInclVat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(160, 30);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Select XML file...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(185, 19);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(0, 15);
            this.lblFilePath.TabIndex = 1;
            // 
            // dgvCars
            // 
            this.dgvCars.AllowUserToAddRows = false;
            this.dgvCars.AllowUserToDeleteRows = false;
            this.dgvCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCars.Location = new System.Drawing.Point(12, 50);
            this.dgvCars.Name = "dgvCars";
            this.dgvCars.ReadOnly = true;
            this.dgvCars.RowHeadersVisible = false;
            this.dgvCars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCars.Size = new System.Drawing.Size(540, 438);
            this.dgvCars.TabIndex = 2;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExclVat,
            this.colInclVat});
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.Location = new System.Drawing.Point(568, 50);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.Size = new System.Drawing.Size(320, 438);
            this.dgvResults.TabIndex = 3;
            // 
            // colExclVat
            // 
            this.colExclVat.HeaderText = "Model name\r\nPrice excl. VAT";
            this.colExclVat.Name = "colExclVat";
            this.colExclVat.ReadOnly = true;
            this.colExclVat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colInclVat
            // 
            this.colInclVat.HeaderText = "\r\nPrice incl. VAT";
            this.colInclVat.Name = "colInclVat";
            this.colInclVat.ReadOnly = true;
            this.colInclVat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.dgvCars);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Sold Cars";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.DataGridView dgvCars;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExclVat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInclVat;
    }
}

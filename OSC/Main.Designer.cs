namespace OSC
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnGetFile = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.ofdCsv = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFile.Location = new System.Drawing.Point(13, 13);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(477, 22);
            this.txtFile.TabIndex = 0;
            // 
            // btnGetFile
            // 
            this.btnGetFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetFile.Location = new System.Drawing.Point(496, 13);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(91, 23);
            this.btnGetFile.TabIndex = 1;
            this.btnGetFile.Text = "Buscar";
            this.btnGetFile.UseVisualStyleBackColor = true;
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
            // 
            // btnImport
            // 
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Location = new System.Drawing.Point(13, 41);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(284, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Importar";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnManual
            // 
            this.btnManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManual.Location = new System.Drawing.Point(303, 41);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(284, 23);
            this.btnManual.TabIndex = 3;
            this.btnManual.Text = "Inserir manualmente";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // ofdCsv
            // 
            this.ofdCsv.Filter = "Excel File|*.csv";
            this.ofdCsv.ShowReadOnly = true;
            this.ofdCsv.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdCsv_FileOk);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(596, 74);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnGetFile);
            this.Controls.Add(this.txtFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(612, 113);
            this.MinimumSize = new System.Drawing.Size(612, 113);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Classes.Helpers.FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnGetFile;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.OpenFileDialog ofdCsv;
    }
}
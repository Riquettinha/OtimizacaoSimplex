using OSC.Classes;

namespace OSC
{
    partial class Function
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Function));
            this.rdMaxValue = new System.Windows.Forms.RadioButton();
            this.rdMinValue = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // rdMaxValue
            // 
            this.rdMaxValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdMaxValue.AutoSize = true;
            this.rdMaxValue.Location = new System.Drawing.Point(120, 4);
            this.rdMaxValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdMaxValue.Name = "rdMaxValue";
            this.rdMaxValue.Size = new System.Drawing.Size(119, 20);
            this.rdMaxValue.TabIndex = 0;
            this.rdMaxValue.TabStop = true;
            this.rdMaxValue.Text = "Maximizar Valor";
            this.rdMaxValue.UseVisualStyleBackColor = true;
            this.rdMaxValue.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rdMinValue
            // 
            this.rdMinValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdMinValue.AutoSize = true;
            this.rdMinValue.Location = new System.Drawing.Point(5, 4);
            this.rdMinValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdMinValue.Name = "rdMinValue";
            this.rdMinValue.Size = new System.Drawing.Size(115, 20);
            this.rdMinValue.TabIndex = 1;
            this.rdMinValue.TabStop = true;
            this.rdMinValue.Text = "Minimizar Valor";
            this.rdMinValue.UseVisualStyleBackColor = true;
            this.rdMinValue.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Enabled = false;
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(50, 80);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(203, 32);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Próximo Passo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rdMinValue);
            this.panel1.Controls.Add(this.rdMaxValue);
            this.panel1.Location = new System.Drawing.Point(14, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 27);
            this.panel1.TabIndex = 7;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Image = global::OSC.Properties.Resources.back;
            this.btnBack.Location = new System.Drawing.Point(12, 80);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnBack.TabIndex = 8;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Function
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(264, 122);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNext);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Function";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Função";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Helpers.FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdMaxValue;
        private System.Windows.Forms.RadioButton rdMinValue;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnBack;
    }
}
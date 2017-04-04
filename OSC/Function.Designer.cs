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
            this.maxValue = new System.Windows.Forms.RadioButton();
            this.minValue = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // maxValue
            // 
            this.maxValue.AutoSize = true;
            this.maxValue.Location = new System.Drawing.Point(12, 12);
            this.maxValue.Name = "maxValue";
            this.maxValue.Size = new System.Drawing.Size(98, 17);
            this.maxValue.TabIndex = 0;
            this.maxValue.TabStop = true;
            this.maxValue.Text = "Maximizar Valor";
            this.maxValue.UseVisualStyleBackColor = true;
            // 
            // minValue
            // 
            this.minValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minValue.AutoSize = true;
            this.minValue.Location = new System.Drawing.Point(131, 12);
            this.minValue.Name = "minValue";
            this.minValue.Size = new System.Drawing.Size(95, 17);
            this.minValue.TabIndex = 1;
            this.minValue.TabStop = true;
            this.minValue.Text = "Minimizar Valor";
            this.minValue.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(12, 65);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(214, 38);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Próximo Passo";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // Function
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(239, 115);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.minValue);
            this.Controls.Add(this.maxValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Function";
            this.Text = "Função";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Function_FormClosing);
            this.Load += new System.EventHandler(this.Function_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton maxValue;
        private System.Windows.Forms.RadioButton minValue;
        private System.Windows.Forms.Button btnNext;
    }
}
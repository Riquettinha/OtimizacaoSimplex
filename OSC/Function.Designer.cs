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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // maxValue
            // 
            this.maxValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxValue.AutoSize = true;
            this.maxValue.Location = new System.Drawing.Point(125, 4);
            this.maxValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maxValue.Name = "maxValue";
            this.maxValue.Size = new System.Drawing.Size(119, 20);
            this.maxValue.TabIndex = 0;
            this.maxValue.TabStop = true;
            this.maxValue.Text = "Maximizar Valor";
            this.maxValue.UseVisualStyleBackColor = true;
            // 
            // minValue
            // 
            this.minValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minValue.AutoSize = true;
            this.minValue.Location = new System.Drawing.Point(4, 4);
            this.minValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.minValue.Name = "minValue";
            this.minValue.Size = new System.Drawing.Size(115, 20);
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
            this.btnNext.Location = new System.Drawing.Point(14, 74);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(243, 47);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Próximo Passo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.minValue);
            this.panel1.Controls.Add(this.maxValue);
            this.panel1.Location = new System.Drawing.Point(14, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 27);
            this.panel1.TabIndex = 7;
            // 
            // Function
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(272, 135);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNext);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Function";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Função";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Function_FormClosing);
            this.Load += new System.EventHandler(this.Function_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton maxValue;
        private System.Windows.Forms.RadioButton minValue;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel panel1;
    }
}
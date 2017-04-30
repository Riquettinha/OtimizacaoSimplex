namespace OSC
{
    partial class SimplexStep0
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplexStep0));
            this.txtSimplex = new System.Windows.Forms.TextBox();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSimplex
            // 
            this.txtSimplex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSimplex.Location = new System.Drawing.Point(12, 12);
            this.txtSimplex.Multiline = true;
            this.txtSimplex.Name = "txtSimplex";
            this.txtSimplex.ReadOnly = true;
            this.txtSimplex.Size = new System.Drawing.Size(218, 226);
            this.txtSimplex.TabIndex = 0;
            // 
            // btnNextStep
            // 
            this.btnNextStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextStep.FlatAppearance.BorderSize = 2;
            this.btnNextStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextStep.Location = new System.Drawing.Point(12, 245);
            this.btnNextStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(217, 32);
            this.btnNextStep.TabIndex = 2;
            this.btnNextStep.Text = "Próximo Passo";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // SimplexStep0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(242, 286);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.txtSimplex);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(258, 325);
            this.Name = "SimplexStep0";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Preparação para o Simplex";
            this.Load += new System.EventHandler(this.SimplexStep0_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSimplex;
        private System.Windows.Forms.Button btnNextStep;
    }
}
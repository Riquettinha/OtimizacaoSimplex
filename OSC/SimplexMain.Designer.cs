using System;
using OSC.Classes;

namespace OSC
{
    partial class SimplexMain
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
            this.btnStepByStep = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnSimplesExecution = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStepByStep
            // 
            this.btnStepByStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStepByStep.FlatAppearance.BorderSize = 2;
            this.btnStepByStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepByStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStepByStep.Location = new System.Drawing.Point(49, 14);
            this.btnStepByStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStepByStep.Name = "btnStepByStep";
            this.btnStepByStep.Size = new System.Drawing.Size(188, 32);
            this.btnStepByStep.TabIndex = 6;
            this.btnStepByStep.Text = "Passo a Passo";
            this.btnStepByStep.UseVisualStyleBackColor = true;
            this.btnStepByStep.Click += new System.EventHandler(this.btnSetpByStep_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Image = global::OSC.Properties.Resources.back;
            this.btnBack.Location = new System.Drawing.Point(12, 14);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnBack.TabIndex = 8;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnSimplesExecution
            // 
            this.btnSimplesExecution.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSimplesExecution.FlatAppearance.BorderSize = 2;
            this.btnSimplesExecution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimplesExecution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimplesExecution.Location = new System.Drawing.Point(243, 14);
            this.btnSimplesExecution.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSimplesExecution.Name = "btnSimplesExecution";
            this.btnSimplesExecution.Size = new System.Drawing.Size(188, 32);
            this.btnSimplesExecution.TabIndex = 9;
            this.btnSimplesExecution.Text = "Execução Simples";
            this.btnSimplesExecution.UseVisualStyleBackColor = true;
            this.btnSimplesExecution.Click += new System.EventHandler(this.btnSimplesExecution_Click);
            // 
            // SimplexMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 56);
            this.Controls.Add(this.btnSimplesExecution);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnStepByStep);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(459, 95);
            this.MinimumSize = new System.Drawing.Size(459, 95);
            this.Name = "SimplexMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Método Simplex";
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStepByStep;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.Button btnSimplesExecution;
    }
}
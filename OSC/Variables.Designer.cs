﻿namespace OSC
{
    partial class Variables
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
            this.variableList = new System.Windows.Forms.ListBox();
            this.txtVariableValue = new System.Windows.Forms.TextBox();
            this.btnRemoveVariable = new System.Windows.Forms.Button();
            this.txtVariableDesc = new System.Windows.Forms.TextBox();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // variableList
            // 
            this.variableList.FormattingEnabled = true;
            this.variableList.ItemHeight = 16;
            this.variableList.Location = new System.Drawing.Point(14, 15);
            this.variableList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.variableList.Name = "variableList";
            this.variableList.Size = new System.Drawing.Size(206, 116);
            this.variableList.TabIndex = 0;
            // 
            // txtVariableValue
            // 
            this.txtVariableValue.Location = new System.Drawing.Point(82, 175);
            this.txtVariableValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVariableValue.Name = "txtVariableValue";
            this.txtVariableValue.Size = new System.Drawing.Size(138, 22);
            this.txtVariableValue.TabIndex = 2;
            // 
            // btnRemoveVariable
            // 
            this.btnRemoveVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveVariable.Location = new System.Drawing.Point(14, 139);
            this.btnRemoveVariable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveVariable.Name = "btnRemoveVariable";
            this.btnRemoveVariable.Size = new System.Drawing.Size(99, 28);
            this.btnRemoveVariable.TabIndex = 1;
            this.btnRemoveVariable.Text = "Remover";
            this.btnRemoveVariable.UseVisualStyleBackColor = true;
            this.btnRemoveVariable.Click += new System.EventHandler(this.btnRemoveVariable_Click);
            // 
            // txtVariableDesc
            // 
            this.txtVariableDesc.Location = new System.Drawing.Point(82, 207);
            this.txtVariableDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVariableDesc.Name = "txtVariableDesc";
            this.txtVariableDesc.Size = new System.Drawing.Size(138, 22);
            this.txtVariableDesc.TabIndex = 3;
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVariable.Location = new System.Drawing.Point(14, 239);
            this.btnAddVariable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(206, 28);
            this.btnAddVariable.TabIndex = 4;
            this.btnAddVariable.Text = "Adicionar Variável";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Valor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(14, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Descrição:";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(14, 274);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 47);
            this.button1.TabIndex = 5;
            this.button1.Text = "Próximo Passo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNext);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(121, 139);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 28);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Variables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(239, 331);
            this.Controls.Add(this.txtVariableValue);
            this.Controls.Add(this.txtVariableDesc);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddVariable);
            this.Controls.Add(this.btnRemoveVariable);
            this.Controls.Add(this.variableList);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Variables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Variáveis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox variableList;
        private System.Windows.Forms.TextBox txtVariableValue;
        private System.Windows.Forms.Button btnRemoveVariable;
        private System.Windows.Forms.TextBox txtVariableDesc;
        private System.Windows.Forms.Button btnAddVariable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEdit;
    }
}


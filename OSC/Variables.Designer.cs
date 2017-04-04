namespace OSC
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
            this.variableList.Location = new System.Drawing.Point(12, 12);
            this.variableList.Name = "variableList";
            this.variableList.Size = new System.Drawing.Size(177, 95);
            this.variableList.TabIndex = 0;
            // 
            // txtVariableValue
            // 
            this.txtVariableValue.Location = new System.Drawing.Point(70, 142);
            this.txtVariableValue.Name = "txtVariableValue";
            this.txtVariableValue.Size = new System.Drawing.Size(119, 20);
            this.txtVariableValue.TabIndex = 2;
            // 
            // btnRemoveVariable
            // 
            this.btnRemoveVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveVariable.Location = new System.Drawing.Point(12, 113);
            this.btnRemoveVariable.Name = "btnRemoveVariable";
            this.btnRemoveVariable.Size = new System.Drawing.Size(85, 23);
            this.btnRemoveVariable.TabIndex = 1;
            this.btnRemoveVariable.Text = "Remover";
            this.btnRemoveVariable.UseVisualStyleBackColor = true;
            this.btnRemoveVariable.Click += new System.EventHandler(this.btnRemoveVariable_Click);
            // 
            // txtVariableDesc
            // 
            this.txtVariableDesc.Location = new System.Drawing.Point(70, 168);
            this.txtVariableDesc.Name = "txtVariableDesc";
            this.txtVariableDesc.Size = new System.Drawing.Size(119, 20);
            this.txtVariableDesc.TabIndex = 3;
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVariable.Location = new System.Drawing.Point(12, 194);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(177, 23);
            this.btnAddVariable.TabIndex = 4;
            this.btnAddVariable.Text = "Adicionar Variável";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Valor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Descrição:";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Próximo Passo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNext);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(104, 113);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(85, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Variables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(205, 269);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddVariable);
            this.Controls.Add(this.txtVariableDesc);
            this.Controls.Add(this.btnRemoveVariable);
            this.Controls.Add(this.txtVariableValue);
            this.Controls.Add(this.variableList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Variables";
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


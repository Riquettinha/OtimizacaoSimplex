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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Variables));
            this.variableList = new System.Windows.Forms.ListBox();
            this.txtVariableValue = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtVariableDesc = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
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
            this.variableList.SelectedIndexChanged += new System.EventHandler(this.variableList_SelectedIndexChanged);
            // 
            // txtVariableValue
            // 
            this.txtVariableValue.Location = new System.Drawing.Point(82, 175);
            this.txtVariableValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVariableValue.Name = "txtVariableValue";
            this.txtVariableValue.Size = new System.Drawing.Size(138, 22);
            this.txtVariableValue.TabIndex = 3;
            this.txtVariableValue.TextChanged += new System.EventHandler(this.txtVariableData_TextChanged);
            this.txtVariableValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVariableValue_KeyPress);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(14, 139);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(99, 28);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remover";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemoveVariable_Click);
            // 
            // txtVariableDesc
            // 
            this.txtVariableDesc.ForeColor = System.Drawing.Color.DimGray;
            this.txtVariableDesc.Location = new System.Drawing.Point(82, 207);
            this.txtVariableDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVariableDesc.Name = "txtVariableDesc";
            this.txtVariableDesc.Size = new System.Drawing.Size(138, 22);
            this.txtVariableDesc.TabIndex = 4;
            this.txtVariableDesc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtVariableDesc_MouseClick);
            this.txtVariableDesc.TextChanged += new System.EventHandler(this.txtVariableData_TextChanged);
            this.txtVariableDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVariableDesc_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(14, 239);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(206, 28);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Adicionar Variável";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAddVariable_Click);
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
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(14, 274);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(206, 32);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Próximo Passo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(121, 139);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 28);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Variables
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(239, 316);
            this.Controls.Add(this.txtVariableValue);
            this.Controls.Add(this.txtVariableDesc);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.variableList);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(255, 355);
            this.MinimumSize = new System.Drawing.Size(255, 355);
            this.Name = "Variables";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Variáveis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Variables_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox variableList;
        private System.Windows.Forms.TextBox txtVariableValue;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtVariableDesc;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnEdit;
    }
}


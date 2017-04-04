using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSC
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            if(variableList.Items.Count != 0)
                variableList.Items.RemoveAt(variableList.SelectedIndex);
            else
                MessageBox.Show("Lista vazia!");
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            var value = txtVariableValue.Text.Replace("-", "");
            var desc = txtVariableDesc.Text.Replace("-", "");
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(desc))
            {
                bool thereIs = false;
                foreach (object item in variableList.Items)
                    if(item.ToString().Split('-')[0].Trim() == value)
                    {
                        thereIs = true;
                        break;
                    }
                if (thereIs)
                {
                    MessageBox.Show("Valor já adicionado.");
                    txtVariableValue.Text = "";
                }
                else
                    variableList.Items.Add(value + " - " + desc);
            }
            else
            {
                MessageBox.Show("Preencha o valor e descrição.");
            }
        }

        private void btnNext(object sender, EventArgs e)
        {
            if (variableList.Items.Count < 2)
            {
                MessageBox.Show("Número de variáveis insuficientes!");
            }
            else
            {
                var list = new List<string>();
                foreach (var item in variableList.Items)
                    list.Add(item.ToString().Split('-')[0].Trim());

                var function = new Function(list);
                function.ShowDialog();
                this.Visible = false;
            }
        }
    }
}

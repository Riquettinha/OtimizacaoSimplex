using System;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable AccessToModifiedClosure
namespace OSC.Classes
{
    static class Helpers
    {
        public static void FormClosing(object sender, FormClosingEventArgs e)
        {
            // FormClosing generic
            if (FinishProcess())
                Environment.Exit(0);
            else
                e.Cancel = true;
        }

        /// <summary>
        /// Remove all texts from textboxs, combobox selections, and checkbox or radiobutton checks.
        /// </summary>
        /// <param name="form">Owner form.</param>
        public static void ClearFormValues(Form form)
        {
            var formControls = form.Controls;
            for (int i = 0; i < formControls.Count; i++)
            {
                // Check component type and clean if possible.
                if (formControls[i].GetType() == typeof(TextBox))
                {
                    form.Invoke(new Action(delegate
                    {
                        ((TextBox)form.Controls[i]).Text = "";
                    }));
                }
                else if (formControls[i].GetType() == typeof(ComboBox))
                {
                    form.Invoke(new Action(delegate
                    {
                        ((ComboBox)form.Controls[i]).SelectedIndex = -1;
                    }));
                }
                else if (formControls[i].GetType() == typeof(CheckBox))
                {
                    form.Invoke(new Action(delegate
                    {
                        ((CheckBox)form.Controls[i]).Checked = false;
                    }));
                }
                else if (formControls[i].GetType() == typeof(RadioButton))
                {
                    form.Invoke(new Action(delegate
                    {
                        ((RadioButton)form.Controls[i]).Checked = false;
                    }));
                }
            }
        }

        /// <summary>
        /// Checks if string contains invalid characters.
        /// </summary>
        public static bool CheckForInvalidChars(string value)
        {
            return value.Any(t => !char.IsLetter(t) && !char.IsNumber(t) && t != ' ');
        }

        /// <summary>
        /// Show message box with warning icon.
        /// </summary>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, @"Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Check if string contains black space.
        /// </summary>
        public static bool CheckForSpace(string text)
        {
            return text.Contains(" ");
        }

        /// <summary>
        /// Convert the value to a decimal value.
        /// </summary>
        public static decimal ConvertToDecimal(this string text)
        {
            return Convert.ToDecimal(text.Replace('.', ',').Trim());
        }

        /// <summary>
        /// Ask for the user if he/she want to close all the process
        /// </summary>
        /// <returns>User decision</returns>
        public static bool FinishProcess()
        {
            var userDecision =
                MessageBox.Show(@"O cálculo ainda não foi finalizado, deseja fechar o programa mesmo assim?",
                    @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return userDecision != DialogResult.No;
        }

        /// <summary>
        /// Ask for the user if he/she wnat to back a form
        /// </summary>
        /// <returns></returns>
        public static bool BackForm()
        {
            var userDecision =
                MessageBox.Show(@"Se você voltar para tela anterior perderá todas as alterações "
                + @"realizadas nesta tela, deseja voltar para tela anteiror mesmo assim?",
                    @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return userDecision == DialogResult.Yes;
        }
        
        /// <summary>
        /// Try to convert value to decimal
        /// </summary>
        /// <param name="value"></param>
        public static bool CheckIfIsAValidDecimal(string value)
        {
            // Tenta converter para decimal, caso seja possível é um decimal válido
            try
            {
                value.ConvertToDecimal();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if all TextBoxes are filles
        /// </summary>
        /// <param name="controls"></param>
        public static bool CheckIfAllTextAreFilled(Control.ControlCollection controls)
        {
            // Verifica se todos os controles do tipo textbox estão preenchidos
            foreach (Control control in controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    if (((TextBox)control).Text.Length == 0)
                        return false;
                }
            }
            return true;
        }
    }
}

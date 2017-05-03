using OSC.Problem_Classes;
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
                        (form.Controls[i] as TextBox).Text = "";
                    }));
                }
                else if (formControls[i].GetType() == typeof(ComboBox))
                {
                    var comboBox = form.Controls[i] as ComboBox;
                    if (comboBox.DataSource == null)
                    {
                        form.Invoke(new Action(delegate
                        {
                            comboBox.SelectedIndex = -1;
                        }));
                    }
                }
                else if (formControls[i].GetType() == typeof(CheckBox))
                {
                    form.Invoke(new Action(delegate
                    {
                        (form.Controls[i] as CheckBox).Checked = false;
                    }));
                }
                else if (formControls[i].GetType() == typeof(RadioButton))
                {
                    form.Invoke(new Action(delegate
                    {
                        (form.Controls[i] as RadioButton).Checked = false;
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
        /// Show message box with warning icon and "Atenção" header.
        /// </summary>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, @"Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Show message box with exclamation icon and "Sucesso" header.
        /// </summary>
        public static void ShowSucessMessage(string message)
        {
            MessageBox.Show(message, @"Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        /// Check if is a negative number
        /// </summary>
        public static bool IsNegative(this decimal number)
        {
            return number < 0;
        }


        /// <summary>
        /// Get the number string
        /// </summary>
        public static string GetString(this decimal number)
        {
            if (number.IsNegative())
                return " - " + number * -1;
            return " + " + number;
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

        /// <summary>
        /// Return the text that reference the RestrictionType
        /// </summary>
        public static string GetRestrictionTypeString(RestrictionType restType)
        {
            switch (restType)
            {
                case RestrictionType.EqualTo:
                    return "=";
                case RestrictionType.LessThan:
                    return "<=";
                case RestrictionType.MoreThan:
                    return ">=";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Superscript the numbers in a text.
        /// </summary>
        public static string SubscriptNumber(this string textNumber)
        {
            string finalString = "";
            foreach (var letter in textNumber)
            {
                switch (letter)
                {
                    case '0':
                        finalString += "₀";
                        break;
                    case '1':
                        finalString += "₁";
                        break;
                    case '2':
                        finalString += "₂";
                        break;
                    case '3':
                        finalString += "₃";
                        break;
                    case '4':
                        finalString += "₄";
                        break;
                    case '5':
                        finalString += "₅";
                        break;
                    case '6':
                        finalString += "₆";
                        break;
                    case '7':
                        finalString += "₇";
                        break;
                    case '8':
                        finalString += "₈";
                        break;
                    case '9':
                        finalString += "₉";
                        break;
                    default:
                        finalString += letter;
                        break;
                }
            }
            return finalString;
        }

        /// <summary>
        /// Get excel column name from number
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}

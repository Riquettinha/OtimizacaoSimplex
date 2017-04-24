using System;
using System.Windows.Forms;

namespace OSC
{
    static class Helpers
    {
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
            for (int i = 0; i < value.Length; i++)
            {
                if(!char.IsLetter(value[i]) && !char.IsNumber(value[i]) && value[i] != ' ')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Show message box with warning icon.
        /// </summary>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

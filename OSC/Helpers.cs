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

        public static bool CheckForInvalidChars(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if(!Char.IsLetter(value[i]) && !Char.IsNumber(value[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

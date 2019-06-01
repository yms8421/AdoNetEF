using System.Collections.Generic;
using System.Windows.Forms;

namespace BilgeAdam.Data.ADONet.Extensions
{
    static class ControlExtensions
    {
        public static void BindComboBox<T>(this ComboBox comboBox, 
                                           List<T> data, 
                                           string keyColumn, 
                                           string textColumn)
        {
            comboBox.DataSource = data;
            comboBox.ValueMember = keyColumn;
            comboBox.DisplayMember = textColumn;
        }
    }
}

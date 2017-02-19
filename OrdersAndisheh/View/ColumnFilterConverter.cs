using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace OrdersAndisheh.View
{
    public class CloumnFilterConverter : IMultiValueConverter
    {
        #region IValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Length != 2)
                return DependencyProperty.UnsetValue;
            BinaryOperator oper1 = new BinaryOperator("Value", (string)values[0], BinaryOperatorType.Like);
            BinaryOperator oper2 = new BinaryOperator("Value", (string)values[1], BinaryOperatorType.Like);
            BinaryOperator oper3 = new BinaryOperator("Value", (string)values[2], BinaryOperatorType.Like);
            BinaryOperator oper4 = new BinaryOperator("Value", (string)values[3], BinaryOperatorType.Like);
            GroupOperator group = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] { oper1, oper2, oper3, oper4 });
            return group;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        
        #endregion




        
    }
}

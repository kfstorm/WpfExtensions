using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// Provides a way to convert between <see cref="bool"/> type and <see cref="Visibility"/> type
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        /// <summary>
        /// Converts the <see cref="bool"/> value to the <see cref="Visibility"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter to specify what value of <see cref="Visibility"/> to return when value is <c>false</c>.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="Visibility"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value ^ Reverse) return Visibility.Visible;
            return parameter ?? Visibility.Hidden;
        }

        /// <summary>
        /// Converts back to the <see cref="bool"/> value by the <see cref="Visibility"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Visibility"/> value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="bool"/> value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Collapsed:
                case Visibility.Hidden:
                    return Reverse;
                case Visibility.Visible:
                    return !Reverse;
                default:
                    return !Reverse;
            }
        }
    }
}

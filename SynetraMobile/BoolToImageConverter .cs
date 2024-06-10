using MauiIcons.Core;
using MauiIcons.Cupertino;
using MauiIcons.Fluent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SynetraMobile
{
    class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool Statut)
            {
                return Statut ? new MauiIcon().Icon(CupertinoIcons.Desktopcomputer).IconColor(Colors.Green).IconSize(60) : new MauiIcon().Icon(CupertinoIcons.Desktopcomputer).IconColor(Colors.Red).IconSize(60);
            }
            return new MauiIcon().Icon(CupertinoIcons.Question).IconColor(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

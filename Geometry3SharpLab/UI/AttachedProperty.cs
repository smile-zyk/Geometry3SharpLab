using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Geometry3SharpLab.UI
{
    public class AttachedProperty
    {
        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(AttachedProperty), new PropertyMetadata(false));

        public static bool GetIsFoucs(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFoucsProperty);
        }

        public static void SetIsFoucs(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFoucsProperty, value);
        }

        public static readonly DependencyProperty IsFoucsProperty =
            DependencyProperty.RegisterAttached("IsFoucs", typeof(bool), typeof(AttachedProperty), new PropertyMetadata(false));
    }
}

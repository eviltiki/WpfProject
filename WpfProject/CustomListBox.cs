using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfProject
{
    public class CustomListBox : ListBox
    {
        //public Brush AlternativeBackground
        //{
        //    get { return (Brush)GetValue(AlternativeBackgroundProperty); }
        //    set { SetValue(AlternativeBackgroundProperty, value); }
        //}

        //public static readonly DependencyProperty AlternativeBackgroundProperty =
        //    DependencyProperty.Register("AlternativeBackground", typeof(Brush), typeof(CustomListBox), new PropertyMetadata(null));


        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    base.PrepareContainerForItemOverride(element, item);
        //    var listBoxItem = element as ListBoxItem;
        //    if (listBoxItem != null)
        //    {
        //        var index = IndexFromContainer(element);

        //        if ((index + 1) % 2 != 1)
        //        {
        //            listBoxItem.Background = AlternativeBackground;
        //        }
        //    }
        //}
    }
}

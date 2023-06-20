using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;

namespace WpfProject
{
    public class CustomListView : ListView
    {
        public Brush AlternativeBackground
        {
            get { return (Brush)GetValue(AlternativeBackgroundProperty); }
            set { SetValue(AlternativeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty AlternativeBackgroundProperty =
            DependencyProperty.Register("AlternativeBackground", typeof(Brush), typeof(CustomListView), new PropertyMetadata(null));


        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var listViewItem = element as ListViewItem;
            if (listViewItem != null)
            {
                var index = IndexFromContainer(listViewItem);

                if ((index + 1) % 2 != 1)
                {
                    listViewItem.Background = AlternativeBackground;
                }
            }
        }

        public void RefreshCustomListView()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            view.Refresh();
        }

        private int IndexFromContainer(ListViewItem el)
        {
            int index = this.ItemContainerGenerator.IndexFromContainer(el);
            return index;
        }

    }
}

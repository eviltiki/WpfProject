using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WpfProject;

namespace WpfProject
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }
        public static int counter = 0;
        private Product selectedProduct;
        private CustomListView customListView;


        public int GetIndextFromList(Product product)
        {
            return Products.IndexOf(product);
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Product product = new Product { Id = counter, Name = "Товар", Price = 0 };
                        Products.Add(product);
                        SelectedProduct = product;
                        customListView.RefreshCustomListView();
                    }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Product product = obj as Product;
                      if (product != null)
                      {
                          Products.Remove(product);
                          customListView.RefreshCustomListView();
                      }
                  },
                 (obj) => Products.Count > 0));
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ApplicationViewModel(CustomListView customListView)
        {
            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Телефон", Price = 1700 },
                new Product { Id = 2, Name = "Калькулятор", Price = 70},
                new Product { Id = 3, Name = "Ноутбук", Price = 2850 },
            };

            this.customListView = customListView;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }


    }
}

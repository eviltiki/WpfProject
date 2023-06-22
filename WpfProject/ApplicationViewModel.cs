using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfProject;

namespace WpfProject
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }
        private Product selectedProduct;
        private CustomListView customListView;
        private string connectionString = @"Data Source=ARTHUR-PC\ARTHURSQL;
                                                            Initial Catalog=ShopDB;Integrated Security=True";

        public void LoadDataIntoList()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Товар", connection);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                int id;
                string productName;
                double productPrice;
                string date;

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Код"]);
                    productName = Convert.ToString(reader["Название"]);
                    productPrice = Convert.ToDouble(reader["Цена"]);
                    date = Convert.ToString(reader["Дата"]).Substring(0, 10);

                    Products.Add(new Product { Id = id, Name = productName, Price = productPrice, Date = date });
                }
            }
        }

        public int GetIndextFromList(Product product)
        {
            return Products.IndexOf(product);
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand; /*??*/
                //    (addCommand = new RelayCommand(obj =>
                //    {
                //        var values = obj as Object[];

                //        if (values != null)
                //        {
                //            var name = (string)values[0];
                //            var price = Convert.ToDouble(values[1]);

                //            Product product = new Product { Id = counter, Name = name, Price = price };
                //            Products.Add(product);
                //            SelectedProduct = product;
                //            customListView.RefreshCustomListView();
                //        }

                //    }));
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand; /*??*/
                //    (editCommand = new RelayCommand(obj =>
                //    {
                //        var values = obj as Object[];

                //        if (values != null)
                //        {
                //            var name = (string)values[0];
                //            var price = Convert.ToDouble(values[1]);

                //            SelectedProduct.Name = name;
                //            SelectedProduct.Price = price;

                //            customListView.RefreshCustomListView();
                //        }

                //    },
                //    (obj) => Products.Count > 0));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand; /*??*/
                //  (removeCommand = new RelayCommand(obj =>
                //  {
                //      Product product = obj as Product;
                //      if (product != null)
                //      {
                //          Products.Remove(product);
                //          customListView.RefreshCustomListView();
                //      }
                //  },
                // (obj) => Products.Count > 0));
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

            Products = new ObservableCollection<Product>();

            this.customListView = customListView;

            LoadDataIntoList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }

        //public AddProductToDb()
        //{ 

        //}

    }
}

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
        private MainWindow mainWindow;
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
                return addCommand ??
                (addCommand = new RelayCommand(obj =>
                {
                    var values = obj as Object[];

                    if (values != null)
                    {

                        if (Convert.ToString(values[0]).Length == 0 || Convert.ToString(values[1]).Length == 0 
                            || Convert.ToString(values[2]).Length == 0)
                        {
                            MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        var name = (string)values[0];
                        var price = Convert.ToDouble(values[1]);
                        var date = Convert.ToDateTime(values[2]);

                        if (date < mainWindow.AppStartDate.Date || date > DateTime.Now.Date)
                        {
                            MessageBox.Show("Неверно задана дата!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        string fdate = Convert.ToString(date);

                        fdate = fdate.Substring(6, 4) + "-" + fdate.Substring(3, 2) + "-" + fdate.Substring(0, 2);

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand($"EXEC prДобавлениеТовара '{name}', {price}, '{fdate}'", connection);

                            connection.Open();
                            try
                            {
                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        int id = Convert.ToInt32(reader["Код записи"]);

                                        Product product = new Product { Id = id, Name = name, Price = price, Date = Convert.ToString(date).Substring(0,10) };

                                        Products.Add(product);
                                        SelectedProduct = product;

                                        customListView.RefreshCustomListView();

                                        MessageBox.Show("Товар был успешно создан!", "Ответ", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                            }
                            catch (System.Data.SqlClient.SqlException)
                            {
                                MessageBox.Show("Произошла ошибка при отправке данных на сервер! \nКод ошибки: 1", "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            } 
                        }
                    }

                }));
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                (editCommand = new RelayCommand(obj =>
                {
                    var values = obj as Object[];

                    if (values != null)
                    {
                        if (Convert.ToString(values[0]).Length == 0 || Convert.ToString(values[1]).Length == 0
                            || Convert.ToString(values[2]).Length == 0)
                        {
                            MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        var name = (string)values[0];
                        var price = Convert.ToDouble(values[1]);
                        var date = Convert.ToDateTime(values[2]);

                        if ((date != Convert.ToDateTime(SelectedProduct.Date).Date) && (date < mainWindow.AppStartDate.Date || date > DateTime.Now.Date))
                        {
                            MessageBox.Show("Неверно задана дата!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        string fdate = Convert.ToString(date);

                        fdate = fdate.Substring(6, 4) + "-" + fdate.Substring(3, 2) + "-" + fdate.Substring(0, 2);

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand($"EXEC prОбновлениеТовара {selectedProduct.Id}, '{name}', {price}, '{fdate}'", connection);

                            connection.Open();
                            try
                            {
                                int flag = cmd.ExecuteNonQuery();

                                if (flag > 0)
                                {
                                    SelectedProduct.Name = name;
                                    SelectedProduct.Price = price;
                                    SelectedProduct.Date = Convert.ToString(date).Substring(0, 10);

                                    customListView.RefreshCustomListView();

                                    MessageBox.Show("Данные о товаре были успешно изменены!", "Ответ", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            catch (System.Data.SqlClient.SqlException)
                            {
                                MessageBox.Show("Произошла ошибка при отправке данных на сервер! \nКод ошибки: 1", "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                },
                (obj) => Products.Count > 0));
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

        public ApplicationViewModel(MainWindow mainWindow, CustomListView customListView)
        {
            this.mainWindow = mainWindow;

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

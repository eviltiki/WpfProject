using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using WpfProject;

namespace WpfProject
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } // создаем коллекцию товаров.
        private Product selectedProduct; // выбранный объект.
        private CustomListView customListView; 
        private MainWindow mainWindow;
        private string connectionString = @"Data Source=ARTHUR-PC\ARTHURSQL;
                                                            Initial Catalog=ShopDB;Integrated Security=True";

        public void LoadDataIntoList() // загружаем данные в наш кастомный список.
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product", connection); 

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                int id;
                string productName;
                double productPrice;
                DateTimeOffset date;

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Id"]);
                    productName = Convert.ToString(reader["Name"]);
                    productPrice = Convert.ToDouble(reader["Price"]);
                    date = Convert.ToDateTime(Convert.ToString(reader["Date"]));

                    Products.Add(new Product { Id = id, Name = productName, Price = productPrice, Date = date }); // добавляем в коллекцию Products объекты базы данных.
                }
            }
        }

        public int GetIndextFromList(Product product) // возвращает номер товара в списке.
        {
            return Products.IndexOf(product);
        }

        private RelayCommand addCommand; // команда добавления объекта в список и бд.
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
                        var date = Convert.ToDateTime(values[2]) + new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                        if (date < mainWindow.AppStartDate || date > DateTime.Now)
                        {
                            MessageBox.Show("Неверно задана дата!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand($"EXEC prAddProduct '{name}', {price}, '{date}'", connection);

                            connection.Open();
                            try
                            {
                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        int id = Convert.ToInt32(reader["Product Id"]);

                                        Product product = new Product { Id = id, Name = name, Price = price, Date = date };

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

        private RelayCommand editCommand; // команда изменения объекта списка и бд.
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
                        var date = Convert.ToDateTime(values[2]) + new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);


                        if ((date.Date != SelectedProduct.Date.Date) && (date < mainWindow.AppStartDate || date > DateTime.Now))
                        {
                            MessageBox.Show("Неверно задана дата!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand($"EXEC prUpdateProduct {selectedProduct.Id}, '{name}', {price}, '{date}'", connection);

                            connection.Open();
                            try
                            {
                                int flag = cmd.ExecuteNonQuery();

                                if (flag > 0)
                                {
                                    SelectedProduct.Name = name;
                                    SelectedProduct.Price = price;
                                    SelectedProduct.Date = date;

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
        public RelayCommand RemoveCommand // команда удаления объекта из списка и бд.
        {
            get
            {
                return removeCommand ??
                (removeCommand = new RelayCommand(obj =>
                {
                    Product product = obj as Product;

                    if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK && product != null)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand($"EXEC prDeleteProduct {product.Id}", connection);

                            connection.Open();
                            try
                            {
                                int flag = cmd.ExecuteNonQuery();

                                if (flag > 0) 
                                {
                                    Products.Remove(product);

                                    customListView.RefreshCustomListView(); // обновляем отображение списка.

                                    MessageBox.Show("Выбранная запись была успено удалена!", "Ответ", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            catch (System.Data.SqlClient.SqlException e)
                            {
                                MessageBox.Show($"Произошла ошибка при отправке данных на сервер! \nКод ошибки: 3.\n Ошибка:\n{e.Message}", "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
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
                OnPropertyChanged("SelectedProduct"); // сообщаем о том, что одно из свойств было изменено, чтобы перерисовать список.
            }
        }

        public ApplicationViewModel(MainWindow mainWindow, CustomListView customListView)
        {
            this.mainWindow = mainWindow;

            Products = new ObservableCollection<Product>(); // инициализируем нашу коллекцию.

            this.customListView = customListView; // кастомный список

            LoadDataIntoList(); // загружаем данные из бд в наш список.
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }

    }
}

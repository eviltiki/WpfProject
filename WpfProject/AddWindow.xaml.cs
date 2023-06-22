﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow(int WindowType = 0, Product product = null)
        {
            InitializeComponent();

            if (WindowType == 0)
            {
                this.Title = "Добавление товара";
                addBtn.Content = "Добавить";

                Binding binding = new Binding();

                binding.Path = new PropertyPath("AddCommand");

                addBtn.SetBinding(Button.CommandProperty, binding);
            }
            else 
            {
                this.Title = "Изменение данных о товаре";
                addBtn.Content = "Изменить";

                nameInput.Text = product.Name;
                priceInput.Text = Convert.ToString(product.Price);

                Binding binding = new Binding();

                binding.Path = new PropertyPath("EditCommand");

                addBtn.SetBinding(Button.CommandProperty, binding);

            }
        }

        private void addBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}

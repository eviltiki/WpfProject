﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int counter = 0;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(customListView);

            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(0);
            addWindow.DataContext = DataContext;
           
            addWindow.ShowDialog();
            

            
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customListView.SelectedItem == null)
            {
                MessageBox.Show($"Произошла ошибка. Выберите товар из списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddWindow addWindow = new AddWindow(1, customListView.SelectedItem as Product);
            addWindow.DataContext = DataContext;

            addWindow.ShowDialog();
        }
    }
}

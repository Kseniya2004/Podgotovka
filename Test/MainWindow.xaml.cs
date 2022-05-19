using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Test.Classes;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                ConnectHelper.fileName = openFileDialog.FileName;
                ConnectHelper.ReadListFromFile(ConnectHelper.fileName);
            }
            else
                return;
            dtgListLibrary.ItemsSource = ConnectHelper.libraries.ToList();
        }  
        
        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
            {
                string file = saveFileDialog.FileName;
                ConnectHelper.SaveListFromFile(file);
            }            
        }

        private void MiExit_Click(object sender, RoutedEventArgs e) =>
           Close();

        private void MiPrint_Click(object sender, RoutedEventArgs e)
        {
            dtgListLibrary.ItemsSource = ConnectHelper.libraries.ToList();
            dtgListLibrary.SelectedIndex = -1;
        }

        private void MiClear_Click(object sender, RoutedEventArgs e)
        {
            dtgListLibrary.ItemsSource = null;
        }

        private void MiAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBooks windowAdd = new WindowAddBooks();
            windowAdd.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBooks windowAdd = new WindowAddBooks((sender as Button).DataContext as Library);
            windowAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show($"Вы точно хотите удалить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                int ind = dtgListLibrary.SelectedIndex;
                ConnectHelper.libraries.RemoveAt(ind);
                dtgListLibrary.ItemsSource = ConnectHelper.libraries.ToList();
                ConnectHelper.SaveListFromFile(ConnectHelper.fileName);
            }
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            dtgListLibrary.ItemsSource = ConnectHelper.libraries.OrderBy(x => x.Avtor).ToList();
        }

        private void RbDoun_Checked(object sender, RoutedEventArgs e)
        {
            dtgListLibrary.ItemsSource = ConnectHelper.libraries.OrderByDescending(x => x.Avtor).ToList();
        }

        private void TxbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgListLibrary.ItemsSource = ConnectHelper.libraries.Where(x =>
            x.Avtor.ToLower().Contains(TxbSerch.Text.ToLower())).ToList();
        }
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( CmbFiltr.SelectedIndex == 0)
            {
                dtgListLibrary.ItemsSource = ConnectHelper.libraries.Where(x =>
                x.CountBook >= 0 && x.CountBook <= 10).ToList();
                MessageBox.Show("Недостаточное количество книг на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            if (CmbFiltr.SelectedIndex == 1)
            {
                dtgListLibrary.ItemsSource = ConnectHelper.libraries.Where(x =>
                x.CountBook >= 11 && x.CountBook <= 50).ToList();
                MessageBox.Show("Необходимо пополнить запасы в ближайшее время!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dtgListLibrary.ItemsSource = ConnectHelper.libraries.Where(x =>
                x.CountBook >= 51).ToList();
                MessageBox.Show("Достаточное количество книг на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }        
    }
}

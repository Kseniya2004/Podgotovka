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
using System.Windows.Shapes;
using Test.Classes;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для WindowAddBooks.xaml
    /// </summary>
    public partial class WindowAddBooks : Window
    {
        int mode;
        public WindowAddBooks()
        {
            InitializeComponent();
            mode = 0;
        }
        public WindowAddBooks(Library library)
        {
            InitializeComponent();
            TxbAvtor.Text = library.Avtor;
            TxbName.Text = library.Name;
            TxbYear.Text = library.Year.ToString();
            TxbPrice.Text = library.Price.ToString();
            TxbCountBook.Text = library.CountBook.ToString();
            mode = 1;
            BtnAddBook.Content = "Сохранить";
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            //исключения
            try
            {
                if (int.Parse(TxbYear.Text) < 0)
                {
                    MessageBox.Show("Значение не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxbYear.Clear();
                    TxbYear.Focus();
                    return;
                }
                else if (double.Parse(TxbPrice.Text) < 0)
                {
                    MessageBox.Show("Значение не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxbPrice.Clear();
                    TxbPrice.Focus();
                    return;
                }
                else if (int.Parse(TxbCountBook.Text) < 0)
                {
                    MessageBox.Show("Значение не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxbCountBook.Clear();
                    TxbCountBook.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //добавление данных
            if (mode == 0)
            {
                try
                {
                    Library library = new Library()
                    {
                        Avtor = TxbAvtor.Text,
                        Name = TxbName.Text,
                        Year = int.Parse(TxbYear.Text),
                        Price = double.Parse(TxbPrice.Text),
                        CountBook = int.Parse(TxbCountBook.Text)
                    };
                    ConnectHelper.libraries.Add(library);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            //редактирование файла
            else
            {
                try
                {
                    for (int i = 0; i < ConnectHelper.libraries.Count; i++)
                    {
                        if (ConnectHelper.libraries[i].Avtor == TxbAvtor.Text)
                        {
                            ConnectHelper.libraries[i].Name = TxbName.Text;
                            ConnectHelper.libraries[i].Year = int.Parse(TxbYear.Text);
                            ConnectHelper.libraries[i].Price = double.Parse(TxbPrice.Text);
                            ConnectHelper.libraries[i].CountBook = int.Parse(TxbCountBook.Text);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ConnectHelper.SaveListFromFile(ConnectHelper.fileName);
            this.Close();
        }
    }
}

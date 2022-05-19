using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Classes;
using System.IO;
using System.Windows;

namespace Test.Classes
{
    class ConnectHelper
    {
        public static List<Library> libraries = new List<Library>();
        
        public static string fileName;

        public static void ReadListFromFile (string filename)
        {
            libraries.Clear();
            try
            { 
                StreamReader sr = new StreamReader(filename, Encoding.UTF8);
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] items = line.Split(';');
                    Library library = new Library
                    {
                        Avtor = items[0].Trim(),
                        Name = items[1].Trim(),
                        Year = int.Parse(items[2].Trim()),
                        Price = double.Parse(items[3].Trim()),
                        CountBook = int.Parse(items[4].Trim())
                    };
                    libraries.Add(library);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неверный формат файла!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void SaveListFromFile(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
                foreach (Library lb in libraries)
                {
                    sw.WriteLine($"{lb.Avtor}; {lb.Name}; {lb.Year}; {lb.Price}; {lb.CountBook}");
                }
                sw.Close();
            }
            catch
            {
                MessageBox.Show($"Файл не открыт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}

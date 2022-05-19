using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Classes
{
    public class Library
    {
        //Поля
        private string avtor;
        private string name;
        private int year;
        private double price;
        private int countBook;

        //Свойства
        public string Avtor { get {return avtor;} set {avtor = value;} }
        public string Name { get { return name; } set { name = value; } }
        public int Year { get { return year; } set { year = value; } }
        public double Price { get { return price; } set { price = value; } }
        public int CountBook { get { return countBook; } set { countBook = value; } }
        //Конструкторы
        public Library()//конструктор по умолчанию
        {
            avtor = "";
            name = "";
            year = 0;
            price = 0;
            countBook = 0;
        }
       public Library(string avtor, string name, int year, double price, int countBook)//конструктор с параметром
        {
            Avtor = avtor;
            Name = name;
            Year = year;
            Price = price;
            CountBook = countBook; 
        }      
    }
}

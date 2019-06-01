using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BilgeAdam.Data.Expressions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var people = new List<Person>
            {
                new Person("Can Perk"),
                new Person("Deniz Aslan"),
                new Person("Gamze Efendioğlu"),
                new Person("Ayşegül Sarıkaya"),
                new Person("Gülşah Duman"),
                new Person("Fatih Eser"),
                new Person("Berkay Tüzen"),
                new Person("Ali Ercan"),
                new Person("Mehmet Nur İlhan"),
                new Person("Alper Köklü"),
                new Person("Mazlum İlhanlı"),
                new Person("Neslihan Şimşek")
            };
            //Fluent Expression
            var oldestDate = people.Min(i => i.BirthDate);
            var oldestPerson = people.OrderBy(i => i.BirthDate).First();

            var peopleWhoLovesP = people.Where(i => i.FullName.ToLower().Contains("p")).ToList();
            var youngestPeople = people.OrderByDescending(i => i.BirthDate).Take(3).ToList();
            var ThirdYoungest = people.OrderByDescending(i => i.BirthDate).Skip(2).Take(3).First();
        }
    }
}

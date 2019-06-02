using BilgeAdam.Data.Expressions.Context;
using BilgeAdam.Data.Expressions.DTOs;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BilgeAdam.Data.Expressions
{
    public partial class frmSamples : Form
    {
        public frmSamples()
        {
            InitializeComponent();
        }
        public NorthwindEntities Context { get; set; }
        private void frmSamples_Load(object sender, EventArgs e)
        {
            Context = new NorthwindEntities();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            //1956 yılından önce doğan Londra'daki personeller
            /* SELECT * FROM Employees
               WHERE BirthDate < '1956-01-01' AND City = 'London' */

            var result = Context.Employees
                                .Where(w => w.BirthDate < new DateTime(1956, 1, 1) && w.City == "London")
                                .Select(s => new EmployeeDTO
                                {
                                    Id = s.EmployeeID,
                                    Name = s.FirstName + " " + s.LastName,
                                    Country = s.Country
                                })
                                .ToList();
            dgvResult.DataSource = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Stokta kalmayan ürünler yada ucuz olanlar
            var result = Context.Products
                                .Where(w => w.UnitsInStock == 0 || w.UnitPrice < 10)
                                .Select(s => new ProductDTO
                                {
                                    Id = s.ProductID,
                                    Name = s.ProductName,
                                    Price = s.UnitPrice,
                                    Stock = s.UnitsInStock
                                })
                                .ToList();
            dgvResult.DataSource = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1997 Temmuz ayında alınanlar
            var result = Context.Orders
                                .Where(w => w.OrderDate.Value.Year == 1997 && w.OrderDate.Value.Month == 7)
                                .Select(s => new OrderDTO
                                {
                                    Id = s.OrderID,
                                    Date = s.OrderDate
                                })
                                .ToList();
            dgvResult.DataSource = result;
        }
    }

    class EmployeeDTO
    {
        [DisplayName("Personel Kodu")]
        public int Id { get; set; }
        [DisplayName("Tam Adı")]
        public string Name { get; set; }
        [DisplayName("Ülke")]
        public string Country { get; set; }
    }

    class OrderDTO
    {
        [DisplayName("Sipariş Kodu")]
        public int Id { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime? Date { get; set; }
    }
}

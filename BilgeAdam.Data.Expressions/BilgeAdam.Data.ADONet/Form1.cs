using BilgeAdam.Data.ADOManager.Helpers;
using BilgeAdam.Data.ADONet.Extensions;
using BilgeAdam.Data.ADONet.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BilgeAdam.Data.ADONet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var q = new QueryHelper();
            var catQuery = "SELECT CategoryId AS Id, CategoryName AS Name FROM Categories ORDER BY Name";
            cmbCategories.BindComboBox(q.GetData<Category>(catQuery), "Id", "Name");

            var empQuery = "SELECT EmployeeId AS Id, FirstName + ' ' + LastName AS FullName FROM Employees ORDER BY FullName";
            cmbEmployees.BindComboBox(q.GetData<Employee>(empQuery), "Id", "FullName");

            var supQuery = "SELECT SupplierId AS Id, CompanyName AS Name FROM Suppliers ORDER BY Name";
            cmbSuppliers.BindComboBox(q.GetData<Supplier>(supQuery), "Id", "Name");
        }

        private void btnListProducts_Click(object sender, EventArgs e)
        {
            var q = new QueryHelper();
            var query = @"SELECT ProductId AS Id, ProductName AS Name, 
                                 UnitsInStock AS Stock, UnitPrice AS Price
                          FROM Products 
                          WHERE CategoryID = @catId AND SupplierID = @supId";
            dgvProducts.DataSource = q.GetData<Product>(query,
                                                        new
                                                        {
                                                            catId = cmbCategories.SelectedValue,
                                                            supId = cmbSuppliers.SelectedValue
                                                        });
        }

        private void btnListOrders_Click(object sender, EventArgs e)
        {
            var q = new QueryHelper();
            var query = @"SELECT p.ProductName, d.UnitPrice, d.Quantity, d.UnitPrice * d.Quantity AS Summary 
                          FROM [Order Details] d
                          INNER JOIN Products p ON d.ProductID = p.ProductID
                          WHERE d.OrderID = @orderId";
            var result = q.GetData<ProductOrder>(query, new { orderId = int.Parse(txtOrderNo.Text) });
            var total = result.Sum(i => i.Summary);
            dgvOrderDetail.DataSource = result;
            lblTotal.Text = $"Toplam Fiyat : {total} ₺";
        }
    }
}

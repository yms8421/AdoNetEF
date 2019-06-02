using BilgeAdam.Data.Expressions.Context;
using BilgeAdam.Data.Expressions.DTOs;
using BilgeAdam.Data.Expressions.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BilgeAdam.Data.Expressions
{
    public partial class frmHelloEF : Form
    {
        public frmHelloEF()
        {
            InitializeComponent();
            PageNumber = 1;
        }
        public int PageNumber { get; set; }
        //public int RowCount { get { return 10; } }
        public int RowCount => 10;
        private void frmHelloEF_Load(object sender, EventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var query = context.Categories.Select(s => new ComboBoxItem<int>
                {
                    Id = s.CategoryID,
                    Text = s.CategoryName
                });
                cmbCategories.DataSource = query.ToList();
                cmbCategories.ValueMember = nameof(ComboBoxItem<int>.Id);
                cmbCategories.DisplayMember = nameof(ComboBoxItem<int>.Text);

                cmbEmployees.DataSource = context.Employees
                                                 .Select(s => new ComboBoxItem<int>
                                                 {
                                                     Id = s.EmployeeID,
                                                     Text = s.FirstName + " " + s.LastName
                                                 })
                                                 .ToList();
                cmbEmployees.ValueMember = nameof(ComboBoxItem<int>.Id);
                cmbEmployees.DisplayMember = nameof(ComboBoxItem<int>.Text);

                cmbSuppliers.DataSource = context.Suppliers
                                                 .Select(s => new ComboBoxItem<int>
                                                 {
                                                     Id = s.SupplierID,
                                                     Text = s.CompanyName
                                                 }).ToList();
                cmbSuppliers.ValueMember = nameof(ComboBoxItem<int>.Id);
                cmbSuppliers.DisplayMember = nameof(ComboBoxItem<int>.Text);
                //cmbSuppliers.ValueMember = "SupplierId";
                //cmbSuppliers.DisplayMember = "CompanyName";

                cmbCustomers.DataSource = context.Customers
                                                     .Select(s => new ComboBoxItem<string>
                                                     {
                                                         Id = s.CustomerID,
                                                         Text = s.CompanyName + "( " + s.ContactName + " )"
                                                     }).ToList();
                cmbCustomers.ValueMember = nameof(ComboBoxItem<string>.Id);
                cmbCustomers.DisplayMember = nameof(ComboBoxItem<string>.Text);
            }
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var ctx = new NorthwindEntities())
            {
                var result = ctx.Products
                                .Select(i => new ProductDTO
                                {
                                    Id = i.ProductID,
                                    Name = i.ProductName,
                                    Price = i.UnitPrice,
                                    Stock = i.UnitsInStock
                                })
                                .OrderBy(i => i.Id)
                                .Skip((PageNumber - 1) * RowCount)
                                .Take(RowCount)
                                .ToList();
                dgvProducts.DataSource = result;
                btnPrevious.Enabled = PageNumber > 1;
                btnNext.Enabled = result.Count == 10;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            PageNumber--;
            LoadProducts();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            PageNumber++;
            LoadProducts();
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            var f = new frmNewProduct();
            f.ShowDialog();
        }

        private void cbtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 1)
            {
                var row = dgvProducts.SelectedRows[0].DataBoundItem as ProductDTO;
                if (row == null)
                {
                    return;
                }

                var msg = $"{row.Name} isimli ürünü silmek istiyor musunuz?";
                var result = MessageBox.Show(msg, "Ürün Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //delete başlangıç
                    using (var ctx = new NorthwindEntities())
                    {
                        var product = ctx.Products.FirstOrDefault(i => i.ProductID == row.Id);
                        if (product != null)
                        {
                            ctx.Products.Remove(product);
                            ctx.SaveChanges();
                        }
                    }
                    //delete bitiş
                    LoadProducts();
                }
            }
        }

        private void cbtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 1)
            {
                var row = dgvProducts.SelectedRows[0].DataBoundItem as ProductDTO;
                if (row == null)
                {
                    return;
                }

                var f = new frmNewProduct(row.Id);
                f.ShowDialog();
                LoadProducts();
            }
        }
    }
}

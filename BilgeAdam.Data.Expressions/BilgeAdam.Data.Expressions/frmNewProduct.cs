using BilgeAdam.Data.Expressions.Context;
using BilgeAdam.Data.Expressions.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BilgeAdam.Data.Expressions
{
    public partial class frmNewProduct : Form
    {
        public frmNewProduct(int productId = 0)
        {
            EditMode = productId > 0;
            ProductId = productId;
            InitializeComponent();
        }
        public bool EditMode { get; set; }
        public int ProductId { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var ctx = new NorthwindEntities())
            {
                if (EditMode)
                {
                    UpdateProduct(ctx);
                }
                else
                {
                    NewProduct(ctx);
                }
            }
        }

        private void NewProduct(NorthwindEntities ctx)
        {
            //Insert  başlangıç
            var p = new Product
            {
                ProductName = txtProductName.Text,
                UnitsInStock = (short)nudStock.Value,
                UnitPrice = nudPrice.Value,
                CategoryID = (int)cmbCategories.SelectedValue,
                SupplierID = (int)cmbSuppliers.SelectedValue
            };
            ctx.Products.Add(p);
            ctx.SaveChanges(); //kaydet
            //Insert  bitiş
            MessageBox.Show("Ürün Eklendi");
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
            cmbCategories.SelectedIndex = -1;
            cmbSuppliers.SelectedIndex = -1;
        }

        private void UpdateProduct(NorthwindEntities ctx)
        {
            //Update başlangıç
            var product = ctx.Products.FirstOrDefault(i => i.ProductID == ProductId);
            if (product != null)
            {
                product.ProductName = txtProductName.Text;
                product.UnitsInStock = (short)nudStock.Value;
                product.UnitPrice = nudPrice.Value;
                product.CategoryID = (int)cmbCategories.SelectedValue;
                product.SupplierID = (int)cmbSuppliers.SelectedValue;
                ctx.SaveChanges();
                //Update bitiş
                this.Close();
            }
        }

        private void frmNewProduct_Load(object sender, EventArgs e)
        {
            btnSave.Text = EditMode ? "Güncelle" : "Kaydet";
            using (var context = new NorthwindEntities())
            {
                cmbCategories.DataSource = context.Categories
                                                  .Select(s => new ComboBoxItem<int>
                                                  {
                                                      Id = s.CategoryID,
                                                      Text = s.CategoryName
                                                  }).ToList();
                cmbCategories.ValueMember = nameof(ComboBoxItem<int>.Id);
                cmbCategories.DisplayMember = nameof(ComboBoxItem<int>.Text);

                cmbSuppliers.DataSource = context.Suppliers
                                                 .Select(s => new ComboBoxItem<int>
                                                 {
                                                     Id = s.SupplierID,
                                                     Text = s.CompanyName
                                                 }).ToList();
                cmbSuppliers.ValueMember = nameof(ComboBoxItem<int>.Id);
                cmbSuppliers.DisplayMember = nameof(ComboBoxItem<int>.Text);

                if (EditMode)
                {
                    var product = context.Products.FirstOrDefault(i => i.ProductID == ProductId);
                    cmbCategories.SelectedValue = product.CategoryID;
                    cmbSuppliers.SelectedValue = product.SupplierID;
                    txtProductName.Text = product.ProductName;
                    nudPrice.Value = product.UnitPrice.HasValue ? product.UnitPrice.Value : 0;
                    nudStock.Value = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : 0;
                }
            }

            
        }
    }
}

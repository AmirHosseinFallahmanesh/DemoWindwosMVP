using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndPoint
{
    public partial class Form1 : Form
    {
        KavehEntities context;
        public Form1()
        {
            InitializeComponent();
            context = new KavehEntities();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                decimal price = Convert.ToDecimal(textBoxPrice.Text);
                string unit = textBoxUnit.Text;

                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    Unit = unit
                };

                context.Products.Add(product);
                context.SaveChanges();
                MessageBox.Show("با موفقیت ثبت شد", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("خطا در ثبت کالا لطفا مجددا تلاش کنید", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreashListBox();
            textBoxName.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            textBoxUnit.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            RefreashListBox();
        }

        void RefreashListBox()
        {
            listBoxProducts.Items.Clear();
            List<Product> data = context.Products.AsNoTracking().ToList();
            foreach (var item in data)
            {
                listBoxProducts.Items.Add(item.Name);
            }
        }
        void RefreashListBox(List<Product> products)
        {
            listBoxProducts.Items.Clear();
            foreach (var item in products)
            {
                listBoxProducts.Items.Add(item.Name);
            }
        }

        private void listBoxProducts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string name = listBoxProducts.SelectedItem.ToString();
            int id = context.Products.Single(a => a.Name == name).ProductId;
            EditForm editForm = new EditForm(id);
            editForm.ShowDialog();

            RefreashListBox();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                decimal minPrice = Convert.ToDecimal(textBoxSearchPriceMin.Text);
                decimal maxPrice = Convert.ToDecimal(textBoxSearchPriceMax.Text);

                List<Product> products = context.Products
                    .Where(a => a.Price >= minPrice && a.Price < maxPrice).ToList();
                if (products.Count != 0)
                {
                    RefreashListBox(products);
                }
                else
                {
                    MessageBox.Show("کالا یی با این مشخصات یافت نشد", "پیام");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطا در جستجو لطفا مجددا تلاش کنید", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           

        }

        private void textBoxSearchName_TextChanged(object sender, EventArgs e)
        {
            string search = textBoxSearchName.Text;
            List<Product> products= context.Products.Where(a => a.Name.StartsWith(search)).ToList();
            RefreashListBox(products);

        }
    }
}

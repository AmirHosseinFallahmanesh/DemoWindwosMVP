using Demo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        ProductRepository productRepository;
        CategoryRepositry categoryRepositry;
        public Form1()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepositry = new CategoryRepositry();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                decimal price = Convert.ToDecimal(textBoxPrice.Text);
                string unit = textBoxUnit.Text;

                string categoryName = comboBoxCategory.Text;
                Category category= categoryRepositry.FindByName(categoryName);

                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    Unit = unit,
                    CatagoryId=category.CatagoryId
                  
                };

                productRepository.Add(product);
                MessageBox.Show("با موفقیت ثبت شد", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreashListBox();
            }
            catch (Exception ex)
            {

                MessageBox.Show("خطا در ثبت کالا لطفا مجددا تلاش کنید", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
            textBoxName.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            textBoxUnit.Text = string.Empty;
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreashListBox();
            List<string> categories = categoryRepositry.GetAll().Select(a => a.Name).ToList();

            foreach (var item in categories)
            {
                comboBoxCategory.Items.Add(item);
            }

        }

        void RefreashListBox()
        {
            listBoxProducts.Items.Clear();
            List<Product> data = productRepository.GetAll();
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
            int id = productRepository.FindByName(name).ProductId;
            EditForm editForm = new EditForm(id);
            editForm.ShowDialog();

            RefreashListBox();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                decimal minPrice = Convert.ToDecimal(textBoxSearchPriceMin.Text);
                decimal maxPrice = Convert.ToDecimal(textBoxSearchPriceMax.Text);

                List<Product> products = productRepository.FilterPrice(minPrice, maxPrice);
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
    }
}

using Demo.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Demo
{
    public partial class EditForm : Form
    {
        ProductRepository productRepository;
        CategoryRepositry categoryRepositry;
        Product product;
        public EditForm(int id)
        {
           
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepositry = new CategoryRepositry();
            product= productRepository.Get(id);
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            List<string> categories = categoryRepositry.GetAll().Select(a => a.Name).ToList();

            foreach (var item in categories)
            {
                comboBoxCategory.Items.Add(item);
            }


            textBoxName.Text = product.Name;
            textBoxPrice.Text = product.Price.ToString();
            textBoxUnit.Text = product.Unit;
            comboBoxCategory.Text = product.Category.Name;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                product.Name = textBoxName.Text;
                product.Price = Convert.ToDecimal(textBoxPrice.Text);
                product.Unit = textBoxUnit.Text;

                string categoryName = comboBoxCategory.Text;
                Category category = categoryRepositry.FindByName(categoryName);

                product.CatagoryId = category.CatagoryId;
                productRepository.Update(product);


                MessageBox.Show("با موفقیت ویرایش شد", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("خطا در ثبت کالا لطفا مجددا تلاش کنید", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("آیا مطمن هستید؟", "پیام", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                productRepository.Delete(product);
                this.Close();
            }
        }
    }
}

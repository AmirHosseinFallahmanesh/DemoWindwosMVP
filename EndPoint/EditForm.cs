using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndPoint
{
    public partial class EditForm : Form
    {
        KavehEntities context;
        Product product;
        public EditForm(int id)
        {
            
            InitializeComponent();
            context = new KavehEntities();
            product = context.Products.Find(id);
        }

        private void EditForm_Load(object sender, EventArgs e)
        {


            textBoxName.Text = product.Name;
            textBoxPrice.Text = product.Price.ToString();
            textBoxUnit.Text = product.Unit;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                product.Name = textBoxName.Text;
                product.Price = Convert.ToDecimal(textBoxPrice.Text);
                product.Unit = textBoxUnit.Text;
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                MessageBox.Show("با موفقیت ویرایش شد", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("خطا در ثبت کالا لطفا مجددا تلاش کنید", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            context.Dispose();
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult= MessageBox.Show("آیا مطمن هستید؟", "پیام", MessageBoxButtons.OKCancel);
            if (dialogResult==DialogResult.OK)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                this.Close();
            }
          
        }
    }
}

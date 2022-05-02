using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productTableAdapter1.Fill(this.advDataSet1.Product);
            this.productPhotoTableAdapter1.Fill(this.advDataSet1.ProductPhoto);
            YearsintoCombox();

        }

        private void btnall_Click(object sender, EventArgs e)
        {
            var q = from ph in this.advDataSet1.ProductPhoto
                    select ph;

            lblMaster.Text = "Master   共"+q.ToList().Count+"筆";
            resetBindingsource();
            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
        }


        void resetBindingsource()
        {
            this.bindingSource1.Clear();
            this.pictureBox1.DataBindings.Clear();
        }


        private void btnran_Click(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker1.Value;
            DateTime d2 = dateTimePicker2.Value;

            //var q = from ph in this.advDataSet1.ProductPhoto
            //        where ph.ModifiedDate > d1 && ph.ModifiedDate < d2
            //        select ph;

            var q = advDataSet1.ProductPhoto.Where(ph => ph.ModifiedDate > d1 && ph.ModifiedDate < d2);
            lblMaster.Text = "Master   共" + q.ToList().Count + "筆";
            resetBindingsource();
            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
        }

        private void btnyear_Click(object sender, EventArgs e)
        {
            if(this.comboBoxyear.SelectedItem != null)
            {
                var q = this.advDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Year == (int)comboBoxyear.SelectedItem);
                lblMaster.Text = "Master   共" + q.ToList().Count + "筆";
                resetBindingsource();
                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
            }

        }

        void YearsintoCombox()
        {
            var q = this.advDataSet1.ProductPhoto.Select(y => y.ModifiedDate.Year).OrderBy(y => y).Distinct();


            foreach (int item in q)
            {
                this.comboBoxyear.Items.Add(item);
            }
        }

        private void btnseason_Click(object sender, EventArgs e)
        {
            int n = comboBoxSeason.SelectedIndex;
            resetBindingsource();
            if (n==0)
            {
                var q = this.advDataSet1.ProductPhoto.Where(ph => ph.ModifiedDate.Month >0 && ph.ModifiedDate.Month <4);
                lblMaster.Text = "Master   共" + q.ToList().Count + "筆";

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
            }
            if(n==1)
            {
                var q = this.advDataSet1.ProductPhoto.Where(ph => ph.ModifiedDate.Month > 3 && ph.ModifiedDate.Month < 7);
                lblMaster.Text = "Master   共" + q.ToList().Count + "筆";

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
            }
            if(n==2)
            {
                var q = this.advDataSet1.ProductPhoto.Where(ph => ph.ModifiedDate.Month > 6 && ph.ModifiedDate.Month < 10);
                lblMaster.Text = "Master   共" + q.ToList().Count + "筆";

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
            }
            if(n==3)
            {
                var q = this.advDataSet1.ProductPhoto.Where(ph => ph.ModifiedDate.Month > 9 && ph.ModifiedDate.Month < 13);
                lblMaster.Text = "Master   共" + q.ToList().Count + "筆";

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "LargePhoto", true);
            }

        }

    }
}

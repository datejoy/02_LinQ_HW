using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinQ_HW
{
    public partial class FormHW : Form
    {
        public FormHW()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            fillYearCombox();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
        }

        void fillYearCombox()
        {
            /*EnumerableRowCollection*/
            IEnumerable<int> oyears = from o in this.nwDataSet1.Orders
                                      group o by o.OrderDate.Year into groupyear  //放到另一個空間
                                                                                  //   where IsDstinct(o.OrderDate.Year)
                                                                                  //  orderby o.OrderDate.Year ascending
                                      select groupyear.Key;

            foreach (int n in oyears)
            {
                this.comboBox1.Items.Add(n);
            }
            #region 用方法
            //IEnumerable<int> oy = from o in this.nwDataSet1.Orders
            //                      select o.OrderDate.Year;
            //foreach (int year in oy)
            //{
            //    if(oy.)
            //    this.comboBox1.Items.Add(oy);
            //}
            //this.comboBox1.Items.Add(oyears.ToList());
            //private bool IsDstinct(NWDataSet.OrdersRow o)
            //{
            //    return ;
            //}
            #endregion
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            IEnumerable<LinQ_HW.NWDataSet.OrdersRow> allorder = from o in this.nwDataSet1.Orders
                                                              //  where  /*!o.IsShipRegionNull() &&*/ !o.IsShippedDateNull() /*&& !o.IsShipPostalCodeNull()*/
                                                                select o;
            //在DataSet的資料行裡設定DataType屬性，將System.DateTime改成object就可以顯示null

            this.bindingSource1.DataSource = allorder.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;

            showinGriview2();
        }

        void showinGriview2()
        {
            //DataRow dro = (DataRow)this.bindingSource1.Current;
            ////以OrderID找
            //int id = (int)dro[0];  //row的第0列
            //int i = 0;
            //while (id !=/*this.nwDataSet1.Orders[i].OrderID */ )         //ID不重複
            //{
            //    i++;
            //    this.dataGridView2.DataSource = this.nwDataSet1.Orders[i].GetChildRows("FK_Order_Details_Orders");
            //}

        }


        //某年訂單&明細
        private void btnWhen_Click(object sender, EventArgs e)
        {
            //Orders
            if(comboBox1.SelectedItem !=null)
            {
                IEnumerable<NWDataSet.OrdersRow> YearOfOrder = from o in nwDataSet1.Orders
                                                               where o.OrderDate.Year == (int)comboBox1.SelectedItem
                                                               select o;
                this.dataGridView1.DataSource = YearOfOrder.ToList();
               // //Order_Details
               // IEnumerable<NWDataSet.Order_DetailsRow> detail = from od in nwDataSet1.Order_Details
               //                                                  join o in nwDataSet1.Orders
               //                                                  on od.OrderID equals o.OrderID
               //                                                  where o.OrderDate.Year==(int)comboBox1.SelectedItem
               //                                                  select od;
               //this.dataGridView2.DataSource = detail.ToList();
            }
            else
            {
                MessageBox.Show("請選擇年份");
            }



        }


        //FileInfo[]  .Log  擋
        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var log = from f in files
                      where f.Extension ==".log"
                      select f;

            this.dataGridView1.DataSource = log.ToList() ;

        }


        // FileInfo[]   - 2017 Created - oerder 
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            
            System.IO.FileInfo[] files = dir.GetFiles();
            
            IEnumerable<System.IO.FileInfo> creat = from f in files
                                                    where f.CreationTime.Year == 2022
                                                    select f;
            this.dataGridView1.DataSource = creat.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"c:\windows");
            
            System.IO.FileInfo[] files = directory.GetFiles();

            IEnumerable<System.IO.FileInfo> large = from f in files
                                                    where f.Length > 2000
                                                    select f;
            this.dataGridView1.DataSource = large.ToList();

        }


        //上一頁
        private void button12_Click(object sender, EventArgs e)
        {
           // int page;
            if(int.TryParse(textBox1.Text,out int page))
            {
                if(count > 1/**page>page*/)
                {
                    count--;
                    IEnumerable<NWDataSet.ProductsRow> allpro = from n in this.nwDataSet1.Products
                                                                select n;
                    IEnumerable<NWDataSet.ProductsRow> pre10 = allpro.Skip((count-1) * page).Take(page);
                    this.dataGridView1.DataSource = pre10.ToList();
                    label2.Text = count + "";
                }

            }
            else
            {
                MessageBox.Show("請輸入數字");
            }
        
        }

        //下一頁
        int count = 0;
        private void button13_Click(object sender, EventArgs e)
        {
            int page;
            if (int.TryParse(textBox1.Text, out page))
            {
                
                if(count*page<this.nwDataSet1.Products.Rows.Count)   //次數<頁數
                {
                   
                    //載入資料表
                    IEnumerable<NWDataSet.ProductsRow> allpro = from n in this.nwDataSet1.Products
                                                                select n;
                    var next10 = allpro.Skip(count * page).Take(page);

                    this.dataGridView1.DataSource = next10.ToList();
                    count++;
                    if(count * page > this.nwDataSet1.Products.Rows.Count)

                    label2.Text = count + "";
                }

                
                else
                {
                    count = count - 1;
                }

            }
            else
            {
                MessageBox.Show("請輸入數字");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var q = from od in this.nwDataSet1.Order_Details
                        where od.OrderID == (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value
                        select od;
                this.dataGridView2.DataSource = q.ToList();
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
           
        }

    }
}

using LinQ_HW;
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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }


        //int[]  分三群 - No LINQ
        private void button4_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            int[] nums = { 1, 2, 7, 8, 9, 10, 33, 55, 77, 88, 99, 100, 257, 692,780,859,998 };
            TreeNode node;
            foreach (int item in nums)
            {
              //  TreeNode node = this.treeView1.Nodes.Add("Small");
                if (item<100)
                {
                    if (treeView1.Nodes["Small"] == null)
                    {//                                                           ↓Key     ↓HeaderText
                         node = treeView1.Nodes.Add("small", "Small");
                        node.Nodes.Add(item.ToString());
                    }
                    else //treeView1.Nodes["Small"] != null
                    {
                        this.treeView1.Nodes["Small"].Nodes.Add(item.ToString());
                        this.treeView1.Nodes["Small"].Text =$"Small({this.treeView1.Nodes["Small"].Nodes.Count})";
                    } 
                }
                else if (item<300)
                {
                    if(treeView1.Nodes["Medium"]==null)
                    {
                        node = this.treeView1.Nodes.Add("medium","Medium");
                        node.Nodes.Add(item.ToString());
                    }
                    else
                    {
                        this.treeView1.Nodes["Medium"].Nodes.Add(item.ToString());
                        this.treeView1.Nodes["Medium"].Text = $"Medium({this.treeView1.Nodes["Medium"].Nodes.Count})";
                    }
                }
                else
                {
                    if(this.treeView1.Nodes["Large"]==null)
                    {
                        node = this.treeView1.Nodes.Add("large","Large");
                        node.Nodes.Add(item.ToString());
                    }
                    else
                    {
                        this.treeView1.Nodes["Large"].Nodes.Add(item.ToString());
                        this.treeView1.Nodes["Large"].Text = $"Large({this.treeView1.Nodes["Large"].Nodes.Count})";
                    }
                }
            }


        }



        //依 檔案大小 分組檔案 (大中小)
        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    group f by FileSize(f.Length) into g
                    orderby g.Key descending
                    select new
                    {
                        Filesize = g.Key,
                        SizeCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            //=======TreeView===============================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Filesize}({group.SizeCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyGroup.ToString(), s);  //疑問:s不能註解但為甚麼能夠單獨寫&與第一個參數一起寫
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

        }

        private string FileSize(long length)
        {
            if(length<50000)  //50K
            {
                return "Small";
            }
            else if(length<102401)  //100K
            {
                return "Medium";
            }
            else         //100Kup
            {
                return "Large";
            }
        }


        //  依 年 分組檔案 (大=>小)
        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    group f by MyYear(f.CreationTime.Year) into g
                    select new
                    {
                        MyYear = g.Key,
                        YearCount = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            //=======================================================
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.MyYear}({group.YearCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyGroup.ToString(),s);
                foreach(var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

        }

        private string MyYear(int year)
        {
            if(year<2020)
            {
                return "Old";
            }
            else if (year<2021)
            {
                return "Medium";
            }
            else
            {
                return "New";
            }
        }

        NorthwindEntities nwEdb = new NorthwindEntities();
        //NW Products 低中高 價產品 
        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwEdb.Products.AsEnumerable()
                    group p by PriceRange(p.UnitPrice.Value) into g
                    select new
                    {
                        PriceRange = g.Key,
                        ProductCount = g.Count(),
                        ProductGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();
            //=========TreeView================================================
            this.treeView1.Nodes.Clear();
            foreach(var group in q)
            {
                string s = $"{group.PriceRange}({group.ProductCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.PriceRange.ToString(), s);
                foreach(var item in group.ProductGroup)
                {
                    node.Nodes.Add(item.ProductName);
                }
            }

        }

        private string PriceRange(decimal unitPrice)
        {
            if(unitPrice<40)
            {
                return "Low";
            }
            else if(unitPrice<80)
            {
                return "Medium";
            }
            else
            {
                return "High";
            }
        }

        // Orders -  Group by 年
        private void button15_Click(object sender, EventArgs e)
        {
            var q = from o in nwEdb.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        OrdersCount = g.Count(),

                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        // Orders -  Group by 年 / 月
        private void button10_Click(object sender, EventArgs e)
        {
            var q = from o in nwEdb.Orders.AsEnumerable()
                        // 運算式樹狀架構不得包含元組常值
                    group o by new { o.OrderDate.Value.Year, o.OrderDate.Value.Month } into g
                    select new
                    {
                        Year_Month =g.Key,
                        Count = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        //總銷售金額?
        private void button2_Click(object sender, EventArgs e)
        {
            var q = from od in this.nwEdb.Order_Details
                    group od by od.Order.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        Total =g.Sum(od=>od.UnitPrice*od.Quantity)
                    };
            this.dataGridView1.DataSource = q.ToList();

        }

        //銷售最好的top 5業務員
        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from o in this.nwEdb.Order_Details.AsEnumerable()
                     group o by o.Order.EmployeeID into g
                     select new
                     {
                         EmployeeID = g.Key,
                         Sum = g.Sum(o => o.UnitPrice * o.Quantity)   //←這裡要的東西得視資料來源裡有的
                     }).OrderByDescending(g => g.Sum).Select(g=>new {g.EmployeeID,Total=$"{g.Sum:c2}" }).Take(5);
           // 　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　↑格式化後為字串
            this.dataGridView1.DataSource = q.ToList();
           
            //this.dataGridView1.DataSource = this.nwEdb.Order_Details.GroupBy(od => od.Order.EmployeeID)
            //    .SelectMany(od=>od.Orders(od,o));
        
        }

        //     NW 產品最高單價前 5 筆 (包括類別名稱)
        private void button9_Click(object sender, EventArgs e)
        {
            var q = (from p in this.nwEdb.Products
                     orderby p.UnitPrice descending
                     select new 
                     { 
                     p.ProductID,
                     p.ProductName,
                     p.UnitPrice,
                     p.Category.CategoryName
                     } ).Take(5);
            
            this.dataGridView1.DataSource = q.ToList();
            //this.dataGridView1.DataSource = this.nwEdb.Products.OrderByDescending(p => p.UnitPrice)
            //    .SelectMany(p => p.Category, (p, c) => new { p.ProductName, p.UnitPrice, c.??? }).Take(5);
        }

        //     NW 產品有任何一筆單價大於300 ?
        private void button7_Click(object sender, EventArgs e)
        {
           MessageBox.Show( this.nwEdb.Products.Any(p => p.UnitPrice > 300).ToString());
        }
    }
}

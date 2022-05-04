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
    }
}

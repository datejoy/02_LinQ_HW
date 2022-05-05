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

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                            new Student{ Name = "gaga", Class = "CS_103", Chi = 20, Eng = 73, Math = 0, Gender = "Female"},
                                            new Student{ Name = "rara", Class = "CS_103", Chi = 46, Eng = 56, Math = 90, Gender = "Male"},
                                            new Student{ Name = "uuu", Class = "CS_103", Chi = 81, Eng = 78, Math = 60, Gender = "Male"}

                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?		
            var q = from s in students_scores
                    group s by s.Class into g
                    select new
                    {
                        Class=g.Key,
                        ChiCount = g.Select(s => s.Chi).Count(),
                        EngCount = g.Select(s => s.Eng).Count(),
                        MathCount = g.Select(s=>s.Math).Count(),
                       // ClassCount=g.Key.Count() 為甚麼是6???
                    };


            //this.dataGridView1.DataSource = students_scores.ToList();
            this.dataGridView1.DataSource = q/*.Select( s=>new {Total=(s.ChiCount+s.EngCount+s.MathCount)*3 }).*/.ToList();

                    // 找出 前面三個 的學員所有科目成績					
                    // 找出 後面兩個 的學員所有科目成績					

                    // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

                    // 找出學員 'bbb' 的成績	                          

                    // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

                    // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
                    // 數學不及格 ... 是誰 
                    #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            var q = from s in students_scores
                    group s by s.Name into g
                    select new
                    {
                        Name = g.Key,
                        Sum = g.Sum(s => s.Chi + s.Eng + s.Math),
                        Avg = g.Average(s => (s.Chi + s.Eng + s.Math) / 3),
                        //Min = g.Min(),
                        //Max = g.Max()
                    };
            this.dataGridView1.DataSource = q.ToList();


            //////各科 sum, min, max, avg
            //var q1 = from s in students_scores
            //         group s by 
            //         select new
            //         {

            //         };
        }

        int ComparScore(int[]scores)
        {
            foreach(int item in scores)
            {

            }
            return 3;
            //if (chi > eng && chi > math)
            //{
            //    return chi;
            //}
            //else if (eng > chi && eng > math)
            //{
            //    return eng;
            //}
            //else if (math > chi && math > eng)
            //{
            //    return math;
            //}
        }


        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }

        NorthwindEntities nwe = new NorthwindEntities();
        private void button34_Click(object sender, EventArgs e)
        {
            clearChart();
            #region// 年度??/最高銷售金額 年度最低銷售金額 (單筆???最高/最低)
            var q2 = from o in this.nwe.Order_Details
                      group o by  o.Order.OrderDate.Value.Year  into g
                      select new                                             //↑先用年分↓
                      {//Question: 要如何同時顯示最高跟最低                ↓
                          Year = g.Key,           //再用年之下的ID groupby，再選年裡的ID的金額的最大最小值
                          Min = g.GroupBy(o=>o.OrderID).Select(o=>o.Sum(od=>od.Quantity*od.UnitPrice)).Min(),
                          Max = g.GroupBy(o=>o.OrderID).Select(o=>o.Sum(od=>od.Quantity*od.UnitPrice)).Max()
                      };
            this.chart3.DataSource = q2.ToList();
            this.chart3.Series[0].XValueMember = "Year";
            this.chart3.Series[0].YValueMembers = "Min";
            this.chart3.Series[1].XValueMember = "Year";
            this.chart3.Series[1].YValueMembers = "Max";
            this.chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart3.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            #endregion

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            label1.Text += "哪一年總銷售最好 ? A:1997\n那一年總銷售最不好 ?  A:1996\n";
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            label1.Text += "那一個月總銷售最好 ? A:4月  \n那一個月總銷售最不好 ? A:6月\n ";

            #region// 每年 總銷售分析 圖
            var q = from o in this.nwe.Order_Details
                    group o by o.Order.OrderDate.Value.Year into g
                    select new
                    {
                        Years = g.Key,
                        Total = g.Sum(o => o.Quantity * o.UnitPrice)
                    };
            //this.dataGridView1.DataSource = q.ToList();
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "Years";
            this.chart1.Series[0].YValueMembers = "Total";
            this.chart1.Series[0].Name = "Year";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            #endregion

            #region// 每月 總銷售分析 圖
            var q1 = from o in this.nwe.Order_Details
                     group o by o.Order.OrderDate.Value.Month into g
                     select new
                     {
                         Months = g.Key,
                         Total = g.Sum(o => o.Quantity * o.UnitPrice)
                     };
            this.chart2.DataSource = q1.ToList();
            this.chart2.Series[0].XValueMember = "Months";
            this.chart2.Series[0].YValueMembers = "Total";
            this.chart2.Series[0].Name = "Month";
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            #endregion
        }

        void clearChart()
        {
            this.chart1.DataSource = null;
            this.chart2.DataSource = null;
            this.chart3.DataSource = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}

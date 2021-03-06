﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.Collections;
namespace radarsystem
{
    public partial class Form1 : Form
    {
        //自适应窗口类
        AutoSizeFormClass autoForm = new AutoSizeFormClass();

        List<PointD> list = new List<PointD>();
        List<Point> list_trace = new List<Point>();
        Point screenpoint_pic4;
        private bool isDragging = false; //拖中
        private int currentX = 0, currentY = 0; //原来鼠标X,Y坐标
        bool flag_thread2 = false;
        bool flag_thread1 = false;
        Thread t2;
        Thread t1;
     //用pictureBox4 的左上角坐标表示雷达的中心点坐标
     //   ArrayList AL = new ArrayList();
        public class PointD
        {
            public double X;
            public double Y;
        }
        public Form1()
        {
            InitializeComponent();
            textBox_doppler.Visible = false;
            button_goback.Visible = false;
            pictureBox4.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            autoForm.controllInitializeSize(this);
            //MessageBox.Show("ha");
            string ConStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;
                            Data source="+Application.StartupPath+"\\database\\whut\\RecognitionAid.mdb");
            //MessageBox.Show(ConStr);
            OleDbConnection oleCon = new OleDbConnection(ConStr);
            OleDbDataAdapter oleDap = new OleDbDataAdapter("select * from TargetTrailPoints", oleCon);
            DataSet ds = new DataSet();
            oleDap.Fill(ds, "目标轨迹");
           
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)          //循环取出ds.table中的值
            {

                PointD s = new PointD();                  // 实例化Point对象
                s.X= Convert.ToDouble(ds.Tables[0].Rows[i]["X"]);
                s.Y = Convert.ToDouble(ds.Tables[0].Rows[i]["Y"]);            

                list.Add(s);    // 将取出的对象保存在LIST中  以上是获得值。


            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)          //循环取出ds.table中的值
            {

                Point s = new Point();                  // 实例化Point对象
                s.X = Convert.ToInt32(ds.Tables[0].Rows[i]["X"]);
                s.Y = Convert.ToInt32(ds.Tables[0].Rows[i]["Y"]);

                list_trace.Add(s);    // 将取出的对象保存在LIST中  以上是获得值。


            }
            foreach (Point p in list_trace)
            {
                Console.WriteLine(p.X);
             
                Console.WriteLine(p.Y);
            }
            screenpoint_pic4 =PointToScreen(pictureBox4.Location);
            Console.WriteLine(screenpoint_pic4.X);
            Console.WriteLine(screenpoint_pic4.Y);
          //  list.ForEach()
            //     ds.
            // this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            oleCon.Close();
            oleCon.Dispose();

        }
        private void drawtrace()
        {
            //Thread t1 = new Thread(new ThreadStart(TestMethod));
            //t1.IsBackground = true;
            //if(!t1.IsAlive)
            //    t1.Start();
            if (!flag_thread1)
            {
                //  t2.Abort();
                //   t2 = new Thread(new ThreadStart(thread2));
                //  t2 = new Thread(new ThreadStart(thread2));
                //  t2.IsBackground = true;
                t1 = new Thread(new ThreadStart(TestMethod));
                t1.IsBackground = true;
                t1.Start();
                flag_thread1 = true;
            }
            else
            {

                t1.Abort();
                t1 = new Thread(new ThreadStart(TestMethod));
                t1.IsBackground = true;
                t1.Start();
            }
            //   t2.Start();

           
        }
        public void TestMethod()
        {
            Graphics g;
            
            g =pictureBox3.CreateGraphics();
    //        g.s
            Pen p = new Pen(Color.Red, 2);
            //Point[] p1 = new Point[100];
            //Random ran = new Random();
            //p1[0].X = 70;
            //p1[0].Y = 70;
            //for (int i = 1; i < 100; i++)
            //{
            //    p1[i].X = ran.Next(p1[i - 1].X - 50, p1[i - 1].X + 50);
            //    p1[i].Y = ran.Next(p1[i - 1].Y - 50, p1[i - 1].Y + 50);
            //}
            //Point one, two;
            //for (int i = 0; i < 99; i++)
            //{
            //    one = p1[i];
            //    two = p1[i + 1];
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //    g.DrawLine(p, one, two);
            //    System.Threading.Thread.Sleep(500);
            //}
            //foreach (PointD p1 in AL)
            //{
            //    Console.WriteLine(p1.X);

            //    Console.WriteLine(p1.Y);
            //}
            Point one, two;
            for (int i = 0; i < list_trace.Count-1; i++)
            {
                one = list_trace[i];
                two = list_trace[i + 1];
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(p, one, two);
                System.Threading.Thread.Sleep(200);
            }
            //for(int pos=0;pos<list.Count;pos++)
            //{
            //}
            g.Dispose();
        }
        private void draw_monitor_trace()
        {
           // Graphics g;
           // Pen p = new Pen(Color.Red, 2);
           // g = panel1.CreateGraphics();
           // Point point;
           // Point point_diff;
           // Point cir_Point = new Point(0, 0);
           // Point one = new Point(0, 0);
           // Point two = new Point(0, 0);
           // cir_Point.X = panel1.Width / 10 * 5;
           // cir_Point.Y = panel1.Height / 10 * 5;
           // //  point_diff.X = 300;
           // //  point_diff.Y = 300;
           // //point.X=pictureBox4.Top;
           // //MessageBox.Show("3");
        
           // for (int i = 0; i < list_trace.Count - 1; i++)
           // {
           //     point = list_trace[i];
           //     point_diff = point;
           //     point_diff.X = point.X - pictureBox4.Left;
           //     point_diff.Y = point.Y - pictureBox4.Top;
           //     SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Red);//画刷
           //     g.FillEllipse(myBrush, new Rectangle(cir_Point.X + point_diff.X - 3, cir_Point.Y + point_diff.Y - 3, 3, 3));//画实心椭圆
           //     //    g.DrawLine(new Pen(Color.Red), point_diff.X, point_diff.Y, point_diff.X, point_diff.Y);
           //     //    g.DrawLine(new Pen(Color.Red), 200, 200,210, 210);
           //     one.X = cir_Point.X + point_diff.X;
           //     one.Y = cir_Point.Y + point_diff.Y;
           //     two.X = list_trace[i + 1].X - pictureBox4.Left + cir_Point.X;
           //     two.Y = list_trace[i + 1].Y - pictureBox4.Top + cir_Point.Y;
           //     g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
           //     g.DrawLine(p, one, two);
           ////     System.Threading.Thread.Sleep(500);
           // }
           
            if (!flag_thread2) 
            {
               //  t2.Abort();
              //   t2 = new Thread(new ThreadStart(thread2));
              //  t2 = new Thread(new ThreadStart(thread2));
              //  t2.IsBackground = true;
                t2 = new Thread(new ThreadStart(thread2));
                t2.IsBackground = true;
                t2.Start();
                flag_thread2 = true;
            }
            else
            {
                
                t2.Abort();
                t2 = new Thread(new ThreadStart(thread2));
                t2.IsBackground = true;
                t2.Start();
            }
             //   t2.Start();


        }
        private void thread2()
        {
            Graphics g;
            Pen p = new Pen(Color.Red, 2);
            g = panel1.CreateGraphics();
            Point point;
            Point point_diff;
            Point cir_Point = new Point(0, 0);
            Point one = new Point(0, 0);
            Point two = new Point(0, 0);
            cir_Point.X = panel1.Width / 10 * 5;
            cir_Point.Y = panel1.Height / 10 * 5;
            //  point_diff.X = 300;
            //  point_diff.Y = 300;
            //point.X=pictureBox4.Top;
            //MessageBox.Show("3");
            for (int i = 0; i < 20; i++)
            {
                //g.DrawString();
            }
            for (int i = 0; i < list_trace.Count - 1; i++)
            {
                point = list_trace[i];
                point_diff = point;
                point_diff.X = point.X - pictureBox4.Left;
                point_diff.Y = point.Y - pictureBox4.Top;
                SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Red);//画刷
                g.FillEllipse(myBrush, new Rectangle(cir_Point.X + point_diff.X - 3, cir_Point.Y + point_diff.Y - 3, 3, 3));//画实心椭圆
                //    g.DrawLine(new Pen(Color.Red), point_diff.X, point_diff.Y, point_diff.X, point_diff.Y);
                //    g.DrawLine(new Pen(Color.Red), 200, 200,210, 210);
                one.X = cir_Point.X + point_diff.X;
                one.Y = cir_Point.Y + point_diff.Y;
                two.X = list_trace[i + 1].X - pictureBox4.Left + cir_Point.X;
                two.Y = list_trace[i + 1].Y - pictureBox4.Top + cir_Point.Y;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(p, one, two);
                System.Threading.Thread.Sleep(400);
            }
        }
        private void checkedListBox_radartype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox_radartype.GetItemChecked(0))
            {
                label_sel_radartype.Text = "多普勒雷达";
                checkedListBox_radartype.Hide();
                textBox_doppler.Visible=true;
                textBox_doppler.Text="检测范围\r\n\r\n距离精度\r\n\r\n目标速度\r\n\r\n速度精度";
                button_goback.Visible = true;
                //pictureBox4.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\..\\..\\..\\radarsystem\\Resources\\多普勒雷达.jpg");
                pictureBox4.BackgroundImage = global::radarsystem.Properties.Resources.duopule;
                pictureBox4.Visible = true;
                drawtrace();
            //    draw_monitor_trace();
           //     PaintEventArgs pe = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
            //    pictureBox3_Paint(sender,pe);
             
            }
            if (checkedListBox_radartype.GetItemChecked(1))
            {
                label_sel_radartype.Text = "多基地雷达";
                checkedListBox_radartype.Hide();
                textBox_doppler.Visible = true;
                textBox_doppler.Text = "检测范围\r\n\r\n距离精度\r\n\r\n目标速度\r\n\r\n速度精度";
                button_goback.Visible = true;
            }
        }

        private void button_goback_Click(object sender, EventArgs e)
        {
            label_sel_radartype.Text = "雷达类型选择";
            checkedListBox_radartype.Show();
            textBox_doppler.Visible = false;
            button_goback.Visible = false;
        }

        private void clb_setMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            strCollected = string.Empty;
            for (i = 0; i < clb_setMod.Items.Count; i++)
            {
                if (clb_setMod.GetItemChecked(i))
                {
                    if (strCollected == string.Empty)
                    {
                        strCollected = clb_setMod.GetItemText(clb_setMod.Items[i]);
                    }
                    else
                    {
                        strCollected = strCollected + "，" + clb_setMod.GetItemText(clb_setMod.Items[i]);
                    }
                }
            }
        }

        private void btn_Finish_Click(object sender, EventArgs e)
        {
            if (strCollected == string.Empty)
                MessageBox.Show("您未添加任何噪声！");
            else MessageBox.Show("您选择添加了" + strCollected, "提示");
        }

        private Dictionary<String,double> getTimeAndSpaceFeature(int count)
        {
            Dictionary<String,double> featDic = new Dictionary<String,double>();
            //计算时域空域特征分析
            double[] features = new double[count];

            PointD[] p1 = new PointD[list.Count];
            for (int i=0;i<list.Count;i++)
            {
                p1[i] = list[i];
            }


            double[] pX = new double[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                pX[i] = p1[i].X;
            }
            //算术平均值,arithmetic mean value
            for (int i = 0; i < list.Count; i++)
            {
                features[0] += p1[i].X;
            }
            features[0] /= list.Count;
            featDic.Add("算术平均值", features[0]);

            //几何平均值,geometric mean
            features[1] = 1.0;
            for (int i = 0; i < list.Count; i++)
            {
                if (p1[i].X != 0)
                    features[1] *= p1[i].X;

            }
            features[1] = Math.Pow(features[1], 1 / list.Count);
            featDic.Add("几何平均值", features[1]);


            //均方根值,root mean square value
            for (int i = 0; i < list.Count; i++)
            {
                features[2] += Math.Pow(p1[i].X, 2);
            }
            features[2] /= list.Count;
            features[2] = Math.Pow(features[2], 1 / 2);
            featDic.Add("均方根值", features[2]);

                //方差, variance
            for (int i = 0; i < list.Count; i++)
            {
                features[3] += Math.Pow(p1[i].X - features[0],2);
            }
            features[3] /= list.Count-1;
            featDic.Add("方差", features[3]);

                //标准差,standard deviation
            for (int i = 0; i < list.Count; i++)
            {
                features[4] += Math.Pow(p1[i].X - features[0], 2);
            }
            features[4] /= list.Count;
            features[4] = Math.Pow(features[4], 1 / 2);
            featDic.Add("标准差", features[4]);

                //波形指标,waveform indicators
            features[5] = featDic["均方根值"] / Math.Abs(featDic["算术平均值"]);
            featDic.Add("波形指标", features[5]);

                //峰值指标,peak index
            features[6] = pX.Max() / featDic["均方根值"];
            featDic["峰值指标"] = features[6];

                //脉冲指标,pulse factor
            features[7] = pX.Max() / Math.Abs(featDic["算术平均值"]);
            featDic["脉冲指标"] = features[7];

                //方根幅值,root amplitude
            for (int i = 0; i < list.Count; i++)
            {
                features[8] += Math.Pow(Math.Abs(p1[i].X), 1 / 2);
            }
            features[8] = Math.Pow(features[8] / list.Count, 2);
            featDic["方根幅值"] = features[8];

                //裕度指标,margin indicator
            features[9] = pX.Max() / featDic["方根幅值"];
            featDic["裕度指标"] = features[9];

                //峭度指标,Kurosis amplitude
            for (int i = 0; i < list.Count; i++)
            {
                features[10] += Math.Pow(p1[i].X, 4);
            }
            features[10] /= list.Count;
            features[10] = features[10] / Math.Pow(featDic["均方根值"], 4);
            featDic["峭度指标"] = features[10];

                //自相关函数,m=2,autocorrelation
            for (int i = 0; i < (list.Count-2); i++)
            {
                features[11] += p1[i].X * p1[i + 2].X;
            }
            featDic["自相关函数"] = features[11];

                //互相关函数,cross-correlation
            for (int i = 0; i < (list.Count-2); i++)
            {
                features[12] += p1[i].X * p1[i + 2].Y;
            }
            featDic["互相关函数"] = features[12];

                return featDic;
        }

        private void featurecomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int showIndex = featurecomboBox1.SelectedIndex;
            if (showIndex == 0)
            {
                //当前选中了时域和空域特征分析
                Dictionary<String, double> featDic = getTimeAndSpaceFeature(13);
                String[] featName = new String[13];
                int i = 0;
                foreach (String key in featDic.Keys)
                {
                    featName[i++] = key;
                }

                featurelistView.BeginUpdate();
                
                
                featurelistView.Clear();
                ColumnHeader header1 = new ColumnHeader();
                header1.Text = "算法";
                ColumnHeader header2 = new ColumnHeader();
                header2.Text = "数值分析";

                featurelistView.Columns.AddRange(new ColumnHeader[] { header1, header2 });
                featurelistView.FullRowSelect = true;
                //listview 中添加数据

                for (i = 0; i < 13; i++)
                {
                    featurelistView.Items.Add("" + featName[i]);
                    ListViewItem listItem = new ListViewItem();
                    listItem.SubItems.Add(""+featDic[featName[i]]);
                    featurelistView.Items[i].SubItems.Add("" + featDic[featName[i]]);
                    

                }

                featurelistView.View = System.Windows.Forms.View.Details;
                featurelistView.GridLines = true;
                featurelistView.EndUpdate();


            }
            else if(showIndex == 1)
            {
                //当前选中了频域特征分析
               MessageBox.Show("频域特征分析未实现", "hints");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //创建画板
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1/2);
            //g.DrawLine(pen, 0, 0, 335, 0);

            int factor = panel1.Width/10;
            for ( int i = 0; i < 10; i++)
            {
                //画水平线
                g.DrawLine(pen, 0, i*factor, panel1.Width, i*factor);
                //画竖直线
                g.DrawLine(pen, i*factor, 0, factor*i, panel1.Height);
            }

            //画圆
            for(int j = 1;j<5;j++)
            {
                g.DrawEllipse(pen, 4*factor-(j-1)*factor, 4*factor-(j-1)*factor, j * 2*factor, j * 2*factor);
                //g.DrawEllipse(panel1.Width/10*4,)
            }
           // timer1.Start();
        }
        int degress = 0;
        float angle = 0;
        int x2 = 115, y2 = 0;
        double r = 50;
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                Pen pen = new Pen(Color.Black, 1 / 2);
                g.DrawLine(pen, 115, 115, x2, y2);
                degress += 10;
                angle = (float)(Math.PI * degress / 180.0);
                x2 = (int)(115 - r + Math.Cos(angle) * r);
                y2 = (int)(0-Math.Sin(angle)*r);
                //g.RotateTransform(angle);
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = panel2.CreateGraphics())
            {
                Pen pen = new Pen(Color.Black, 1 / 2);
                for (int j = 1; j < 5; j++)
                {
                    g.DrawEllipse(pen, 25 - (j - 1) * 8, 25 - (j - 1) * 8, j * 16, j * 16);
                }
            }
        }

        private void OnDargDrop(object sender, DragEventArgs e) //拖动雷达时候产生该事件
        {
           // pictureBox4.
            MessageBox.Show("ga");
            screenpoint_pic4 = PointToScreen(pictureBox4.Location);
            Console.WriteLine(screenpoint_pic4.X);

        }
    //    private bool isDragging = false; //拖中
    //    private int currentX = 0, currentY = 0; //原来鼠标X,Y坐标
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;  //可以拖动
            currentX = e.X;
            currentY = e.Y;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
          //  MessageBox.Show("ga");
            if (isDragging)
            {
                pictureBox4.Top = pictureBox4.Top + (e.Y - currentY);
                pictureBox4.Left = pictureBox4.Left + (e.X - currentX);
            }
            isDragging = false;
      //      screenpoint_pic4 = PointToScreen(pictureBox4.Location);
      //      Console.WriteLine(pictureBox4.Top);  
       //     Console.WriteLine(screenpoint_pic4.Y);
       //     Console.WriteLine(screenpoint_pic4.X);
           
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            isDragging = true;  //可以拖动
          //  if (isDragging)
          //  {
          //      pictureBox4.Top = pictureBox1.Top + (e.Y - currentY);
           //     pictureBox4.Left = pictureBox1.Left + (e.X - currentX);
          //  }
        }

        private void Feature_SelectedIndexChanged(object sender, EventArgs e)
        {
            //flag_thread2 = 1;
            //Control ctrl=tabControl1.GetControl(2);
            if (tabControl1.SelectedIndex == 2)
                draw_monitor_trace();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            autoForm.controlAutoSize(this);
        }

        private void Xpanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Xpanel.CreateGraphics();
            double start = -1;
            int sLoc = 0;
            int addition = 40;
            for (int i = 0; i < 11; i++)
            {
                g.DrawString((start + 0.2 * i).ToString(), new Font(FontFamily.GenericMonospace, 10f), Brushes.Black, new PointF(sLoc + addition * i, 0));
            }
        }

        private void Ypanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Ypanel.CreateGraphics();
            double start = 1;
            int sLoc = 0;
            int addition = 40;
            for (int i = 0; i < 11; i++)
            {
                g.DrawString((start - 0.2 * i).ToString(), new Font(FontFamily.GenericMonospace, 10f), Brushes.Black, new PointF(0, sLoc + addition * i));
            }
        }

        private void checkedListBox_radartype_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox_radartype.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox_radartype.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        this.checkedListBox_radartype.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
                    }
                }
            }  
        }

    

        //private void pictureBox3_Paint(object sender, PaintEventArgs e)
        //{
        //    drawtrace();
        //}
    }
}

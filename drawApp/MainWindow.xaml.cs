using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;


namespace drawApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbCanvas.Text = string.Format("1200 × 600 Pixel");
            tbThick.Text = string.Format("Stroke: 1 Pixel");
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
        }


        List<myGeometry> allGeometry = new List<myGeometry>();
        List<myGeometry> saveGeometry = new List<myGeometry>();
        GeometryType myGeomtryType = GeometryType.None;
        bool moving = false;

        #region shapes
        private void btPoint_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Point;
            tbStatus.Text = "Shape:" + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void btCircle_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Circle;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void btPolyline_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Polyline;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void btPolylgon_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Polygon;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void btEllipse_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Ellipse;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void btRectangle_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.Rectangle;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point tempPoint = e.GetPosition((IInputElement)sender);
            tbPosition.Text = tempPoint.X + "," + tempPoint.Y + " Pixel";
            //above: position
            Point movePoint = e.GetPosition((IInputElement)sender);

            myPoint plPoint = new myPoint();
            myPoint pgPoint = new myPoint();
            if (MyPL != null)
            {
                plPoint = MyPL.linePoints.Last();
                Point plPT = new Point();
                plPT.X = plPoint.x;
                plPT.Y = plPoint.y;
                firstPoint = plPT;
            }
            if (MyPG != null)
            {
                pgPoint = MyPG.gonPoints.Last();
                Point pgPT = new Point();
                pgPT.X = pgPoint.x;
                pgPT.Y = pgPoint.y;
                firstPoint = pgPT;
            }
            double addx = 0;
            double addy = 0;
            if ((movePoint.Y - firstPoint.Y) > 0 & (movePoint.X - firstPoint.X) > 0)
            {
                addx = 3;
                addy = 3;
            }
            else if ((movePoint.Y - firstPoint.Y) < 0 & (movePoint.X - firstPoint.X) < 0)
            {
                addx = -3;
                addy = -3;
            }
            else if ((movePoint.Y - firstPoint.Y) > 0 & (movePoint.X - firstPoint.X) < 0)
            {
                addx = -3;
                addy = 3;
            }
            else if ((movePoint.Y - firstPoint.Y) < 0 & (movePoint.X - firstPoint.X) > 0)
            {
                addx = 3;
                addy = -3;
            }


            myPoint moveMyPoint = new myPoint(movePoint.X - addx, movePoint.Y - addy);  //移动

            switch (myGeomtryType)
            {
                case GeometryType.None:
                    break;
                case GeometryType.Circle:
                    {

                        if (moving == true)
                        {
                            myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                            MyCircle = new myCircle(thispoint, moveMyPoint);
                            MyCircle.getColor(rvalue, gvalue, bvalue);
                            MyCircle.getThickness(mythickness);
                            MyCircle.myDraw(drawCanvas);
                            if (drawCanvas.Children.Count >= allGeometry.Count + 2)
                            {
                                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                            }
                        }

                    }
                    break;
                case GeometryType.Polyline:
                    {
                        if (moving == true && MyPL.linePoints.Count > 0)
                        {
                            MyPL.getColor(rvalue, gvalue, bvalue);
                            MyPL.getThickness(mythickness);
                            MyPL.myDraw(drawCanvas);
                            MyPL.AddPoint(moveMyPoint);
                            if (MyPL.linePoints.Count != pointcount && MyPL.linePoints.Count > 2)
                            {
                                MyPL.linePoints.RemoveAt(MyPL.linePoints.Count - 2);
                            }
                            if (drawCanvas.Children.Count >= allGeometry.Count + 2)
                            {
                                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                            }
                        }
                    }
                    break;
                case GeometryType.Polygon:
                    {
                        if (moving == true && MyPG.gonPoints.Count > 0)
                        {
                            MyPG.getColor(rvalue, gvalue, bvalue);
                            MyPG.getThickness(mythickness);
                            MyPG.myDraw(drawCanvas);
                            MyPG.AddPoint(moveMyPoint);
                            if (MyPG.gonPoints.Count != pointcount && MyPG.gonPoints.Count > 2)
                            {
                                MyPG.gonPoints.RemoveAt(MyPG.gonPoints.Count - 2);
                            }
                            if (drawCanvas.Children.Count >= allGeometry.Count + 2)
                            {
                                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                            }
                        }
                    }
                    break;
                case GeometryType.Ellipse:
                    {
                        if (moving == true)
                        {
                            myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                            MyEllipse = new myEllipse(thispoint, moveMyPoint);
                            MyEllipse.getColor(rvalue, gvalue, bvalue);
                            MyEllipse.getThickness(mythickness);
                            MyEllipse.myDraw(drawCanvas);
                            if (drawCanvas.Children.Count >= allGeometry.Count + 2)
                            {
                                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                            }
                        }
                    }
                    break;
                case GeometryType.Rectangle:
                    {
                        if (moving == true)
                        {
                            myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                            MyRectangle = new myRectangle(thispoint, moveMyPoint);
                            MyRectangle.getColor(rvalue, gvalue, bvalue);
                            MyRectangle.getThickness(mythickness);
                            MyRectangle.myDraw(drawCanvas);
                            if (drawCanvas.Children.Count >= allGeometry.Count + 2)
                            {
                                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                            }
                        }
                    }
                    break;
            }
            if (select == true & noway == true & moving == false)
            {
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                if ((movePoint.Y - selectRectY) > 0 & (movePoint.X - selectRectX) > 0)
                {
                    Canvas.SetLeft(selectRect, selectRectX);
                    Canvas.SetTop(selectRect, selectRectY);
                    selectRect.Height = Math.Abs(movePoint.Y - selectRectY);
                    selectRect.Width = Math.Abs(movePoint.X - selectRectX);
                    int a = (int)selectRectX;
                    int b = (int)selectRect.Width;
                    int c = (int)selectRectY;
                    int d = (int)selectRect.Height;
                    foreach (var Geo in allGeometry)
                    {
                        //if (Geo is myPoint)
                        if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                        {
                            Geo.getColorA(100);
                        }
                        else
                        {
                            Geo.getColorA(255);
                        }
                    }

                }

                else if ((movePoint.Y - selectRectY) < 0 & (movePoint.X - selectRectX) < 0)
                {
                    Canvas.SetLeft(selectRect, movePoint.X);
                    Canvas.SetTop(selectRect, movePoint.Y);
                    selectRect.Height = Math.Abs(movePoint.Y - selectRectY);
                    selectRect.Width = Math.Abs(movePoint.X - selectRectX);
                    int a = (int)selectRectX;
                    int b = (int)selectRect.Width;
                    int c = (int)selectRectY;
                    int d = (int)selectRect.Height;
                    foreach (var Geo in allGeometry)
                    {
                        //if (Geo is myPoint)

                        if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                        {
                            Geo.getColorA(100);
                        }
                        else
                        {
                            Geo.getColorA(255);
                        }
                    }
                }
                else if ((movePoint.Y - selectRectY) > 0 & (movePoint.X - selectRectX) < 0)
                {
                    Canvas.SetLeft(selectRect, movePoint.X);
                    Canvas.SetTop(selectRect, selectRectY);
                    selectRect.Height = Math.Abs(movePoint.Y - selectRectY);
                    selectRect.Width = Math.Abs(movePoint.X - selectRectX);
                    int a = (int)selectRectX;
                    int b = (int)selectRect.Width;
                    int c = (int)selectRectY;
                    int d = (int)selectRect.Height;
                    foreach (var Geo in allGeometry)
                    {
                        //if (Geo is myPoint)

                        if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                        {
                            Geo.getColorA(100);
                        }
                        else
                        {
                            Geo.getColorA(255);
                        }
                    }
                }
                else if ((movePoint.Y - selectRectY) < 0 & (movePoint.X - selectRectX) > 0)
                {
                    Canvas.SetLeft(selectRect, selectRectX);
                    Canvas.SetTop(selectRect, movePoint.Y);
                    selectRect.Height = Math.Abs(movePoint.Y - selectRectY);
                    selectRect.Width = Math.Abs(movePoint.X - selectRectX);
                    int a = (int)selectRectX;
                    int b = (int)selectRect.Width;
                    int c = (int)selectRectY;
                    int d = (int)selectRect.Height;
                    foreach (var Geo in allGeometry)
                    {
                        //if (Geo is myPoint)

                        if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                        {
                            Geo.getColorA(100);
                        }
                        else
                        {
                            Geo.getColorA(255);
                        }
                    }
                }
                drawCanvas.Children.Add(selectRect);
            }
        }

        int pointcount = 1;
        int clickNum = 0;
        int selectNum = 0;
        Point firstPoint;
        myCircle MyCircle = null;
        myPolyline MyPL = null;
        myPolygon MyPG = null;
        myEllipse MyEllipse = null;
        myRectangle MyRectangle = null;

        double selectRectX;
        double selectRectY;

        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point tempPoint = e.GetPosition((IInputElement)sender);
            myPoint tempMyPoint = new myPoint();
            tempMyPoint.x = tempPoint.X;
            tempMyPoint.y = tempPoint.Y;
            switch (myGeomtryType)
            {
                case GeometryType.None:
                    break;
                case GeometryType.Point:
                    if (e.ClickCount == 1)
                    {
                        myPoint pt = new myPoint(tempMyPoint.x, tempMyPoint.y);
                        pt.getColor(rvalue, gvalue, bvalue);
                        pt.getThickness(mythickness);
                        allGeometry.Add(pt);
                        saveGeometry.Add(pt);
                        pt.myDraw(drawCanvas);
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                    }
                    //drawCanvas.Cursor = Cursors.Arrow;
                    break;
                case GeometryType.Circle:
                    if (clickNum == 0)
                    {
                        firstPoint = tempPoint;
                        clickNum++;
                        moving = true;
                    }
                    else
                    {
                        myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                        MyCircle = new myCircle(thispoint, tempMyPoint);
                        MyCircle.getColor(rvalue, gvalue, bvalue);
                        MyCircle.getThickness(mythickness);
                        allGeometry.Add(MyCircle);
                        saveGeometry.Add(MyCircle);
                        MyCircle.myDraw(drawCanvas);
                        moving = false;
                        clickNum = 0;
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                        //drawCanvas.Cursor = Cursors.Arrow;
                    }
                    break;
                case GeometryType.Polyline:
                    if (MyPL == null)
                    {
                        MyPL = new myPolyline();
                        MyPL.AddPoint(tempMyPoint);
                        moving = true;
                    }
                    else if (e.ClickCount >= 2)
                    {
                        MyPL.linePoints.RemoveAt(MyPL.linePoints.Count - 1);
                        MyPL.getColor(rvalue, gvalue, bvalue);
                        MyPL.getThickness(mythickness);
                        allGeometry.Add(MyPL);
                        saveGeometry.Add(MyPL);
                        MyPL.myDraw(drawCanvas);
                        moving = false;
                        MyPL = null;
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                        pointcount = 1;
                        //drawCanvas.Cursor = Cursors.Arrow;
                    }
                    else
                    {
                        pointcount++;
                        MyPL.AddPoint(tempMyPoint);
                    }
                    break;
                case GeometryType.Polygon:
                    if (MyPG == null)
                    {
                        MyPG = new myPolygon();
                        MyPG.AddPoint(tempMyPoint);
                        moving = true;
                    }
                    else if (e.ClickCount >= 2)
                    {
                        MyPG.gonPoints.RemoveAt(MyPG.gonPoints.Count - 1);
                        MyPG.getColor(rvalue, gvalue, bvalue);
                        MyPG.getThickness(mythickness);
                        allGeometry.Add(MyPG);
                        saveGeometry.Add(MyPG);
                        MyPG.myDraw(drawCanvas);
                        moving = false;
                        MyPG = null;
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                        pointcount = 1;
                        //drawCanvas.Cursor = Cursors.Arrow;
                    }
                    else
                    {
                        pointcount++;
                        MyPG.AddPoint(tempMyPoint);
                    }
                    break;
                case GeometryType.Ellipse:
                    if (clickNum == 0)
                    {
                        firstPoint = tempPoint;
                        clickNum++;
                        moving = true;
                    }
                    else
                    {
                        myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                        MyEllipse = new myEllipse(thispoint, tempMyPoint);
                        MyEllipse.getColor(rvalue, gvalue, bvalue);
                        MyEllipse.getThickness(mythickness);
                        allGeometry.Add(MyEllipse);
                        saveGeometry.Add(MyEllipse);
                        MyEllipse.myDraw(drawCanvas);
                        moving = false;
                        clickNum = 0;
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                        //drawCanvas.Cursor = Cursors.Arrow;
                    }
                    break;
                case GeometryType.Rectangle:
                    if (clickNum == 0)
                    {
                        firstPoint = tempPoint;
                        clickNum++;
                        moving = true;
                    }
                    else
                    {
                        myPoint thispoint = new myPoint(firstPoint.X, firstPoint.Y);    //the first point is not myPoint
                        MyRectangle = new myRectangle(thispoint, tempMyPoint);
                        MyRectangle.getColor(rvalue, gvalue, bvalue);
                        MyRectangle.getThickness(mythickness);
                        allGeometry.Add(MyRectangle);
                        saveGeometry.Add(MyRectangle);
                        MyRectangle.myDraw(drawCanvas);
                        moving = false;
                        clickNum = 0;
                        drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 2);
                        //drawCanvas.Cursor = Cursors.Arrow;
                    }
                    break;
            }
            if (select == false & noway == true)
            {
                selectNum++;
                select = true;
                selectRect = new Rectangle();
                DoubleCollection dc = new DoubleCollection();
                dc.Add(3);
                selectRect.StrokeDashArray = dc;
                selectRect.StrokeThickness = 2;
                SolidColorBrush rect = new SolidColorBrush();
                rect.Color = Colors.Cyan;
                selectRect.Stroke = rect;
                drawCanvas.Children.Add(selectRect);
                selectRectX = tempPoint.X;
                selectRectY = tempPoint.Y;
            }
            else if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.getColorA(255);
                    }
                }
            }
        }
        #endregion

        #region Exit & Stop & ClearCanvas &Info
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void menuStop_Click(object sender, RoutedEventArgs e)
        {
            myGeomtryType = GeometryType.None;
            drawCanvas.Cursor = Cursors.Arrow;
            tbStatus.Text = "Shape: " + myGeomtryType.ToString();
        }

        private void menuClear_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.Children.Clear();
            allGeometry.Clear();
            saveGeometry.Clear();
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Items.Clear();

        }

        private void btShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbInfo.Items.Clear();
                foreach (var geo in allGeometry)
                {
                    lbInfo.Items.Add(geo.info());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region color
        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
        byte rvalue;
        byte gvalue;
        byte bvalue;
        private void color1_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 0;
            gvalue = 0;
            bvalue = 0;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(0, 0, 0);
            colorbox.Fill = Brushes.Black;
        }

        private void color2_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 128;
            gvalue = 128;
            bvalue = 128;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(128, 128, 128);
            colorbox.Fill = Brushes.Gray;
        }

        private void color3_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 139;
            gvalue = 69;
            bvalue = 19;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(139, 69, 19);
            colorbox.Fill = Brushes.SaddleBrown;
        }

        private void color4_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 255;
            gvalue = 0;
            bvalue = 0;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
            colorbox.Fill = Brushes.Red;
        }

        private void color5_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 255;
            gvalue = 165;
            bvalue = 0;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(255, 165, 0);
            colorbox.Fill = Brushes.Orange;
        }

        private void color6_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 255;
            gvalue = 255;
            bvalue = 0;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(255, 255, 0);
            colorbox.Fill = Brushes.Yellow;
        }

        private void color7_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 173;
            gvalue = 255;
            bvalue = 47;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(173, 255, 47);
            colorbox.Fill = Brushes.GreenYellow;
        }

        private void color8_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 0;
            gvalue = 0;
            bvalue = 255;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
            colorbox.Fill = Brushes.Blue;
        }

        private void color9_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 128;
            gvalue = 0;
            bvalue = 128;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(128, 0, 128);
            colorbox.Fill = Brushes.Purple;
        }

        private void color10_Click(object sender, RoutedEventArgs e)
        {
            rvalue = 50;
            gvalue = 205;
            bvalue = 50;
            colorR.Value = rvalue;
            colorG.Value = gvalue;
            colorB.Value = bvalue;
            mySolidColorBrush.Color = Color.FromRgb(50, 205, 50);
            colorbox.Fill = Brushes.LimeGreen;
        }

        private void colorR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            rvalue = (byte)colorR.Value;
            mySolidColorBrush.Color = Color.FromRgb(rvalue, gvalue, bvalue);
            colorbox.Fill = mySolidColorBrush;
            if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                noway = true;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.getColor(rvalue, gvalue, bvalue);
                    }
                }
            }
        }

        private void colorG_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            gvalue = (byte)colorG.Value;
            mySolidColorBrush.Color = Color.FromRgb(rvalue, gvalue, bvalue);
            colorbox.Fill = mySolidColorBrush;
            if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                noway = true;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.getColor(rvalue, gvalue, bvalue);
                    }
                }
            }
        }

        private void colorB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bvalue = (byte)colorB.Value;
            mySolidColorBrush.Color = Color.FromRgb(rvalue, gvalue, bvalue);
            colorbox.Fill = mySolidColorBrush;
            if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                noway = true;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.getColor(rvalue, gvalue, bvalue);
                    }
                }
            }
        }
        #endregion

        #region thickness
        int mythickness = 1;
        private void thickness1_Click(object sender, RoutedEventArgs e)
        {
            mythickness = 1;
            tbThick.Text = "Stroke: 1 Pixel";
        }

        private void thickness2_Click(object sender, RoutedEventArgs e)
        {
            mythickness = 2;
            tbThick.Text = "Stroke: 2 Pixel";
        }

        private void thickness3_Click(object sender, RoutedEventArgs e)
        {
            mythickness = 3;
            tbThick.Text = "Stroke: 3 Pixel";
        }

        private void thickness4_Click(object sender, RoutedEventArgs e)
        {
            mythickness = 5;
            tbThick.Text = "Stroke: 5 Pixel";
        }
        #endregion

        #region sizechange
        TextBox tbwid = null;
        TextBox tbhei = null;
        Button btSize = null;
        Button btCancle = null;
        Window CanvasSize = null;

        private void menuCanvas_Click(object sender, RoutedEventArgs e)
        {
            CanvasSize = new Window();
            CanvasSize.Title = "Canvas Size";
            CanvasSize.Width = 400;
            CanvasSize.Height = 300;
            CanvasSize.MaxWidth = 400;
            CanvasSize.MaxHeight = 300;

            Grid myGrid = new Grid();
            //myGrid.ShowGridLines = true;
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            ColumnDefinition col4 = new ColumnDefinition();
            ColumnDefinition col5 = new ColumnDefinition();

            myGrid.RowDefinitions.Add(row1);
            myGrid.RowDefinitions.Add(row2);
            myGrid.RowDefinitions.Add(row3);
            myGrid.RowDefinitions.Add(row4);
            myGrid.RowDefinitions.Add(row5);

            myGrid.ColumnDefinitions.Add(col1);
            myGrid.ColumnDefinitions.Add(col2);
            myGrid.ColumnDefinitions.Add(col3);
            myGrid.ColumnDefinitions.Add(col4);
            myGrid.ColumnDefinitions.Add(col5);

            Label lbwid = new Label();
            lbwid.FontSize = 14;
            lbwid.Content = "Width";
            lbwid.VerticalContentAlignment = VerticalAlignment.Center;
            Label lbhei = new Label();
            lbhei.FontSize = 14;
            lbhei.Content = "Height";
            lbhei.VerticalContentAlignment = VerticalAlignment.Center;

            tbwid = new TextBox();
            tbwid.Text = "";
            tbwid.Height = 20;
            tbwid.Width = 60;
            tbhei = new TextBox();
            tbhei.Text = "";
            tbhei.Height = 20;
            tbhei.Width = 60;

            btSize = new Button();
            btSize.Click += btSize_Click;
            btSize.Content = "OK";
            btSize.Width = 70;
            btSize.Height = 30;

            btCancle = new Button();
            btCancle.Click += btCancle_Click;
            btCancle.Content = "Cancle";
            btCancle.Width = 70;
            btCancle.Height = 30;

            myGrid.Children.Add(lbwid);
            myGrid.Children.Add(lbhei);
            myGrid.Children.Add(tbwid);
            myGrid.Children.Add(tbhei);
            myGrid.Children.Add(btSize);
            myGrid.Children.Add(btCancle);

            Grid.SetRow(lbwid, 1);
            Grid.SetColumn(lbwid, 1);
            Grid.SetRow(lbhei, 2);
            Grid.SetColumn(lbhei, 1);
            Grid.SetRow(tbwid, 1);
            Grid.SetColumn(tbwid, 3);
            Grid.SetRow(tbhei, 2);
            Grid.SetColumn(tbhei, 3);
            Grid.SetRow(btSize, 3);
            Grid.SetColumn(btSize, 1);
            Grid.SetRow(btCancle, 3);
            Grid.SetColumn(btCancle, 3);

            CanvasSize.Content = myGrid;
            CanvasSize.WindowStartupLocation = WindowStartupLocation.CenterScreen; //
            CanvasSize.ShowDialog();
        }

        private void btSize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drawCanvas.Width = int.Parse(tbwid.Text);
                drawCanvas.Height = int.Parse(tbhei.Text);
                tbwid = null;
                tbhei = null;
                btSize = null;
                btCancle = null;
                CanvasSize.Close();
                CanvasSize = null;
                tbCanvas.Text = string.Format("{0} × {1} Pixel", drawCanvas.Width, drawCanvas.Height);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbwid = null;
                tbhei = null;
                btSize = null;
                btCancle = null;
                CanvasSize.Close();
                CanvasSize = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region write
        SaveFileDialog saveFile = new SaveFileDialog();
        OpenFileDialog openFile = new OpenFileDialog();
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            saveFile.Filter = "txt|*.txt|All Files|*.*";
            saveFile.FileOk += saveAllText;
            saveFile.ShowDialog();
        }

        private void saveAllText(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(saveFile.FileName))
            {
                sw.WriteLine("CanvasSize:" + drawCanvas.Width.ToString() + "," + drawCanvas.Height.ToString());
                foreach (myGeometry Geo in saveGeometry)
                {
                    sw.WriteLine(Geo.ToString());
                }
            }
        }
        #endregion

        #region read
        private void btRead_Click(object sender, RoutedEventArgs e)
        {
            openFile.Filter = "txt|*.txt|All Files|*.*";
            openFile.FileOk += readAllText;
            openFile.ShowDialog();
        }

        private void readAllText(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveGeometry.Clear();
            allGeometry.Clear();
            drawCanvas.Children.Clear();

            using (StreamReader sr = new StreamReader(openFile.FileName))
            {
                string templine = sr.ReadLine();
                while (templine != null)
                {
                    string[] part = templine.Split(':');
                    switch (part[0])
                    {
                        case "CanvasSize":
                            string[] numpart = part[1].Split(',');
                            drawCanvas.Width = int.Parse(numpart[0]);
                            drawCanvas.Height = int.Parse(numpart[1]);
                            tbCanvas.Text = string.Format("{0} × {1} Pixel", drawCanvas.Width, drawCanvas.Height);
                            break;
                        case "Point":
                            numpart = part[1].Split(',');
                            double x = double.Parse(numpart[0]);
                            double y = double.Parse(numpart[1]);

                            myPoint pt = new myPoint(x, y);
                            string[] color = part[2].Split(',');
                            byte r, g, b;
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            pt.pointSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            pt.thickness = int.Parse(part[3]);

                            allGeometry.Add(pt);
                            pt.myDraw(drawCanvas);
                            break;
                        case "Circle":
                            numpart = part[1].Split(',');
                            x = double.Parse(numpart[0]);
                            y = double.Parse(numpart[1]);

                            double tempWidth = int.Parse(numpart[2]) / 2;
                            double x1 = x + tempWidth;
                            double y1 = y + tempWidth;
                            myPoint pt1 = new myPoint(x, y);
                            myPoint pt2 = new myPoint(x1, y1);
                            myCircle cir = new myCircle(pt1, pt2);
                            color = part[2].Split(',');
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            cir.circleSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            cir.thickness = int.Parse(part[3]);

                            allGeometry.Add(cir);
                            cir.myDraw(drawCanvas);
                            break;
                        case "Ellipse":
                            numpart = part[1].Split(',');
                            x = double.Parse(numpart[0]);
                            y = double.Parse(numpart[1]);

                            double tempHeight = int.Parse(numpart[3]) / 2;
                            tempWidth = int.Parse(numpart[2]) / 2;
                            x1 = x + tempWidth;
                            y1 = y + tempHeight;
                            pt1 = new myPoint(x, y);
                            pt2 = new myPoint(x1, y1);
                            myEllipse elli = new myEllipse(pt1, pt2);
                            color = part[2].Split(',');
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            elli.EllipseSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            elli.thickness = int.Parse(part[3]);

                            allGeometry.Add(elli);
                            elli.myDraw(drawCanvas);
                            break;
                        case "Rectangle":
                            numpart = part[1].Split(',');
                            x = double.Parse(numpart[0]);
                            y = double.Parse(numpart[1]);

                            tempHeight = int.Parse(numpart[3]) / 2;
                            tempWidth = int.Parse(numpart[2]) / 2;
                            x1 = x + tempWidth;
                            y1 = y + tempHeight;
                            pt1 = new myPoint(x, y);
                            pt2 = new myPoint(x1, y1);
                            myRectangle rect = new myRectangle(pt1, pt2);
                            color = part[2].Split(',');
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            rect.RectSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            rect.thickness = int.Parse(part[3]);

                            allGeometry.Add(rect);
                            rect.myDraw(drawCanvas);
                            break;
                        case "Polyline":
                            color = part[1].Split(',');
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            readPolyline(sr);
                            tempReadPL.PLSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            tempReadPL.thickness = int.Parse(part[2]);

                            allGeometry.Add(tempReadPL);
                            tempReadPL.myDraw(drawCanvas);
                            tempReadPL = null;
                            break;
                        case "Polygon":
                            color = part[1].Split(',');
                            r = byte.Parse(color[0]);
                            g = byte.Parse(color[1]);
                            b = byte.Parse(color[2]);
                            readPolygon(sr);
                            tempReadPG.PGSolidColorBrush.Color = Color.FromRgb(r, g, b);
                            tempReadPG.thickness = int.Parse(part[2]);

                            allGeometry.Add(tempReadPG);
                            tempReadPG.myDraw(drawCanvas);
                            tempReadPG = null;
                            break;
                        default:
                            break;
                    }
                    templine = sr.ReadLine();
                }
            }
        }

        myPolyline tempReadPL = null;
        private void readPolyline(StreamReader sr)
        {
            tempReadPL = new myPolyline();
            string tempLine = sr.ReadLine();
            while (tempLine != "End")
            {
                string[] numpart = tempLine.Split(',');
                double x = double.Parse(numpart[0]);
                double y = double.Parse(numpart[1]);
                myPoint temp = new myPoint(x, y);
                tempReadPL.AddPoint(temp);

                tempLine = sr.ReadLine();
            }
        }

        myPolygon tempReadPG = null;
        private void readPolygon(StreamReader sr)
        {
            tempReadPG = new myPolygon();
            string tempLine = sr.ReadLine();
            while (tempLine != "End")
            {
                string[] numpart = tempLine.Split(',');
                double x = double.Parse(numpart[0]);
                double y = double.Parse(numpart[1]);
                myPoint temp = new myPoint(x, y);
                tempReadPG.AddPoint(temp);

                tempLine = sr.ReadLine();
            }
        }
        #endregion

        Rectangle selectRect = null;
        bool select;
        bool noway;
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            noway = true;
            select = false;
            menuStop_Click(sender, e);
            drawCanvas.Cursor = Cursors.Cross;
        }

        private void drawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            select = false;
            noway = false;
            if (myGeomtryType.ToString() != GeometryType.None.ToString())
            {
                drawCanvas.Cursor = Cursors.Cross;
            }
            if (myGeomtryType.ToString() == GeometryType.None.ToString())
            {
                drawCanvas.Cursor = Cursors.Arrow;
            }
        }


        List<myPoint> tempPoint = new List<myPoint>();
        List<myCircle> tempCircle = new List<myCircle>();
        List<myEllipse> tempEllipse = new List<myEllipse>();
        List<myRectangle> tempRect = new List<myRectangle>();
        List<myPolyline> tempPL = new List<myPolyline>();
        List<myPolygon> tempPG = new List<myPolygon>();

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                noway = true;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                List<int> delete = new List<int>();
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.removeDraw(drawCanvas);
                        delete.Add(allGeometry.IndexOf(Geo));
                    }
                }
                for (int i = delete.Count; i > 0; i--)
                {
                    int j = delete.Last();
                    allGeometry.RemoveAt(j);
                    delete.RemoveAt(delete.Count - 1);
                }
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (selectNum == 1 & select == false & noway == false)
            {
                selectNum = 0;
                noway = true;
                drawCanvas.Children.RemoveAt(drawCanvas.Children.Count - 1);
                int a = (int)selectRectX;
                int b = (int)selectRect.Width;
                int c = (int)selectRectY;
                int d = (int)selectRect.Height;
                foreach (var Geo in allGeometry)
                {
                    //if (Geo is myPoint)

                    if (Geo.centroidx() < a + b & Geo.centroidx() > a - b & Geo.centroidy() < c + d & Geo.centroidy() > c - d)
                    {
                        Geo.getColorA(255);
                    }
                }
            }
        }
    }
}


//调试代码MessageBox.Show(string.Format("{0}", drawCanvas.Children.Count));
//paintcolor =  Color.FromRgb(paintcolor.A, paintcolor.B, paintcolor.G);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace drawApp
{
    class myRectangle : myGeometry
    {
        public SolidColorBrush RectSolidColorBrush = new SolidColorBrush();

        public int thickness = 1;
        myPoint centroid = new myPoint();
        double width;
        double height;


        public override void getColor(byte r, byte g, byte b)
        {
            RectSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public myRectangle()
        {

        }

        public myRectangle(myPoint point1, myPoint point2)
        {
            width = Math.Abs(point2.x - point1.x) * 2;
            height = Math.Abs(point2.y - point1.y) * 2;
            centroid.x = point1.x;
            centroid.y = point1.y;
        }

        public override string info()
        {
            string info = string.Format("Rectangle:\nCenter: x:{0}, y:{1}\nWidth:{2}\nHeight:{3}", centroid.x, centroid.y, width, height);
            return info;
        }

        Rectangle tempRectangle = null;
        public override void myDraw(Canvas canvas)
        {
            base.myDraw(canvas);
            tempRectangle = new Rectangle();
            tempRectangle.Stroke = RectSolidColorBrush;
            tempRectangle.StrokeThickness = thickness;
            tempRectangle.Width = width;
            tempRectangle.Height = height;
            Canvas.SetLeft(tempRectangle, centroid.x - (width / 2));
            Canvas.SetTop(tempRectangle, centroid.y - (height / 2));

            canvas.Children.Add(tempRectangle);
        }

        public override string ToString()
        {
            string info = string.Format("Rectangle:{0},{1},{2},{3}:", centroid.x, centroid.y, width, height);
            info += RectSolidColorBrush.Color.R + "," + RectSolidColorBrush.Color.G + "," + RectSolidColorBrush.Color.B + ":";
            info += thickness;
            return info;
        }

        public override double centroidx()
        {
            return centroid.x;
        }

        public override double centroidy()
        {
            return centroid.y;
        }

        public override double centroidwidth()
        {
            return width;
        }

        public override double centroidheight()
        {
            return height;
        }

        public override void removeDraw(Canvas canvas)
        {
            base.removeDraw(canvas);
            canvas.Children.Remove(tempRectangle);
        }

        public override void getColorA(byte a)
        {
            RectSolidColorBrush.Color = Color.FromArgb(a, RectSolidColorBrush.Color.R, RectSolidColorBrush.Color.G, RectSolidColorBrush.Color.B);
        }
    }
}

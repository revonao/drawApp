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
    class myEllipse : myGeometry
    {
        public SolidColorBrush EllipseSolidColorBrush = new SolidColorBrush();
        public int thickness = 1;

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public override void getColor(byte r, byte g, byte b)
        {
            EllipseSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        myPoint centroid = new myPoint();
        double width;
        double height;

        public myEllipse()
        {

        }

        public myEllipse(myPoint point1, myPoint point2)
        {
            width = Math.Abs(point2.x - point1.x) * 2;
            height = Math.Abs(point2.y - point1.y) * 2;
            centroid.x = point1.x;
            centroid.y = point1.y;
        }

        public override string info()
        {
            string info = string.Format("Ellipse:\nCenter: x:{0}, y:{1}\nWidth:{2}\nHeight:{3}", centroid.x, centroid.y, width, height);
            return info;
        }

        Ellipse tempEllipse = null;
        public override void myDraw(Canvas canvas)
        {
            base.myDraw(canvas);
            tempEllipse = new Ellipse();
            tempEllipse.Stroke = EllipseSolidColorBrush;
            tempEllipse.StrokeThickness = thickness;
            tempEllipse.Width = width;
            tempEllipse.Height = height;

            Canvas.SetLeft(tempEllipse, centroid.x - (width / 2));
            Canvas.SetTop(tempEllipse, centroid.y - (height / 2));

            canvas.Children.Add(tempEllipse);
        }

        public override string ToString()
        {
            string info = string.Format("Ellipse:{0},{1},{2},{3}:", centroid.x, centroid.y, width, height);
            info += EllipseSolidColorBrush.Color.R + "," + EllipseSolidColorBrush.Color.G + "," + EllipseSolidColorBrush.Color.B + ":";
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
            canvas.Children.Remove(tempEllipse);
        }

        public override void getColorA(byte a)
        {
            EllipseSolidColorBrush.Color = Color.FromArgb(a, EllipseSolidColorBrush.Color.R, EllipseSolidColorBrush.Color.G, EllipseSolidColorBrush.Color.B);
        }
    }
}

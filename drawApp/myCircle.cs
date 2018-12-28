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
    class myCircle : myGeometry
    {
        myPoint centroid = new myPoint();
        double width;
        double height;
        public int thickness = 1;

        public SolidColorBrush circleSolidColorBrush = new SolidColorBrush();

        public override void getColor(byte r, byte g, byte b)
        {
            circleSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public myCircle()
        {

        }

        public myCircle(myPoint point1, myPoint point2)
        {
            width = Math.Abs(point2.x - point1.x) * 2;
            height = width;
            centroid.x = point1.x;
            centroid.y = point1.y;
        }

        public override string info()
        {
            string info = string.Format("Circle:\nCenter: x:{0}, y:{1}\nWidth:{2}", centroid.x, centroid.y, width);
            return info;
        }

        Ellipse tempCircle = null;
        public override void myDraw(Canvas canvas)
        {
            base.myDraw(canvas);
            tempCircle = new Ellipse();
            tempCircle.Stroke = circleSolidColorBrush;
            tempCircle.StrokeThickness = thickness;
            tempCircle.Width = width;
            tempCircle.Height = height;

            Canvas.SetLeft(tempCircle, centroid.x - (width / 2));
            Canvas.SetTop(tempCircle, centroid.y - (height / 2));

            canvas.Children.Add(tempCircle);
        }

        public override string ToString()
        {
            string info = string.Format("Circle:{0},{1},{2}:", centroid.x, centroid.y, width);
            info += circleSolidColorBrush.Color.R + "," + circleSolidColorBrush.Color.G + "," + circleSolidColorBrush.Color.B + ":";
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
            canvas.Children.Remove(tempCircle);
        }

        public override void getColorA(byte a)
        {
            circleSolidColorBrush.Color = Color.FromArgb(a, circleSolidColorBrush.Color.R, circleSolidColorBrush.Color.G, circleSolidColorBrush.Color.B);
        }
    }
}

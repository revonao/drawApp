using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace drawApp
{
    class myPolyline : myGeometry
    {
        public SolidColorBrush PLSolidColorBrush = new SolidColorBrush();
        public int thickness = 1;

        public override void getColor(byte r, byte g, byte b)
        {
            PLSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public myPolyline()
        {

        }

        public List<myPoint> linePoints = new List<myPoint>();

        public void AddPoint(myPoint Var)
        {
            linePoints.Add(Var);
        }

        public double Perimeter(List<myPoint> Var)
        {
            double Perimeter = 0;
            double tempLineOne = 0;
            double tempLineTwo = 0;
            int j;

            for (int i = 0; i < linePoints.Count - 1; i++)
            {
                j = i + 1;
                tempLineOne = (((linePoints[j]).x) - ((linePoints[i]).x));
                tempLineTwo = (((linePoints[j]).y) - ((linePoints[i]).y));
                Perimeter += Math.Sqrt((tempLineOne * tempLineOne) + (tempLineTwo * tempLineTwo));
            }

            return Perimeter;
        }

        public override string info()
        {
            string info = string.Format("Polyline" + Environment.NewLine);
            foreach (var point in linePoints)
            {
                info += point.info() + Environment.NewLine;
            }
            info += string.Format("Perimeter:{0}", Perimeter(linePoints));
            return info;
        }

        Polyline tempPolyline = null;
        public override void myDraw(Canvas canvas)
        {
            tempPolyline = new Polyline();
            base.myDraw(canvas);
            tempPolyline.Stroke = PLSolidColorBrush;
            tempPolyline.StrokeThickness = thickness;

            foreach(var point in linePoints)
            {
                Point tempPoint = new Point(point.x, point.y);
                tempPolyline.Points.Add(tempPoint);
            }
            canvas.Children.Add(tempPolyline);
        }

        public override string ToString()
        {
            string info = string.Format("Polyline:");
            info += PLSolidColorBrush.Color.R + "," + PLSolidColorBrush.Color.G + "," + PLSolidColorBrush.Color.B + ":";
            info += thickness + Environment.NewLine;
            foreach (var point in linePoints)
            {
                info += string.Format("{0},{1}", point.x, point.y) + Environment.NewLine;
            }
            info += "End";
            return info;
        }

        public override double centroidx()
        {
            List<double> pointx = new List<double>();
            foreach (var point in linePoints)
            {
                pointx.Add(point.x);
            }
            double xmax = pointx.Max();
            double xmin = pointx.Min();
            double centroidx = (xmax + xmin) / 2;
            return centroidx;
        }

        public override double centroidy()
        {
            List<double> pointy = new List<double>();
            foreach (var point in linePoints)
            {
                pointy.Add(point.y);
            }
            double ymax = pointy.Max();
            double ymin = pointy.Min();
            double centroidy = (ymax + ymin) / 2;
            return centroidy;
        }

        public override double centroidwidth()
        {
            List<double> pointx = new List<double>();
            foreach (var point in linePoints)
            {
                pointx.Add(point.x);
            }
            double xmax = pointx.Max();
            double xmin = pointx.Min();
            double width = xmax - xmin;
            return width;
        }

        public override double centroidheight()
        {
            List<double> pointy = new List<double>();
            foreach (var point in linePoints)
            {
                pointy.Add(point.y);
            }
            double ymax = pointy.Max();
            double ymin = pointy.Min();
            double height = ymax - ymin;
            return height;
        }

        public override void removeDraw(Canvas canvas)
        {
            base.removeDraw(canvas);
            canvas.Children.Remove(tempPolyline);
        }

        public override void getColorA(byte a)
        {
            PLSolidColorBrush.Color = Color.FromArgb(a, PLSolidColorBrush.Color.R, PLSolidColorBrush.Color.G, PLSolidColorBrush.Color.B);
        }
    }
}

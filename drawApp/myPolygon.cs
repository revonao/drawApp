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
    class myPolygon : myGeometry
    {
        public SolidColorBrush PGSolidColorBrush = new SolidColorBrush();
        public int thickness = 1;

        public override void getColor(byte r, byte g, byte b)
        {
            PGSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public myPolygon()
        {

        }

        public List<myPoint> gonPoints = new List<myPoint>();

        public void AddPoint(myPoint Var)
        {
            gonPoints.Add(Var);
        }

        public double Perimeter(List<myPoint> Var)
        {
            double Perimeter = 0;
            double tempLineOne = 0;
            double tempLineTwo = 0;
            int j;

            for (int i = 0; i < gonPoints.Count - 1; i++)
            {
                j = i + 1;
                tempLineOne = (((gonPoints[j]).x) - ((gonPoints[i]).x));
                tempLineTwo = (((gonPoints[j]).y) - ((gonPoints[i]).y));
                Perimeter += Math.Sqrt((tempLineOne * tempLineOne) + (tempLineTwo * tempLineTwo));
            }
            double lastLineOne = (((gonPoints[gonPoints.Count - 1]).x) - ((gonPoints[0]).x));
            double lastLineTwo = (((gonPoints[gonPoints.Count - 1]).y) - ((gonPoints[0]).y));
            Perimeter += Math.Sqrt((lastLineOne * lastLineOne) + (lastLineTwo * lastLineTwo));
            return Perimeter;
        }

        public double Area(List<myPoint> Var)
        {
            double area = 0;
            double sum = 0;
            //double tempTriPeri = 0;
            //double templLineOne = 0;
            //double templLineTwo = 0;
            //double templLineThr = 0;
            for (int i = 0; i < gonPoints.Count - 1; i++)
            {

                sum += (gonPoints[i].x * gonPoints[i + 1].y - gonPoints[i + 1].x * gonPoints[i].y);
                //j = i + 1;
                //templLineOne = Math.Sqrt((((gonPoints[0]).x) - ((gonPoints[i]).x)) * (((gonPoints[0]).x) - ((gonPoints[i]).x)) + (((gonPoints[0]).y) - ((gonPoints[i]).y)) * (((gonPoints[0]).y) - ((gonPoints[i]).y)));
                //templLineTwo = Math.Sqrt((((gonPoints[0]).x) - ((gonPoints[j]).x)) * (((gonPoints[0]).x) - ((gonPoints[j]).x)) + (((gonPoints[0]).y) - ((gonPoints[j]).y)) * (((gonPoints[0]).y) - ((gonPoints[j]).y)));
                //templLineThr = Math.Sqrt((((gonPoints[j]).x) - ((gonPoints[i]).x)) * (((gonPoints[j]).x) - ((gonPoints[i]).x)) + (((gonPoints[j]).y) - ((gonPoints[i]).y)) * (((gonPoints[j]).y) - ((gonPoints[i]).y)));
                //tempTriPeri = templLineOne + templLineTwo + templLineThr;
                //area += Math.Sqrt(tempTriPeri * (tempTriPeri - templLineOne) * (tempTriPeri - templLineTwo) * (tempTriPeri - templLineThr));
                area += (Math.Abs(sum + (gonPoints[i].x * gonPoints[0].y) - (gonPoints[0].x * gonPoints[i].y))) / 2;
            }

            return area;
        }

        public override string info()
        {
            string info = string.Format("Polygon" + Environment.NewLine);
            foreach (var point in gonPoints)
            {
                info += point.info() + Environment.NewLine;
            }
            info += string.Format("Perimeter:{0}", Perimeter(gonPoints)) + Environment.NewLine;
            info += string.Format("Area:{0}", Area(gonPoints));
            return info;
        }

        Polygon tempPolygon = null;
        public override void myDraw(Canvas canvas)
        {
            tempPolygon = new Polygon();
            base.myDraw(canvas);
            tempPolygon.Stroke = PGSolidColorBrush;
            tempPolygon.StrokeThickness = thickness;

            foreach (var point in gonPoints)
            {
                Point tempPoint = new Point(point.x, point.y);
                tempPolygon.Points.Add(tempPoint);
            }

            canvas.Children.Add(tempPolygon);
        }

        public override string ToString()
        {
            string info = string.Format("Polygon:");
            info += PGSolidColorBrush.Color.R + "," + PGSolidColorBrush.Color.G + "," + PGSolidColorBrush.Color.B + ":";
            info += thickness + Environment.NewLine;
            foreach (var point in gonPoints)
            {
                info += string.Format("{0},{1}", point.x, point.y) + Environment.NewLine;
            }
            info += "End";
            return info;
        }

        public override double centroidx()
        {
            List<double> pointx = new List<double>();
            foreach (var point in gonPoints)
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
            foreach (var point in gonPoints)
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
            foreach (var point in gonPoints)
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
            foreach (var point in gonPoints)
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
            canvas.Children.Remove(tempPolygon);
        }

        public override void getColorA(byte a)
        {
            PGSolidColorBrush.Color = Color.FromArgb(a, PGSolidColorBrush.Color.R, PGSolidColorBrush.Color.G, PGSolidColorBrush.Color.B);
        }
    }
}

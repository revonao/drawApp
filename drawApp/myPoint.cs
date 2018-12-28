using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace drawApp
{
    class myPoint : myGeometry
    {
        public SolidColorBrush pointSolidColorBrush = new SolidColorBrush();
        public int thickness = 1;

        public double x, y;

        public override void getColor(byte r, byte g, byte b)
        {
            pointSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public override void getThickness(int tk)
        {
            thickness = tk;
        }

        public myPoint()
        {
            x = 0;
            y = 0;
        }

        public myPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string info()
        {
            string info = string.Format("x:{0} y:{1}", x, y);
            //info = $"x:{x} y:{y}"; 字符内插 相较Format要直观很多
            return info;
        }

        Line tempLine1 = null;
        Line tempLine2 = null;
        public override void myDraw(Canvas canvas)
        {
            base.myDraw(canvas);
            tempLine1 = new Line();
            tempLine2 = new Line();

            tempLine1.Stroke = pointSolidColorBrush;
            tempLine1.StrokeThickness = thickness;
            tempLine1.X1 = x - 2;
            tempLine1.Y1 = y;
            tempLine1.X2 = x + 2;
            tempLine1.Y2 = y;
            canvas.Children.Add(tempLine1);

            tempLine2.Stroke = pointSolidColorBrush;
            tempLine2.StrokeThickness = thickness;
            tempLine2.X1 = x;
            tempLine2.Y1 = y - 2;
            tempLine2.X2 = x;
            tempLine2.Y2 = y + 2;
            canvas.Children.Add(tempLine2);
        }

        public override string ToString()
        {
            string info = string.Format("Point:{0},{1}:", x, y);
            info += pointSolidColorBrush.Color.R  + "," + pointSolidColorBrush.Color.G + "," + pointSolidColorBrush.Color.B + ":";
            info += thickness;
            return info;
        }

        public override double centroidx()
        {
            return x;
        }

        public override double centroidy()
        {
            return y;
        }

        public override double centroidwidth()
        {
            return 0;
        }

        public override double centroidheight()
        {
            return 0;
        }

        public override void removeDraw(Canvas canvas)
        {
            base.removeDraw(canvas);
            canvas.Children.Remove(tempLine1);
            canvas.Children.Remove(tempLine2);
        }

        public override void getColorA(byte a)
        {
            pointSolidColorBrush.Color = Color.FromArgb(a, pointSolidColorBrush.Color.R, pointSolidColorBrush.Color.G, pointSolidColorBrush.Color.B);
        }
    }
}

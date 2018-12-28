using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace drawApp
{
    public enum GeometryType
    {
        None,
        Point,
        Circle,
        Polyline,
        Polygon,
        Rectangle,
        Ellipse
    }

    class myGeometry
    {
        public myGeometry()
        {

        }

        public virtual string info()
        {
            return "this is a geometry.";
        }

        public virtual void myDraw(Canvas canvas)
        {

        }

        public virtual double centroidx()
        {
            return 0;
        }

        public virtual double centroidy()
        {
            return 0;
        }

        public virtual double centroidwidth()
        {
            return 0;
        }

        public virtual double centroidheight()
        {
            return 0;
        }

        public virtual void removeDraw(Canvas canvas)
        {

        }

        public SolidColorBrush geoSolidColorBrush = new SolidColorBrush();

        public virtual void getColor(byte r, byte g, byte b)
        {
            geoSolidColorBrush.Color = Color.FromRgb(r, g, b);
        }

        public virtual void getColorA(byte a)
        {
            byte r = geoSolidColorBrush.Color.R;
            byte g = geoSolidColorBrush.Color.G;
            byte b = geoSolidColorBrush.Color.B;
            geoSolidColorBrush.Color = Color.FromArgb(a,r,g,b);
        }

        public virtual void getThickness(int tk)
        {

        }
    }
}

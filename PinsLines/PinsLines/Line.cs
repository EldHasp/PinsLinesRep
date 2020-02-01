using System.Windows;
using System.Windows.Media;

namespace PinsLines
{
    public struct Line : IFigure
    {
        public int IdColor { get; }
        public Point LeftTop { get; }
        public Size Size { get; }
        public Point RightBottom { get; }
        public bool IsEnabled { get; }
        public Point Begin { get; }
        public Point End { get; }
        public PointCollection Points { get; }

        public Line(Point begin, Point end, int idColor, double medium)
        {
            double xMax, yMax;
            if (begin.X > end.X)
                xMax = begin.X;
            else
                xMax = end.X;

            if (begin.Y > end.Y)
                yMax = begin.Y;
            else
                yMax = end.Y;

            Begin = begin;
            End = end;
            LeftTop = new Point(0, 0);
            RightBottom = new Point(xMax, yMax);
            Size = new Size(xMax, yMax);
            IdColor = idColor;
            IsEnabled = false;
            Points = new PointCollection()
            {
                Begin,
                new Point(Begin.X, medium),
                new Point(End.X, medium),
                End
            };
        }

        //public IFigure ChangeColorMedium(int idColor, double medium)
        //    => new Line(Begin, End, idColor, medium);
    }
}

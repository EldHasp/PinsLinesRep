using System.Windows;

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

        public Line(Point begin, Point end, int idColor)
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
        }

        public IFigure ChangeFill(int idColor)
            => new Line(Begin, End, idColor);

        public static Line MainLine { get; } = new Line(new Point(), new Point(200, 200), 3);
        public static Line Connect { get; } = new Line(new Point(20, 20), new Point(40, 80), 5);
    }
}

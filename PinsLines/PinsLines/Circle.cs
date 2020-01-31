using System.Windows;

namespace PinsLines
{
    public struct Circle : IFigure
    {
        public int IdColor { get; }
        public Point LeftTop { get; }
        public Size Size { get; }
        public Point Center { get; }
        public double Radius { get; }
        public bool IsEnabled {get;}

        public Circle(Point center, double radius, int idColor)
        {
            Center = center;
            Radius = radius < 0 ? -radius : radius;
            LeftTop = Center - new Vector(Radius, Radius);
            Size = new Size(Radius * 2, Radius * 2);
            IdColor = idColor;
            IsEnabled = true;
        }

        public IFigure ChangeFill(int idColor)
            => new Circle(Center, Radius, idColor);

        public static Circle Top20 { get; } = new Circle(new Point(20, 20), 20, -1);
        public static Circle Top4010 { get; } = new Circle(new Point(40, 80), 10, 1);
    }
}

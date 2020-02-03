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
        public bool IsEnabled { get; }
        public int Number { get; }

        public Circle(Point center, double radius, int number, int idColor)
        {
            Center = center;
            Radius = radius < 0 ? -radius : radius;
            LeftTop = Center - new Vector(Radius, Radius);
            Size = new Size(Radius * 2, Radius * 2);
            IdColor = idColor;
            IsEnabled = true;
            Number = number;
        }

        public IFigure ChangeFill(int idColor)
            => new Circle(Center, Radius, Number, idColor);

    }
}

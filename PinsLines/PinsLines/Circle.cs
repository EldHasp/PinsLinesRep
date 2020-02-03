using System.Windows;

namespace PinsLines
{
    public class Circle : Figure
    {
        public Point Center { get; }
        public double Radius { get; }
        public int Number { get; }

        public Circle(Point center, double radius, int number)
            : base
            (
                center - new Vector(radius, radius),
                new Size(radius * 2, radius * 2),
                true
            )
        {
            Center = center;
            Radius = radius < 0 ? -radius : radius;
            Number = number;
        }
    }
}

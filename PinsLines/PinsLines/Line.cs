using System.Windows;
using System.Windows.Media;

namespace PinsLines
{
    public class Line : Figure
    {
        public Point Begin { get; }
        public Point End { get; private set; }
        public PointCollection Points { get; private set; }

        public Line(Point begin)
            : base
            (
                new Point(),
                new Size(),
                false
            )
        {
            Begin = begin;
        }

        public void SetEnd(Point end, double medium, int idColor)
        {
            End = end;
            Points = new PointCollection()
            {
                Begin,
                new Point(Begin.X, medium),
                new Point(End.X, medium),
                End
            };
            SetIdColor(idColor);
            OnPropertyChanged(nameof(End));
            OnPropertyChanged(nameof(Points));
        }
    }
}

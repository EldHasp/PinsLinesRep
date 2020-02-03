using System.Windows;

namespace PinsLines
{
    public class Figure : OnPropertyChangedClass, IFigure
    {
        public int IdColor { get; private set; } = -1;
        public Point LeftTop { get; private set; }
        public Size Size { get; private set; }
        public bool IsEnabled { get; private set; }
        public Figure(Point leftTop, Size size, bool isEnabled)
        {
            LeftTop = leftTop;
            Size = size;
            IsEnabled = isEnabled;
        }

        public void SetIdColor(int idColor)
        {
            IdColor = idColor;
            OnPropertyChanged(nameof(IdColor));
        }
    }
}

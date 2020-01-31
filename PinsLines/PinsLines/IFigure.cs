using System.Windows;

namespace PinsLines
{
    public interface IFigure
    {
        int IdColor { get; }
        Point LeftTop { get; }
        Size Size { get; }
        bool IsEnabled { get; }
    }
}

using System.Windows;

namespace PinsLines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
    }

    public class PinsLineVM
    {
        public PinsLine PinsLine { get; } = new PinsLine(11, 17);

        public PinsLineVM()
        {
            AddLine = new RelayCommand(p=> PinsLine.SetLine(FirstIndex, SecondIndex));
            RemoveFirstLine = new RelayCommand(p => PinsLine.RemoveLineFirst(FirstIndex));
            RemoveSecondLine = new RelayCommand(p => PinsLine.RemoveLineSecond(SecondIndex));

            PinsLine.SetLine(0, 8);
            PinsLine.SetLine(2, 13);
            PinsLine.SetLine(6, 3);
            PinsLine.SetLine(10, 1);
        }

        public int FirstIndex { get; set; }
        public int SecondIndex { get; set; }

        public RelayCommand AddLine { get; }
        public RelayCommand RemoveFirstLine { get; }
        public RelayCommand RemoveSecondLine { get; }
    }
}

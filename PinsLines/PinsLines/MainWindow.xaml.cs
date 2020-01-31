using System.Windows;

namespace PinsLines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PinsLine pinsLine = new PinsLine(11, 17);
            pinsLine.SetLine(0, 8);
            pinsLine.SetLine(2, 13);
            pinsLine.SetLine(6, 3);
            pinsLine.SetLine(10, 1);

            pinsUS.PinsLine = pinsLine;
        }
    }
}

namespace PinsLines
{
    public class PinsLineVM
    {
        public PinsLine PinsLine { get; } = new PinsLine(11, 17);

        public PinsLineVM()
        {
            AddLine = new RelayCommand(p => PinsLine.SetLine(FirstIndex - 1, SecondIndex - 1));
            RemoveFirstLine = new RelayCommand(p => PinsLine.RemoveLineFirst(FirstIndex - 1));
            RemoveSecondLine = new RelayCommand(p => PinsLine.RemoveLineSecond(SecondIndex - 1));

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

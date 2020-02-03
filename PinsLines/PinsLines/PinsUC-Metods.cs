using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PinsLines
{
    public partial class PinsUC
    {
        ///// <summary>Коллекция всех фигур</summary>
        /// <summary>Список фигур первого разъёма</summary>
        private readonly ObservableCollection<Circle> First = new ObservableCollection<Circle>();
        /// <summary>Список фигур второго разъёма</summary>
        private readonly ObservableCollection<Circle> Second = new ObservableCollection<Circle>();
        /// <summary>Список фигур соединений</summary>
        private readonly ObservableCollection<Line> Lines = new ObservableCollection<Line>();

        #region Константы 
        /// <summary>Шаг между контактами</summary>
        const double StepPin = 60;
        /// <summary>Радиус контактов</summary>
        const double RadiusPin = 20;
        /// <summary>Расстояние между разъёмами</summary>
        const double DistancePins = 400;
        #endregion 

        /// <summary>Полная перерисовка соединения с переинициализацией коллекций</summary>
        private void InitCollection()
        {
            //Figures.Clear();
            First.Clear();
            Second.Clear();
            Lines.Clear();
            CurentIndexColor = 0;
            SelectedFirstIndex = -1;
            SelectedSecondIndex = -1;

            double offsetFirst = 0;
            double offsetSecond = 0;

            double offset(int min, int max)
            {
                double offs = StepPin / 2.0;

                if (min == max)
                    return offs;

                if (min > max)
                    (min, max) = (max, min);

                return offs + ((max - min) / 2) * StepPin;
            }
            if (PinsLine.FirstLength < PinsLine.SecondLength)
                offsetFirst = offset(PinsLine.FirstLength, PinsLine.SecondLength);
            else
                offsetSecond = offset(PinsLine.FirstLength, PinsLine.SecondLength);

            if (PinsLine == null || PinsLine.FirstLength <= 0 || PinsLine.SecondLength <= 0)
                return;

            // Заполнение коллекции фигур
            for (int index = 0; index < PinsLine.FirstLength; index++)
                First.Add(new Circle(new Point(offsetFirst + (index + 1) * StepPin, StepPin), RadiusPin, index + 1));

            for (int index = 0; index < PinsLine.SecondLength; index++)
                Second.Add(new Circle(new Point(offsetSecond + (index + 1) * StepPin, DistancePins), RadiusPin, index + 1));

            for (int index = 0; index < PinsLine.FirstLength; index++)
                Lines.Add(new Line(First[index].Center));

            /// Задание параметров для горизонтальных линий
            firstLine = First[0].Center.Y + First[0].Size.Height * 2;
            stepLine = Second[0].Center.Y - First[0].Center.Y - First[0].Size.Height * 4;
            stepLine /= First.Count;

            // Отрисовка соединений
            for (int index = 0; index < PinsLine.FirstLength; index++)
            {
                int secInd = PinsLine.Lines[index];
                if (secInd >= 0)
                    DrawLine(index, secInd);
            }
        }

        /// <summary>Индекс текущего цвета</summary>
        private int _curentIndexColor;
        /// <summary>Смещение первой линиии</summary>
        private double firstLine;
        /// <summary>Шаг между линиями</summary>
        private double stepLine;

        public int CurentIndexColor { get => _curentIndexColor; set => _curentIndexColor = value % SolidColors.Length; }

        /// <summary>Рисование линии</summary>
        /// <param name="firstIndex">Индекс первого разъёма</param>
        /// <param name="secondIndex">Индекс первого разъёма</param>
        /// <remarks>Метод задаёт текущий индекс цвета указанным
        /// контактам и параметры для отрисовки линии между ними</remarks>
        private void DrawLine(int firstIndex, int secondIndex)
        {
            Circle first = First[firstIndex];
            Circle second = Second[secondIndex];

            First[firstIndex].SetIdColor(CurentIndexColor);
            Second[secondIndex].SetIdColor(CurentIndexColor);
            double medium = firstLine + stepLine * firstIndex;
            Lines[firstIndex].SetEnd(second.Center, medium, CurentIndexColor);

            CurentIndexColor++;
        }

        /// <summary>Очистка линии</summary>
        /// <param name="firstIndex">Индекс первого разъёма</param>
        /// <param name="secondIndex">Индекс первого разъёма</param>
        /// <remarks>Метод задаёт индекс цвета -1 указанным
        /// контактам и стирает линию отходящую от контакта первого разъёма</remarks>
        private void ClearLine(int firstIndex, int secondIndex)
        {
            First[firstIndex].SetIdColor(-1);
            Second[secondIndex].SetIdColor(-1);
            Lines[firstIndex].SetIdColor(-1);
        }
    }
}

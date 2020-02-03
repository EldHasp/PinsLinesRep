using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PinsLines
{
    public partial class PinsUC
    {
        /// <summary>Коллекция всех фигур</summary>
        /// <remarks>Вначале списка содержатся невыбираемые фигуры (Линии), потом первый (верхний) разъём, в конце - второй</remarks>
        private readonly ObservableCollection<IFigure> Figures = new ObservableCollection<IFigure>();
        /// <summary>Список фигур первого разъёма</summary>
        private readonly List<Circle> First = new List<Circle>();
        /// <summary>Список фигур второго разъёма</summary>
        private readonly List<Circle> Second = new List<Circle>();
        /// <summary>Список фигур соединений</summary>
        private readonly List<Line> Lines = new List<Line>();

        #region Константы 
        /// <summary>Шаг между контактами</summary>
        const double StepPin = 60;
        /// <summary>Радиус контактов</summary>
        const double RadiusPin = 20;
        /// <summary>Расстояние между разъёмами</summary>
        const double DistancePins = 400;
        #endregion 

        /// <summary>Полная перерисовка соединения с переинициавлизацией коллекций</summary>
        private void InitCollection()
        {
            Figures.Clear();
            First.Clear();
            Second.Clear();
            Lines.Clear();
            CurentIndexColor = 0;

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
            {
                Line line = new Line();
                Lines.Add(line);
                Figures.Add(line);
            }
            for (int index = 0; index < PinsLine.FirstLength; index++)
            {
                Circle circle = new Circle(new Point(offsetFirst + (index + 1) * StepPin, StepPin), RadiusPin, index + 1, -1);
                First.Add(circle);
                Figures.Add(circle);
            }
            for (int index = 0; index < PinsLine.SecondLength; index++)
            {
                Circle circle = new Circle(new Point(offsetSecond + (index + 1) * StepPin, DistancePins), RadiusPin, index + 1, -1);
                Second.Add(circle);
                Figures.Add(circle);
            }

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
        private int CurentIndexColor;
        /// <summary>Смещение первой линиии</summary>
        private double firstLine;
        /// <summary>Шаг между линиями</summary>
        private double stepLine;
        /// <summary>Рисование линии</summary>
        /// <param name="firstIndex">Индекс первого разъёма</param>
        /// <param name="secondIndex">Индекс первого разъёма</param>
        /// <remarks>Метод задаёт текущий индекс цвета указанным
        /// контактам и параметры для отрисовки линии между ними</remarks>
        private void DrawLine(int firstIndex, int secondIndex)
        {
            Circle first = First[firstIndex];
            Circle second = Second[secondIndex];

            First[firstIndex] = (Circle)first.ChangeFill(CurentIndexColor);
            Second[secondIndex] = (Circle)second.ChangeFill(CurentIndexColor);
            double medium = firstLine + stepLine * firstIndex;
            Lines[firstIndex] = new Line(first.Center, second.Center, CurentIndexColor, medium);

            Figures[firstIndex + PinsLine.FirstLength] = First[firstIndex];
            Figures[firstIndex] = Lines[firstIndex];
            Figures[PinsLine.FirstLength * 2 + secondIndex] = Second[secondIndex];

            CurentIndexColor++;
        }

        /// <summary>Очистка линии</summary>
        /// <param name="firstIndex">Индекс первого разъёма</param>
        /// <param name="secondIndex">Индекс первого разъёма</param>
        /// <remarks>Метод задаёт индекс цвета -1 указанным
        /// контактам и стирает линию отходящую от контакта первого разъёма</remarks>
        private void ClearLine(int firstIndex, int secondIndex)
        {
            Circle first = First[firstIndex];
            Circle second = Second[secondIndex];

            First[firstIndex] = (Circle)first.ChangeFill(-1);
            Second[secondIndex] = (Circle)second.ChangeFill(-1);
            Lines[firstIndex] = new Line();

            Figures[firstIndex + PinsLine.FirstLength] = First[firstIndex];
            Figures[firstIndex] = Lines[firstIndex];
            Figures[PinsLine.FirstLength * 2 + secondIndex] = Second[secondIndex];
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PinsLines
{
    public partial class PinsUC
    {
        /// <summary>коллекция ысех фигур</summary>
        /// <remarks>Вначале списка содержатся невыбираемые фигуры (Линии), потом первый (верхний) разъйм, в конце - второй</remarks>
        private readonly ObservableCollection<IFigure> Figures = new ObservableCollection<IFigure>();
        /// <summary>Список фигур первого разъёма</summary>
        private readonly List<Circle> First = new List<Circle>();
        /// <summary>Список фигур второго разъёма</summary>
        private readonly List<Circle> Second = new List<Circle>();
        /// <summary>Список фигур соединений</summary>
        private readonly List<Line> Lines = new List<Line>();


        /// <summary>Полная перерисовка соединения</summary>
        private void InitCollection()
        {
            Figures.Clear();
            First.Clear();
            Second.Clear();
            Lines.Clear();
            CurentIndexColor = 0;

            if (PinsLine == null || PinsLine.FirstLength <= 0 || PinsLine.SecondLength <= 0)
                return;

            // Заполнение коллекци фигур
            for (int index = 0; index < PinsLine.FirstLength; index++)
            {
                Line line = new Line();
                Lines.Add(line);
                Figures.Add(line);
            }
            for (int index = 0; index < PinsLine.FirstLength; index++)
            {
                Circle circle = new Circle(new Point((index + 1) * 60, 60), 20, -1);
                First.Add(circle);
                Figures.Add(circle);
            }
            for (int index = 0; index < PinsLine.SecondLength; index++)
            {
                Circle circle = new Circle(new Point((index + 1) * 60, 400), 20, -1);
                Second.Add(circle);
                Figures.Add(circle);
            }

            /// Задание параметров для гризантальных линий
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
        private double firstLine;
        private double stepLine;
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

    }
}

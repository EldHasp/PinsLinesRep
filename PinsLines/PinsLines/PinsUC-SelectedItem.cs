using System;
using System.Windows.Controls;

namespace PinsLines
{
    public partial class PinsUC
    {
        /// <summary>Индес выделенного контакта в первом разъёме</summary>
        private int SelectedFirstIndex;
        /// <summary>Индес выделенного контакта во втором разъёме</summary>
        private int SelectedSecondIndex;
        /// <summary>Флаг изменения колекции внутри события SelectionChanged</summary>
        private bool IsSelectionChanged;


        /// <summary>Обработчик события SelectionChanged</summary>
        private void listBoxFigures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /// Выход если флаг установлен
            if (IsSelectionChanged)
                return;

            /// Установка флага
            IsSelectionChanged = true;

            /// Переменные для индекса и выделенного элемента
            int index;
            Circle? select = listBoxFigures.SelectedItem as Circle?;

            /// Если нет выделения
            if (listBoxFigures.SelectedItem == default)
                SelectClear();

            /// Если выделен контакт первого разъёма
            else if ((index = First.IndexOf(select.Value)) >= 0)
                SelectFirst(index);

            /// Если выделен контакт второго разъёма
            else if ((index = Second.IndexOf(select.Value)) >= 0)
                SelectSecond(index);

            /// Если выделено непонятно что
            else
                throw new Exception("Выделен элемент отсутсвующий в коллекциях");

            /// Сброс флага
            IsSelectionChanged = false;
        }

        /// <summary>Обработка выделения контакта второго разъёма</summary>
        /// <param name="index">Индекс контакта</param>
        private void SelectSecond(int index)
        {
            if (SelectedFirstIndex >= 0)
            {
                int indexFirst = SelectedFirstIndex;
                SelectClear();
                PinsLine.SetLine(indexFirst, index);
            }
            else
            {
                if (SelectedSecondIndex >= 0)
                    SelectClearSecond();
                PinsLine.RemoveLineSecond(index);
                SelectedSecondIndex = index;
                Figures[PinsLine.FirstLength * 2 + SelectedSecondIndex]
                    = Second[SelectedSecondIndex]
                    = Second[SelectedSecondIndex].ChangeColor(CurentIndexColor);
            }
        }

        /// <summary>Обработка выделения контакта первого разъёма</summary>
        /// <param name="index">Индекс контакта</param>
        private void SelectFirst(int index)
        {
            if (SelectedSecondIndex >= 0)
            {
                int indexSecond = SelectedSecondIndex;
                SelectClear();
                PinsLine.SetLine(index, indexSecond);
            }
            else
            {
                if (SelectedFirstIndex >= 0)
                    SelectClearFirst();
                PinsLine.RemoveLineFirst(index);
                SelectedFirstIndex = index;
                Figures[PinsLine.FirstLength + SelectedFirstIndex]
                    = First[SelectedFirstIndex]
                    //= SelectedFirst
                    = First[SelectedFirstIndex].ChangeColor(CurentIndexColor);
            }
        }

        /// <summary>Очистка выделения</summary>
        private void SelectClear()
        {
            SelectClearFirst();
            SelectClearSecond();
        }

        /// <summary>Очистка выделения контакта первого разъёма</summary>
        private void SelectClearFirst()
        {
            if (SelectedFirstIndex >= 0)
            {
                Figures[PinsLine.FirstLength + SelectedFirstIndex] =
                      First[SelectedFirstIndex] = First[SelectedFirstIndex].ChangeColor(-1);
                SelectedFirstIndex = -1;
            }

        }

        /// <summary>Очистка выделения контакта второго разъёма</summary>
        private void SelectClearSecond()
        {
            if (SelectedSecondIndex >= 0)
            {
                Figures[PinsLine.FirstLength * 2 + SelectedSecondIndex]
                     = Second[SelectedSecondIndex] = Second[SelectedSecondIndex].ChangeColor(-1);
                SelectedSecondIndex = -1;
            }
        }
    }
}

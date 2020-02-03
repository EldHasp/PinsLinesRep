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

            /// Если нет выделения
            if (((ListBox)sender).SelectedItem == default)
                SelectClear();

            /// Если выделен контакт первого разъёма
            else if (sender == listBoxFirst)
                SelectFirst(First.IndexOf((Circle)listBoxFirst.SelectedItem));

            /// Если выделен контакт второго разъёма
            else if (sender == listBoxSecond)
                SelectSecond(Second.IndexOf((Circle)listBoxSecond.SelectedItem));


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
                Second[SelectedSecondIndex].SetIdColor(CurentIndexColor);
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
                First[SelectedFirstIndex].SetIdColor(CurentIndexColor);
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
                First[SelectedFirstIndex].SetIdColor(-1);
                SelectedFirstIndex = -1;
            }

        }

        /// <summary>Очистка выделения контакта второго разъёма</summary>
        private void SelectClearSecond()
        {
            if (SelectedSecondIndex >= 0)
            {
                Second[SelectedSecondIndex].SetIdColor(-1);
                SelectedSecondIndex = -1;
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;

namespace PinsLines
{
    /// <summary>Делагат события удаления/добавления линии</summary>
    /// <param name="sender">Источник события</param>
    /// <param name="firstPin">Начало линии</param>
    /// <param name="secondPin">Конец линии</param>
    /// <param name="isRemove"><see langword="false"/> - добавлена линия, 
    /// <see langword="true"/> - удалена линия</param>
    public delegate void PinsLineChangedHandler(object sender, int firstPin, int secondPin, bool isRemove);


    /// <summary>Класс-контейнер для контактов и соединительных линий</summary>
    public class PinsLine
    {
        #region Событие и вспомогательные методы
        /// <summary>Событие возникающее при добавлении/удалении линии</summary>
        public event PinsLineChangedHandler PinsLineChangedEvent;
        /// <summary>Метод вызова события при добавлении линии</summary>
        /// <param name="firstPin">Контакт первого разъёма</param>
        /// <param name="secondPin">Контакт второго разъёма</param>
        protected void OnPinsLineAdd(int firstPin, int secondPin)
            => PinsLineChangedEvent?.Invoke(this, firstPin, secondPin, false);
        /// <summary>Метод вызова события при удалении линии</summary>
        /// <param name="firstPin">Контакт первого разъёма</param>
        /// <param name="secondPin">Контакт второго разъёма</param>
        protected void OnPinsLineRemove(int firstPin, int secondPin)
             => PinsLineChangedEvent?.Invoke(this, firstPin, secondPin, true);
        #endregion

        /// <summary>Длина первого разъёма</summary>
        public int FirstLength { get; }
        /// <summary>Длина второго разъёма</summary>
        public int SecondLength { get; }

        /// <summary>Конструктор</summary>
        /// <param name="firstLength">Длина первого разъёма</param>
        /// <param name="secondLength">Длина второго разъёма</param>
        public PinsLine(int firstLength, int secondLength)
        {
            FirstLength = firstLength;
            SecondLength = secondLength;
            LinesArray = new int[firstLength];
            Lines = Array.AsReadOnly(LinesArray);
            for (int i = 0; i < LinesArray.Length; i++)
                LinesArray[i] = -1;
        }

        /// <summary>Массив линий</summary>
        private readonly int[] LinesArray;
        /// <summary>Неизменяемый массив линий. 
        /// Индекс элемента - номер контакта первого разъёма.
        /// Значение элемента - номер контакта второго разъёма</summary>
        public ReadOnlyCollection<int> Lines { get; }

        /// <summary>Удаление линии по номеру на первом разъёма</summary>
        /// <param name="firstPin">Номер контакта первого разъёма</param>
        public void RemoveLineFirst(int firstPin)
        {
            int secondPin = LinesArray[firstPin];
            if (secondPin >= 0)
            {
                LinesArray[firstPin] = -1;
                OnPinsLineRemove(firstPin, secondPin);
            }
        }

        /// <summary>Удаление линии по номеру на втором разъёме</summary>
        /// <param name="secondPin">Номер контакта второго разъёма</param>
        public void RemoveLineSecond(int secondPin)
        {
            int firstPin = Array.IndexOf(LinesArray, secondPin);
            if (firstPin >= 0)
            {
                LinesArray[firstPin] = -1;
                OnPinsLineRemove(firstPin, secondPin);
            }
        }

        /// <summary>Добавление линии</summary>
        /// <param name="firstPin">Номер контакта первого разъёма</param>
        /// <param name="secondPin">Номер контакта второго разъёма</param>
        public void SetLine(int firstPin, int secondPin)
        {
            if (firstPin > FirstLength || firstPin < 0)
                throw new ArgumentOutOfRangeException(nameof(firstPin));
            if (secondPin > SecondLength || secondPin < 0)
                throw new ArgumentOutOfRangeException(nameof(secondPin));

            RemoveLineFirst(firstPin);
            RemoveLineSecond(secondPin);

            LinesArray[firstPin] = secondPin;
            OnPinsLineAdd(firstPin, secondPin);
        }
    }
}

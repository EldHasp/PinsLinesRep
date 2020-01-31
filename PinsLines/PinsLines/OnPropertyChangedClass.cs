using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PinsLines
{
    /// <summary>Базовый класс с реализацией INPC </summary>
    public abstract class OnPropertyChangedClass : INotifyPropertyChanged
    {
        #region Событие PropertyChanged
        /// <summary>Событие для извещения об изменения свойства</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Методы вызова события PropertyChanged
        /// <summary>Метод для вызова события извещения об изменении свойства</summary>
        /// <param name="propertyName">Изменившееся свойство</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
        /// <param name="propList">Список имён свойств</param>
        public void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>Метод для вызова события извещения об изменении перечня свойств</summary>
        /// <param name="propList">Список имён свойств</param>
        public void OnPropertyChanged(params string[] propList)
        {
            foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Метод для вызова события извещения об изменении всех свойств</summary>
        /// <param name="propList">Список свойств</param>
        public void OnAllPropertyChanged()
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

        /// <summary>Метод для вызова события извещения об изменении свойства
        /// с передачей дополнительных данных</summary>
        /// <param name="propertyName">Изменившееся свойство</param>
        /// <param name="removeData">Удаляемые данные</param>
        /// <param name="addData">Добавляемые данные</param>
        public void OnPropertyChanged(string propertyName, object removeData, object addData) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedDataEventArgs(propertyName, removeData, addData));

        #endregion

        #region Виртуальные защищённые методы для изменения значений свойств
        /// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName]string propertyName = "")
        {
            if ((fieldProperty != null && !fieldProperty.Equals(newValue)) || (fieldProperty == null && newValue != null))
                PropertyNewValue(ref fieldProperty, newValue, propertyName);
        }

        /// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
        /// <param name="fieldProperty">Ссылка на поле значения свойства</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="propertyName">Название свойства</param>
        protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            fieldProperty = newValue;
            OnPropertyChanged(propertyName);
        }
        #endregion
    }

    ///// <summary>Базовый класс с реализацией INPC </summary>
    //public abstract class OnPropertyChangedClass : INotifyPropertyChanged
    //{
    //    /// <summary>Событие для извещения об изменения свойства</summary>
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    /// <summary>Метод для вызова события извещения об изменении свойства</summary>
    //    /// <param name="propertyName">Изменившееся свойство</param>
    //    public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    //    /// <summary>Метод для вызова события извещения об изменении свойства
    //    /// с передачей дополнительных данных</summary>
    //    /// <param name="propertyName">Изменившееся свойство</param>
    //    /// <param name="removeData">Удаляемые данные</param>
    //    /// <param name="addData">Добавляемые данные</param>
    //    public void OnPropertyChanged(string propertyName, object removeData, object addData) =>
    //                PropertyChanged?.Invoke(this, new PropertyChangedDataEventArgs(propertyName, removeData, addData));

    //}
    /// <summary>Производный от PropertyChangedEventArgs класс для 
    /// передачи в событие PropertyChanged дполнительных данных</summary>
    public class PropertyChangedDataEventArgs : PropertyChangedEventArgs
    {
        /// <summary>Удаляемые данные</summary>
        public object RemoveData { get; set; }
        /// <summary>Добавляемые данные</summary>
        public object AddData { get; set; }

        public PropertyChangedDataEventArgs(string propertyName)
            : base(propertyName) { }
        public PropertyChangedDataEventArgs(string propertyName, object removeData, object addData)
            : this(propertyName)
        {
            RemoveData = removeData;
            AddData = addData;
        }
    }

    /// <summary>Базовый класс Окна с реализацией INPC </summary>
    public class WindowINPC : Window, INotifyPropertyChanged
    {
        /// <summary>Событие для извещения об изменения свойства</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Метод для вызова события извещения об изменении свойства</summary>
        /// <param name="propertyName">Изменившееся свойство</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public WindowINPC() : base() { }

    }

}

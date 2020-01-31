﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PinsLines
{
    /// <summary>
    /// Логика взаимодействия для PinsUC.xaml
    /// </summary>
    public partial class PinsUC : UserControl
    {
        public PinsUC()
        {
            InitializeComponent();
            listBoxFigures.ItemsSource = Figures;
        }

        /// <summary>Массив цветов успользуемый для отрисовки элементов</summary>
        public SolidColorBrush[] SolidColors
        {
            get => (SolidColorBrush[])GetValue(SolidColorsProperty);
            set => SetValue(SolidColorsProperty, value);
        }

        // Using a DependencyProperty as the backing store for SolidColors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolidColorsProperty =
            DependencyProperty.Register(nameof(SolidColors), typeof(SolidColorBrush[]), typeof(PinsUC), 
                new PropertyMetadata
                (
                    new SolidColorBrush[]
                    {
                        Brushes.Red,
                        Brushes.Orange,
                        Brushes.Yellow,
                        Brushes.Green,
                        Brushes.LightBlue,
                        Brushes.DarkBlue,
                        Brushes.Violet,
                        Brushes.Pink,
                        Brushes.Aqua,
                        Brushes.Gold
                    }
                ));

        /// <summary>Источник данных о разъёмах и соединениях</summary>
        public PinsLine PinsLine
        {
            get => (PinsLine)GetValue(PinsLineProperty);
            set => SetValue(PinsLineProperty, value);
        }

        // Using a DependencyProperty as the backing store for PinsLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PinsLineProperty =
            DependencyProperty.Register(nameof(PinsLine), typeof(PinsLine), typeof(PinsUC),
                new PropertyMetadata(null, (d, e) => ((PinsUC)d).LinesChanged(e)));

        /// <summary>Метод реагирующий на изменение источника данных</summary>
        /// <param name="e"></param>
        private void LinesChanged(DependencyPropertyChangedEventArgs e)
        {
            if(e.OldValue != null)
                ((PinsLine)e.OldValue).PinsLineChangedEvent -= PinsLine_PinsLineChangedEvent;

            if (e.NewValue != null)
            {
                ((PinsLine)e.NewValue).PinsLineChangedEvent += PinsLine_PinsLineChangedEvent;
                InitCollection();
            }
        }

        /// <summary>Метод-обработчик для события изменения в данных</summary>
        /// <param name="sender"></param>
        /// <param name="firstPin"></param>
        /// <param name="secondPin"></param>
        /// <param name="isRemove"></param>
        private void PinsLine_PinsLineChangedEvent(object sender, int firstPin, int secondPin, bool isRemove)
        {
            throw new NotImplementedException();
        }
    }
}

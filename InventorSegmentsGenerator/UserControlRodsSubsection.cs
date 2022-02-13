using System;
using Inventor;
using System.Windows.Forms;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Класс, содержащий таблицу элементов управления параметрами прутков-труб, упакованную в UserControl.
    /// </summary>
    public class UserControlRodsSubsection : UserControl
    {
        /// <summary>
        /// Максимальное количество подсекций.
        /// </summary>
        static public int MaxNumberSubsection = 10;
        /// <summary>
        /// Текущий номер подсекции.
        /// </summary>
        private int currentNumberSubsection;
        public int CurrentNumberSubsection
        {
            get => currentNumberSubsection;
            set
            {
                if (value <= 0 || value > MaxNumberSubsection)
                    throw new ArgumentException("Некорректное значение номера подсекции (UserControlRodsSubsection).");
                currentNumberSubsection = value;
            }
        }
        /// <summary>
        /// Диаметр прутков-труб вертикального перекрытия секции.
        /// </summary>
        private double  diameterRods;
        public double DiameterRods
        {
            get { return diameterRods; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("Некорректное значение величины диаметра прутка-трубы.");
                diameterRods = value;
            }
        }
        /// <summary>
        /// Толщина стенки прутков-труб вертикального перекрытия секции.
        /// </summary>
        private double wallThicknessRods;
        public double WallThicknessRods
        {
            get { return wallThicknessRods; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Некорректное значение величины толщины стенки трубы.");
                wallThicknessRods = value;
            }
        }
        /// <summary>
        /// Количество коротких прутков-труб вертикального перекрытия секции.
        /// </summary>
        private int numberShortRods;
        public int NumberShortRods
        {
            get { return numberShortRods; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Некорректное значение количества коротких прутков-труб.");
                numberShortRods = value; 
            }
        }
        /// <summary>
        /// Количество длинных прутков-труб вертикального перекрытия секции.
        /// </summary>
        private int numberLongRods;
        public int NumberLongRods
        {
            get { return numberLongRods; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Некорректное значение количества длинных прутков-труб.");
                numberLongRods = value;
            }
        }
        //TODO: определить два свойства: формула заполнения и расстояния до прутков-труб

        public TypeRodsSectionEnum TypeRodsSection;
        public MethodOfFillingRodsEnum MethodOfFillingRodsEnum;

        /// <summary>
        /// Класс, содержащий таблицу элементов управления параметрами прутков-труб, упакованную в UserControl.
        /// </summary>
        public UserControlRodsSubsection()
        {

        }
    }
}

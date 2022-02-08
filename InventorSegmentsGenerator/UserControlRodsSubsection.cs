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

        public 

        /// <summary>
        /// Класс, содержащий таблицу элементов управления параметрами прутков-труб, упакованную в UserControl.
        /// </summary>
        public UserControlRodsSubsection()
        {

        }
    }
}

using System;

namespace InventorSegmentsGenerator
{
    public class CompositeSubsectionRods
    {
        private RodInMiddleEnum rodInMiddle;
        public RodInMiddleEnum RodInMiddle
        {
            get { return rodInMiddle; }
            set { rodInMiddle = value; }
        }

        private int numberShortRods;
        public int NumberShortRods
        {
            get { return numberShortRods; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Недопустимое значение количества коротких прутков!");
                numberShortRods = value;
            }
        }

        private int numberLongRods;
        public int NumberLongRods
        {
            get { return numberLongRods; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Недопустимое значение количества длинных прутков!");
                numberLongRods = value;
            }
        }

        private double distanceLongRods;
        public double DistanceLongRods
        {
            get { return distanceLongRods; }
            set
            {
                if (value > 600.0 | value < 0.0)
                    throw new ArgumentException("Недопустимое значение расстояния между длинными прутками!");
                distanceLongRods = value;
            }
        }

        private double distanceShortRods;
        public double DistanceShortRods
        {
            get { return distanceShortRods; }
            set
            {
                if (value > 600.0 | value < 0.0)
                    throw new ArgumentException("Недопустимое значение расстояния между короткими прутками!");
                distanceShortRods = value;
            }
        }

        private double distanceFirstRodSideStand;
        public double DistanceFirstRodSideStand
        {
            get { return distanceFirstRodSideStand; }
            set
            {
                if (value > 600.0 | value < 0.0)
                    throw new ArgumentException("Недопустимое значение расстояния между короткими прутками!");
                distanceFirstRodSideStand = value;
            }
        }


        public CompositeSubsectionRods()
        {
            RodInMiddle = RodInMiddleEnum.Auto;
            NumberShortRods = 0;
            NumberLongRods = 0;
            DistanceLongRods = 49.2;
            DistanceShortRods = 16.4;
            DistanceFirstRodSideStand = 15.5;
        }
    }
}

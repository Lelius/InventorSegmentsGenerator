using System;

namespace InventorSegmentsGenerator
{
    public enum ProfileTypeEnum { A38, A48, AP48, AP58 };
    public enum RodInMiddleEnum { Yes, No, Auto };
    public enum TypeOfFasteningEnum { Boot, Ground, Rack };


    public struct RodsSection
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
    }


    public class CompositeSection
    {
        private ProfileTypeEnum channel;
        public ProfileTypeEnum Channel
        { 
            get => channel;
            set
            {
                if (value != ProfileTypeEnum.A38 | value != ProfileTypeEnum.A48)
                    throw new ArgumentException("Неверно указан тип профиля основы секции!");
                channel = value;
            }
        }

        private double basicAngle;
        public double BasicAngle
        {
            get => basicAngle;
            set
            {
                if (value <= -60.0 | value >= 60.0)
                    throw new ArgumentException("Недопустимая величина общего угла наклона!");
                basicAngle = value;
            }
        }

        private double lengthSubsection;
        public double LengthSubsection
        {
            get => lengthSubsection;
            set
            {
                if (value > 600.0 | value < 10.0)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                if (Channel == ProfileTypeEnum.A38 & value < 5.0)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                if (Channel == ProfileTypeEnum.A48 & value < 6.4)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                lengthSubsection = value;
            }
        }

        private double lengthRail;
        public double LengthRail
        {
            get => lengthRail;
            set
            {
                if (value > 600.0 | value < 10.0)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                if (Channel == ProfileTypeEnum.A38 & value < 5.0)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                if (Channel == ProfileTypeEnum.A48 & value < 6.4)
                    throw new ArgumentException("Недопустимая величина длины подсекции!");
                lengthRail = value;
            }
        }

        private double heightGroundRail;
        public double HeightGroundRail 
        {
            get => heightGroundRail;
            set
            {
                if (value > 600.0)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                if (Channel == ProfileTypeEnum.A38 & value < 8.2)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                if (Channel == ProfileTypeEnum.A48 & value < 10.0)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                heightGroundRail = value;
            }
        }

        private double heightCrossbarRail;
        public double HeightCrossbarRail
        {
            get { return heightCrossbarRail; }
            set 
            {
                if (value > 600.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (Channel == ProfileTypeEnum.A38 & value < 8.2)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (Channel == ProfileTypeEnum.A48 & value < 10.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                heightCrossbarRail = value; 
            }
        }

        private double heightSideStand;
        public double HeightSideStand
        {
            get { return heightSideStand; }
            set 
            {
                if (value > 600.0 | value < 0.0)
                    throw new ArgumentException("Недопустимое значение высоты стойки!");
                heightSideStand = value; 
            }
        }

        private RodsSection rodsSection;

        private TypeOfFasteningEnum typeSupportLeft;
        public TypeOfFasteningEnum TypeSupportLeft
        {
            get { return typeSupportLeft; }
            set { typeSupportLeft = value; }
        }

        private TypeOfFasteningEnum typeSupportRight;
        public TypeOfFasteningEnum TypeSupportRight
        {
            get { return typeSupportRight; }
            set { typeSupportRight = value; }
        }


        public CompositeSection()
        {
            Channel = ProfileTypeEnum.A38;
            BasicAngle = 0;
            LengthSubsection = 200.0;
            LengthRail = 200.0;
            HeightCrossbarRail = 110.0;
            HeightGroundRail = 120.0;
            HeightSideStand = 170;
            rodsSection.RodInMiddle = RodInMiddleEnum.Auto;
            rodsSection.NumberShortRods = 2;
            rodsSection.NumberLongRods = 2;
            rodsSection.DistanceLongRods = 49.2;
            rodsSection.DistanceShortRods = 16.4;
            rodsSection.DistanceFirstRodSideStand = 15.5;
            TypeSupportLeft = TypeSupportRight = TypeOfFasteningEnum.Boot;
        }
    }
}

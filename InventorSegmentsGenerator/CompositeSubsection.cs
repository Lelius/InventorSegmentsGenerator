using System;

namespace InventorSegmentsGenerator
{
    public class CompositeSubsection
    {
        private CompositeSubsectionRods compositeSubsectionRods;

        private ProfileTypeEnum channel;
        public ProfileTypeEnum Channel
        {
            get => channel;
            set
            {
                if (value != ProfileTypeEnum.A38)
                    if (value != ProfileTypeEnum.A48)
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


        public CompositeSubsection()
        {
            compositeSubsectionRods = new CompositeSubsectionRods();

            Channel = ProfileTypeEnum.A38;
            BasicAngle = 0;
            LengthSubsection = 200.0;
            LengthRail = 200.0;
            HeightCrossbarRail = 110.0;
            HeightGroundRail = 120.0;
            HeightSideStand = 170;
            TypeSupportLeft = TypeSupportRight = TypeOfFasteningEnum.Boot;
        }
    }
}

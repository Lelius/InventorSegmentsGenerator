using System;

namespace InventorSegmentsGenerator
{
    public class CompositeSection
    {
        private ProfileTypeEnum typeOfMainChannel;
        public ProfileTypeEnum TypeOfMainChannel
        {
            get => typeOfMainChannel;
            set
            {
                if (value != ProfileTypeEnum.A38)
                    if (value != ProfileTypeEnum.A48)
                        throw new ArgumentException("Недопустимое значение типа основного профиля!");
                typeOfMainChannel = value;
            }
        }

        private double basicAngle; 
        public double BasicAngle
        {
            get => basicAngle;
            set
            {
                if (value >= 60.0 || value <= -60.0)
                    throw new ArgumentException("Недопустимое значение основного угла наклона секции!");
                basicAngle = value;
            }
        }

        private double lengthSection;                                      //бродят мысли про составные перила
        public double LengthSection
        {
            get => lengthSection;
            set => lengthSection = value;
        }

        private int numberSubsections;
        public int NumberSubsections
        {
            get => numberSubsections;
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentException("Недопустимое значение количества подсекций!");
                numberSubsections = value;
            }
        }

        public bool LeftBodyKit;                                        //здесь будет объект

        public bool RightBodyKit;                                       //здесь будет объект

        private double heightGroundRail;
        public double HeightGroundRail
        {
            get => heightGroundRail;
            set
            {
                if (value > 600.0)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                if (typeOfMainChannel == ProfileTypeEnum.A38 & value < 8.2)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                if (typeOfMainChannel == ProfileTypeEnum.A48 & value < 10.0)
                    throw new ArgumentException("Недопустимая величина высоты (земля-перила) подсекции!");
                heightGroundRail = value;
            }
        }

        private double heightTopCrossbarRail;
        public double HeightTopCrossbarRail
        {
            get { return heightTopCrossbarRail; }
            set
            {
                if (value > 600.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (typeOfMainChannel == ProfileTypeEnum.A38 & value < 8.2)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (typeOfMainChannel == ProfileTypeEnum.A48 & value < 10.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                heightTopCrossbarRail = value;
            }
        }

        private double heightBottomCrossbarRail;
        public double HeightBottomCrossbarRail
        {
            get { return heightBottomCrossbarRail; }
            set
            {
                if (value > 600.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (typeOfMainChannel == ProfileTypeEnum.A38 & value < 8.2)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                if (typeOfMainChannel == ProfileTypeEnum.A48 & value < 10.0)
                    throw new ArgumentException("Недопустимая величина расстояния от нижней перекладины до перил");
                heightBottomCrossbarRail = value;
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

        public CompositeSubsections compositeSubsections;


        public CompositeSection()
        {
            typeOfMainChannel = ProfileTypeEnum.A38;
            BasicAngle = 0;
            LengthSection = 0;
            NumberSubsections = 0;
            LeftBodyKit = false;
            RightBodyKit = false;
            HeightGroundRail = 0;
            HeightTopCrossbarRail = 0;
            HeightBottomCrossbarRail = 0;
            HeightSideStand = 0;

            compositeSubsections = new CompositeSubsections();
        }

        public CompositeSection(int length)
        {
            if (length == 0)
            {
                typeOfMainChannel = ProfileTypeEnum.A38;
                BasicAngle = 0;
                LengthSection = length;
                NumberSubsections = 0;
                LeftBodyKit = false;
                RightBodyKit = false;
                HeightGroundRail = 0;
                HeightTopCrossbarRail = 0;
                HeightBottomCrossbarRail = 0;
                HeightSideStand = 0;

                compositeSubsections = new CompositeSubsections();
            }
            else
            {
                typeOfMainChannel = ProfileTypeEnum.A38;
                BasicAngle = 0;
                LengthSection = length;
                NumberSubsections = 1;
                LeftBodyKit = false;
                RightBodyKit = false;
                HeightGroundRail = 120;
                HeightTopCrossbarRail = 18.5;
                HeightBottomCrossbarRail = 110;
                HeightSideStand = 120;

                compositeSubsections = new CompositeSubsections();
            }
        }
    }
}

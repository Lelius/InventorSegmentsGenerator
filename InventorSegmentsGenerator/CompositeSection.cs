using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InventorSegmentsGenerator
{
    public class CompositeSection
    {
        public string Channel { get; set; }
        public double Length { get; set; }
        public double HeightCrossbarRail { get; set; }
        public double HeightGroundRail { get; set; }
        public double HeightSideStand { get; set; }
        public string CenterLayout { get; set; }
        public int NumberShortRods { get; set; }
        public double DistanceLongRods { get; set; }
        public double DistanceShortRods { get; set; }
        public double DistanceShortRodSideStand { get; set; }
        public string NumberSupport { get; set; }
        public string TypeSupport { get; set; }

        public CompositeSection()
        {
            Channel = "А38";
            Length = 2000.0;
            HeightCrossbarRail = 1100.0;
            HeightGroundRail = 1200.0;
            HeightSideStand = 1700.0;
            CenterLayout = "Пруток";
            NumberShortRods = 2;
            DistanceLongRods = 492.0;
            DistanceShortRods = 164.0;
            DistanceShortRodSideStand = 155.0;
            NumberSupport = "2";
            TypeSupport = "Грунт";
        }
    }
}

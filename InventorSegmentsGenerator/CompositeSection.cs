using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvAddIn
{
    class CompositeSection
    {
        public enum CompositeSectionPropChannel { Channel38, Channel48 }
        public enum CompositeSectionPropRod { Rod, NoRod }
        public enum CompositeSectionPropSupport { Support1, Support2, NoSupport }
        public enum CompositeSectionPropTypeSupport { BootType, GroundType, NoType }

        public CompositeSectionPropChannel Channel { get; set; }
        private double Length { get; set; }
        private double HeightCrossbarRail { get; set; }
        private double HeightGroundRail { get; set; }
        private double HeightSideStand { get; set; }
        private CompositeSectionPropRod CenterLayout { get; set; }
        private int NumberShortRods { get; set; }
        private double DistanceLongRods { get; set; }
        private double DistanceShortRods { get; set; }
        private double DistanceShortRodSideStand { get; set; }
        private CompositeSectionPropSupport NumberSupport { get; set; }
        private CompositeSectionPropTypeSupport TypeSupport { get; set; }

        public CompositeSection()
        {
            Channel = CompositeSectionPropChannel.Channel38;
            Length = 1200.0;
            HeightCrossbarRail = 1100.0;
            HeightGroundRail = 1200.0;
            HeightSideStand = 1700.0;
            CenterLayout = CompositeSectionPropRod.Rod;
            NumberShortRods = 2;
            DistanceLongRods = 492.0;
            DistanceShortRods = 164.0;
            DistanceShortRodSideStand = 155.0;
            NumberSupport = CompositeSectionPropSupport.Support2;
            TypeSupport = CompositeSectionPropTypeSupport.GroundType;
        }
    }
}

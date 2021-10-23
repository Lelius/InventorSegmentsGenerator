using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    abstract public class ProfileFrameSection
    {
        public Inventor.Application oInvApp { get; set; }
        public Document oDoc { get; set; }
        public PartDocument oPartDoc { get; set; }
        public PartComponentDefinition oCompDef { get; set; }
        public TransientGeometry oTransGeo { get; set; }

        public ProfileFrameSection(Inventor.Application m_inventorApplication)
        {
            oInvApp = m_inventorApplication;
            oDoc = oInvApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            oPartDoc = oDoc as PartDocument;
            oCompDef = oPartDoc.ComponentDefinition;
            oTransGeo = oInvApp.TransientGeometry;

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
        }

        protected bool isEdgeAndPoint2dOnStraight(Edge edge, Point2d point)
        {
            if (Math.Abs(edge.StartVertex.Point.X - point.X) < 0.0000001)
                if (Math.Abs(edge.StartVertex.Point.Y - point.Y) < 0.0000001)
                    if (Math.Abs(edge.StopVertex.Point.X - point.X) < 0.0000001)
                        if (Math.Abs(edge.StopVertex.Point.Y - point.Y) < 0.0000001)
                            return true;

            return false;
        }
    }
}

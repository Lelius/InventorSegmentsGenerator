using Inventor;

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
    }
}

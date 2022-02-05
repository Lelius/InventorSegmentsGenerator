using Inventor;
using System;

namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Абстрактный класс-каркас профиля, содержащий основные объекты-документы. 
    /// </summary>
    abstract public class ProfileFrameSection
    {
        /// <summary>
        /// Главный объект Inventor.Application
        /// </summary>
        private protected Inventor.Application oInvApp { get; set; }
        private protected Document oDoc { get; set; }
        private protected PartDocument oPartDoc { get; set; }
        private protected PartComponentDefinition oCompDef { get; set; }
        private protected TransientGeometry oTransGeo { get; set; }

        /// <summary>
        /// Абстрактный класс-каркас профиля, содержащий основные объекты-документы.
        /// </summary>
        /// <param name="invApp">Главный объект Inventor.Application</param>
        public ProfileFrameSection(Inventor.Application invApp)
        {
            oInvApp = invApp;
            oDoc = oInvApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, "", true);
            oPartDoc = oDoc as PartDocument;
            oCompDef = oPartDoc.ComponentDefinition;
            oTransGeo = oInvApp.TransientGeometry;

            oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
        }

        /// <summary>
        /// Проверяет, лежит ли точка point на прямой, коллинеарной грани edge и параллельной оси Z.
        /// </summary>
        /// <param name="edge">Объект Edge проверяемой грани.</param>
        /// <param name="point">Объект Point2d проверяемой точки.</param>
        /// <returns>В случае совпадения точки с прямой возвращается True, иначе возвращается False.</returns>
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

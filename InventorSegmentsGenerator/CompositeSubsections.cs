using System;
using System.Collections.Generic;

namespace InventorSegmentsGenerator
{
    public class CompositeSubsections
    {
        public static readonly int NUMBERSUBSECTIONMAX = 10;
        public CompositeSubsection[] compositeSubsections;

        private int count;
        public int Count
        {
            get => count;
        }

        public CompositeSubsections()
        {
            compositeSubsections = new CompositeSubsection[NUMBERSUBSECTIONMAX];
            count = 0;
        }


        public void Add(CompositeSubsection compositeSubsection)
        {
            if (count <= 120)
            {
                compositeSubsections[count] = compositeSubsection;
                count += 1;
            }
            else
                throw new Exception("Коллекция CompositeSubsections заполена полностью (120 элементов).");
        }


        public void Clear()
        {
            for (int i = 0; i < NUMBERSUBSECTIONMAX; i++)
            {
                compositeSubsections[i] = null;
                count = 0;
            }
        }
    }
}

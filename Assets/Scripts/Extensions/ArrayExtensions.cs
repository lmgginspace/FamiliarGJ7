using System;

namespace Extensions.System
{
    public static class ArrayExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static void Fill(this Array array, object o)
        {
            for (int i = 0; i < array.Length; i++)
                array.SetValue(o, i);
        }
        
        public static bool IndexInRange(this Array array, params int[] indexes)
        {
            for (int i = 0; i < array.Rank; i++)
            {
                if ((indexes[i] < array.GetLowerBound(i)) || (indexes[i] > array.GetUpperBound(i)))
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Devuelve un índice válido para esta instancia elegido al azar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int RandomIndex(this Array array)
        {
            Random r = new Random();
            return r.Next(array.Length);
        }
    }
    
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Extensions.System.Colections
{
    public static class ICollectionExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Determina si System.Collections.ICollection no contiene ningún elemento.
        /// </summary>
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count <= 0;
        }
        
        /// <summary>
        /// Determina si System.Collections.Generic.ICollection&lt;T&gt; no contiene ningún elemento.
        /// </summary>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count <= 0;
        }
    }
    
}
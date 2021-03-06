﻿using System;

namespace Extensions.System
{
    public static class BoolExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Convierte el valor de esta instancia en una representación de cadena de texto personalizada.
        /// </summary>
        /// <param name="trueText">Cadena de texto a utilizar para el valor verdadero.</param>
        /// <param name="falseText">Cadena de texto a utilizar para el valor falso.</param>
        public static string ToCustomString(this bool b, string trueText, string falseText)
        {
            return b ? trueText : falseText;
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en los valores numéricos 0 y 1, como números enteros de 32 bits.
        /// </summary>
        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }
    }
    
}
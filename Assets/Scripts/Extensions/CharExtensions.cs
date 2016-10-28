using System;
using System.Globalization;

namespace Extensions.System
{
    public static class CharExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static bool IsVowel(this char ch)
        {
            return "aeiouáéíóúàèìòùäëïöüâêîôû".Contains("" + ch);
        }
    }
    
}
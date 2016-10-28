using System;
using System.Text;

namespace Extensions.System
{
    public static class IntegerExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Restringe el valor de esta instancia a un intervalo de valores, devolviendo el valor de los límites cuando
        /// el valor está fuera del rango.
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static int ClampTo(this int i, int min, int max)
        {
            if (min < max)
                return (i < min) ? min : (i > max ? max : i);
            return (i < max) ? max : (i > min ? min : i);
        }
        
        /// <summary>
        /// Devuelve el valor de esta instancia si es positivo, o cero en otro caso.
        /// </summary>
        public static int ClampToPositive(this int i)
        {
            return (i > 0) ? i : 0;
        }
        
        /// <summary>
        /// Devuelve el número de dígitos que tiene esta instancia en su representación en base 10.
        /// </summary>
        public static int DigitCount(this int i)
        {
            return string.Format("{0}", i).Length - (i < 0 ? 1 : 0);
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en su representación como número romano.
        /// </summary>
        public static string ToRomanNumeral(this int i)
        {
            if (i == 0) return "Z";
            
            StringBuilder sb = new StringBuilder();
            int remain = i;
            
            if (i < 0) { sb.Append("-"); remain = -remain; }
            
            while (remain > 0)
            {
                if (remain >= 4000000)
                {
                    sb.Append(string.Format("(({0}))", (remain / 1000000).ToRomanNumeral()));
                    remain = remain % 1000000;
                }
                else if (remain >= 4000)
                {
                    sb.Append(string.Format("({0})", (remain / 1000).ToRomanNumeral()));
                    remain = remain % 1000;
                }
                else if (remain >= 1000) { sb.Append("M");  remain -= 1000; }
                else if (remain >= 900 ) { sb.Append("CM"); remain -= 900;  }
                else if (remain >= 500 ) { sb.Append("D");  remain -= 500;  }
                else if (remain >= 400 ) { sb.Append("CD"); remain -= 400;  }
                else if (remain >= 100 ) { sb.Append("C");  remain -= 100;  }
                else if (remain >= 90  ) { sb.Append("XC"); remain -= 90;   }
                else if (remain >= 50  ) { sb.Append("L");  remain -= 50;   }
                else if (remain >= 40  ) { sb.Append("XL"); remain -= 40;   }
                else if (remain >= 10  ) { sb.Append("X");  remain -= 10;   }
                else if (remain >= 9   ) { sb.Append("IX"); remain -= 9;    }
                else if (remain >= 5   ) { sb.Append("V");  remain -= 5;    }
                else if (remain >= 4   ) { sb.Append("IV"); remain -= 4;    }
                else if (remain >= 1   ) { sb.Append("I");  remain -= 1;    }
                else throw new Exception("Unexpected error.");
            }
            
            return sb.ToString();
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia a la base numérica especificada (en el rango [2, 36]).
        /// </summary>
        /// <param name="radix">Base del sistema de numeración de destino (en el rango [2, 36]).</param>
        public static string ToBase(this int i, int radix)
        {
            const int bitsInInt = 32;
            const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            if (radix < 2 || radix > digits.Length)
                throw new ArgumentException("La base numérica de destino debe estar comprendida entre 2 y " +
                    digits.Length.ToString());
            
            if (i == 0)
                return "0";
            
            int index = bitsInInt - 1;
            int currentNumber = i > 0 ? i : -i;
            char[] charArray = new char[bitsInInt];
            
            while (currentNumber != 0)
            {
                int remainder = currentNumber % radix;
                charArray[index--] = digits[remainder];
                currentNumber = currentNumber / radix;
            }
            
            string result = new String(charArray, index + 1, bitsInInt - index - 1);
            if (i < 0)
                result = "-" + result;
            
            return result;
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia se encuentra en el intervalo de valores especificado.
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static bool InRange(this int i, int min, int max)
        {
            return (min < max) ? (i >= min) && (i <= max) : (i >= max) && (i <= min);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es par.
        /// </summary>
        public static bool IsEven(this int i) { return (i % 2) == 0; }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es impar.
        /// </summary>
        public static bool IsOdd(this int i) { return (i % 2) == 1; }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia es primo.
        /// </summary>
        public static bool IsPrime(this int i)
        {
            if ((i % 2) == 0)
                return i == 2;
            
            int limit = (int)Math.Sqrt(i);
            for (int divisor = 3; divisor <= limit; divisor = divisor + 2)
            {
                if ((i % divisor) == 0)
                    return false;
            }
            return i != 1;
        }
        
        /// <summary>
        /// Extrae los dígitos especificados que contiene esta instancia según el sistema de numeración en base 10 y
        /// los devuelve en un nuevo número entero de 32 bits.
        /// </summary>
        /// <param name="from">Índice del dígito de comienzo. El valor 0 representa las unidades, el valor 1 las
        /// decenas, y así sucesivamente.</param>
        /// <param name="digitCount">Número de digitos a incluir en el resultado, contados a partir del dígito de
        /// comienzo.</param>
        public static int GetDigits(this int i, int from, int digitCount)
        {
            int fromPower = 1, toPower = 1, counter;
            for (counter = 0; counter < from; counter++)
                fromPower *= 10;
            
            for (counter = 0; counter < digitCount; counter++)
                toPower *= 10;
            
            return (i / fromPower) % toPower;
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en número de punto flotante de simple precisión.
        /// </summary>
        public static float ToFloat(this int i)
        {
            return (float)i;
        }
        
        /// <summary>
        /// Redondea el valor de esta instancia al múltiplo más cercano del parámetro especificado.
        /// </summary>
        public static int FitToMultiple(this int i, int multiple)
        {
            return (i / multiple) * multiple;
        }
        
        /// <summary>
        /// Para el valor n de esta instancia, y el valor m especificado como parámetro, calcula el valor de n mod m.
        /// Para cualquier valor n, el resultado siempre oscila entre 0 y |n-1|, incluso para valores negativos.
        /// </summary>
        public static int Mod(this int i, int m)
        {
            int r = i % m;
            return r < 0 ? r + m : r;
        }
    }
    
}
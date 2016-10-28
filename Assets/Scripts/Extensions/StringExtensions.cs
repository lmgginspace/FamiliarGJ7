using System;
using System.Globalization;

namespace Extensions.System
{
    public static class StringExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Delegados
        private delegate bool ParsingFunc<T>(string s, out T result);
        private delegate bool ParsingFuncDec<T>(string s, NumberStyles ns, IFormatProvider ci, out T result);
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de manipulación
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural invariante.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpperInvariant();
                return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural especificada.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this string s, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpper(cultureInfo);
                return s.Substring(0, 1).ToUpper(cultureInfo) + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los primeros caracteres que contenía
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        /// <returns></returns>
        public static string DropFirst(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(charCount, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los últimos caracteres que contenía
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        /// <returns></returns>
        public static string DropLast(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(0, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con todos los caracteres en orden inverso.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount"></param>
        /// <returns></returns>
        public static string Reverse(this string s)
        {
            if (s.Length <= 1) return s;
            
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String truncada a una determinada longitud máxima.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charCount">Número máximo de caracteres.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int charCount)
        {
            return (s == null || s.Length < charCount) ? s : s.Substring(0, charCount);
        }
        
        // Métodos de información
        /// <summary>
        /// Determina si el valor de este objeto System.String coincide con de de alguno de los otros objetos
        /// System.String suministrados como parámetros.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="stringList">Objetos System.String para comparar con esta instancia.</param>
        /// <returns></returns>
        public static bool EqualsRange(this string s, params string[] stringList)
        {
            foreach (string item in stringList)
            {
                if (item == s)
                    return true;
            }
            return false;
        }
        
        // Métodos de generación
        /// <summary>
        /// Genera una cadena de texto aleatoria con la longitud especificada y el número de palabras especificado.
        /// </summary>
        /// <param name="length">Longitud de la cadena de texto generada.</param>
        /// <param name="wordCount">Número de palabras.</param>
        public static string Placeholder(int length, int wordCount)
        {
            Random r = new Random();
            char[] result = new char[length];
            
            for (int i = 0; i < length; i++)
                result[i] = (char)r.Next(48, 125);
            
            for (int i = 0; i < wordCount - 1; i++)
                result[r.Next(length)] = ' ';
            
            return new string(result);
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por
        /// elementos de la matriz de caracteres Unicode especificada, y además quita todos los caracteres de espacio
        /// en blanco del principio y del final de cada objeto System.String de la matriz resultante.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static string[] SplitAndTrim(this string s, params char[] separators)
        {
            string[] result = s.Split(separators);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count)
        {
            string[] result = s.Split(separators, count);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        // Métodos de conversión
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero sin signo de 8 bits, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            return StringExtensions.TryParse<byte>(s, byte.TryParse, (byte)0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero sin signo de 8 bits, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static byte ToByte(this string s, byte fallbackValue)
        {
            return StringExtensions.TryParse<byte>(s, byte.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.DateTime que representa una fecha y hora,
        /// atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve una fecha estándar
        /// (1 de Enero del 2000, a las 0:00:00)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime result = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;
            return new DateTime(2000, 1, 1, 0, 0, 0, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.DateTime que representa una fecha y hora,
        /// atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve el valor
        /// especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime fallbackValue)
        {
            DateTime result = fallbackValue;
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;
            return fallbackValue;
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            return StringExtensions.TryParseDec<double>(s, double.TryParse, 0.0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static double ToDouble(this string s, double fallbackValue)
        {
            return StringExtensions.TryParseDec<double>(s, double.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor por defecto según el obtenido por la palabra clave default para la
        /// enumeración expecificada.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return StringExtensions.ToEnum(s, default(T));//(T)Enum.Parse(typeof(T), s, true);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string s, T fallbackValue) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s, true);
            }
            catch
            {
                return fallbackValue;
            }
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            return StringExtensions.TryParseDec<float>(s, float.TryParse, 0.0f);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static float ToFloat(this string s, float fallbackValue)
        {
            return StringExtensions.TryParseDec<float>(s, float.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static int ToInt(this string s, int fallbackValue)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this string s)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static long ToLong(this string s, long fallbackValue)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.TimeSpan que representa una cantidad de
        /// tiempo, atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve una
        /// cantidad de tiempo igual a cero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s)
        {
            return StringExtensions.TryParse<TimeSpan>(s, TimeSpan.TryParse, TimeSpan.Zero);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.TimeSpan que representa una cantidad de
        /// tiempo, atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve el valor
        /// especificado como alternativa.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s, TimeSpan fallbackValue)
        {
            return StringExtensions.TryParse<TimeSpan>(s, TimeSpan.TryParse, fallbackValue);
        }
        
        // Métodos de información sobre conversión
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero sin signo de 8 bits.
        /// </summary>
        public static bool IsByte(this string s)
        {
            return StringExtensions.IsParseable<byte>(s, byte.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un objeto System.DateTime.
        /// </summary>
        public static bool IsDateTime(this string s)
        {
            DateTime result = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return true;
            return false;
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número de punto flotante de doble
        /// precisión.
        /// </summary>
        public static bool IsDouble(this string s)
        {
            return StringExtensions.IsParseableDec<double>(s, double.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en una enumeración del tipo especificado.
        /// </summary>
        public static bool IsEnum<T>(this string s) where T : struct
        {
            try
            {
                Enum.Parse(typeof(T), s, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número de punto flotante de simple
        /// precisión.
        /// </summary>
        public static bool IsFloat(this string s)
        {
            return StringExtensions.IsParseableDec<float>(s, float.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero de 32 bits.
        /// </summary>
        public static bool IsInt(this string s)
        {
            return StringExtensions.IsParseable<int>(s, int.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero de 64 bits.
        /// </summary>
        public static bool IsLong(this string s)
        {
            return StringExtensions.IsParseable<long>(s, long.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un objeto System.TimeSpan.
        /// </summary>
        public static bool IsTimeSpan(this string s)
        {
            return StringExtensions.IsParseable<TimeSpan>(s, TimeSpan.TryParse);
        }
        
        // Métodos auxiliares
        private static T TryParse<T>(string s, ParsingFunc<T> parsingFunc, T defaultValue) where T : struct
        {
            T result = defaultValue;
            if (parsingFunc(s, out result))
                return result;
            return defaultValue;
        }
        
        private static T TryParseDec<T>(string s, ParsingFuncDec<T> parsingFunc, T defaultValue) where T : struct
        {
            T result = defaultValue;
            if (parsingFunc(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return result;
            return defaultValue;
        }
        
        private static bool IsParseable<T>(string s, ParsingFunc<T> parsingFunc) where T : struct
        {
            T result = default(T);
            if (parsingFunc(s, out result))
                return true;
            return false;
        }
        
        private static bool IsParseableDec<T>(string s, ParsingFuncDec<T> parsingFunc) where T : struct
        {
            T result = default(T);
            if (parsingFunc(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;
            return false;
        }
    }
    
}
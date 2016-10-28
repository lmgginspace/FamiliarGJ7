using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.System
{
    public static class MathUtil
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Calcula el máximo común divisor de dos números.
        /// </summary>
        /// <param name="a">Número a.</param>
        /// <param name="b">Número b.</param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            
            if (a == 0) return b;
            if (b == 0) return a;
            
            int maxIterations = 1000;
            for (int i = 0; i < maxIterations; i++)
            {
                int remainder = a % b;
                if (remainder == 0)
                    return b;
                
                a = b;
                b = remainder;
            }
            return 1;
        }
        
        /// <summary>
        /// Calcula el mínimo común múltiplo de dos números.
        /// </summary>
        /// <param name="a">Número a.</param>
        /// <param name="b">Número b.</param>
        /// <returns></returns>
        public static int LCM(int a, int b)
        {
            return a * b / MathUtil.GCD(a, b);
        }
        
        public static float ProbabilityOr(float a, float b)
        {
            a = a > 1.0f ? 1.0f : a; a = a < 0.0f ? 0.0f : a;
            b = b > 1.0f ? 1.0f : b; b = b < 0.0f ? 0.0f : b;
            
            return a + ((1.0f - a) * b);
        }
        
        public static int ToBase10(string number, int radix)
        {
            const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            if (radix < 2 || radix > digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " +
                    digits.Length.ToString());
            
            if (String.IsNullOrEmpty(number))
                return 0;
            
            // Make sure the arbitrary numeral system number is in upper case
            number = number.ToUpperInvariant();
            
            int result = 0;
            int multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }
                
                int digit = digits.IndexOf(c);
                if (digit == -1)
                    throw new ArgumentException(
                        "Invalid character in the arbitrary numeral system number",
                        "number");
                
                result += digit * multiplier;
                multiplier *= radix;
            }
            
            return result;
        }
    }
    
    public static class StatisticsExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static float Mean(this IEnumerable<float> values)
        {
            int count = 0;
            float total = 0.0f;
            foreach (var item in values)
            {
                count++;
                total += item;
            }
            
            return total / (float)count;
        }
        
        public static float Mean(this IEnumerable<int> values)
        {
            int count = 0, total = 0;
            foreach (var item in values)
            {
                count++;
                total += item;
            }
            
            return (float)total / (float)count;
        }
        
        public static float StandardDeviation(this IEnumerable<float> values)
        {
            float mean = values.Mean();
            
            int count = 0;
            float sumOfSquaresTotal = 0;
            foreach (var item in values)
            {
                count++;
                sumOfSquaresTotal += (item - mean) * (item - mean);
            }
            
            return Mathf.Sqrt(sumOfSquaresTotal / (float)count);
        }
        
        public static float StandardDeviation(this IEnumerable<int> values)
        {
            float mean = values.Mean();
            
            int count = 0;
            float sumOfSquaresTotal = 0;
            foreach (var item in values)
            {
                count++;
                sumOfSquaresTotal += ((float)item - mean) * ((float)item - mean);
            }
            
            return Mathf.Sqrt(sumOfSquaresTotal / (float)count);
        }
    }
    
}
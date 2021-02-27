using System;

namespace Common
{
    public static class Utils
    {
        public static double Modulo(double a, double b) => a - b * Math.Floor(a / b);
    }
}
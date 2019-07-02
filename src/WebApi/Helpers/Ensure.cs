using System;

namespace WebApi.Helpers
{
    public static class Ensure
    {
        public static void IsNotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsNotNullOrEmpty(string value, string paramName)
        {
            IsNotNull(value, paramName);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(
                    $"{paramName} cannot be an empty string.",
                    paramName);
            }
        }

        public static void IsPositiveInteger(int value, string paramName)
        {
            if (value < 1)
            {
                throw new ArgumentException(
                    $"{paramName} must be a positive integer.",
                    paramName);
            }
        }
    }
}
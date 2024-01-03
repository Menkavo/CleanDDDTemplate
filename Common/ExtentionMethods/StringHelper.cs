using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Common.ExtentionMethods
{
    public static class StringHelper
    {
        /// <summary>
        /// Checkes if the string is null or empty or only contains whitespaces.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>True if the string is null, empty or only contains whitespaces. false if the string contains any other character.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (str == null) return true;
            return str.Replace(" ", "") == string.Empty;
        }

        /// <summary>
        /// Checks if the string is null or empty.
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>True if the string is either null or empty and false if not.</returns>
        public static bool IsNullOrEmpty(this string str) => str == null || str.Length == 0;

        /// <summary>
        /// Checks if the string is not null or empty.
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>True if the string is neither null nor empty and false if it is.</returns>
        public static bool IsNotNullOrEmpty(this string str) => str != null && str.Length != 0;

        /// <summary>
        /// Removes consecutive whitespaces.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The string with consecutive whitespaces removed.</returns>
        public static string RemoveUnnecessarySpaces(this string str) => Regex.Replace(str, @"\s+", " ").Trim();

        /// <summary>
        /// Converts a string to short if the cast is valid.
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static short ToInt16(this string str)
        {
            if (short.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        /// <summary>
        /// Converts a string to int if the cast is valid.
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static int ToInt32(this string str)
        {
            if (int.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        /// <summary>
        /// Converts a string to long if the cast is valid.
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static long ToInt64(this string str)
        {
            if (long.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        /// <summary>
        /// Converts a string to a nullable short.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The short value if the cast is valid and null if it's not.</returns>
        public static short? ToInt16Nullable(this string str)
        {
            if (str.IsNullOrEmpty() || !short.TryParse(str, out var result)) return null;
            else return result;
        }

        /// <summary>
        /// Converts a string to a nullable int.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The int value if the cast is valid and null if it's not.</returns>
        public static int? ToInt32Nullable(this string str)
        {
            if (str.IsNullOrEmpty()) return null;
            if (int.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        /// <summary>
        /// Converts a string to a nullable short.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The long value if the cast is valid and null if it's not.</returns>
        public static long? ToInt64Nullable(this string str)
        {
            if (str.IsNullOrEmpty()) return null;
            if (long.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        /// <summary>
        /// Extracts all digits from a string and converts them to a short value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as a short.</returns>
        public static short ExtractInt16(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt16();

        /// <summary>
        /// Extracts all digits from a string and converts them to an int value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as an int.</returns>
        public static int ExtractInt32(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt32();

        /// <summary>
        /// Extracts all digits from a string and converts them to a long value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as a long.</returns>
        public static long ExtractInt64(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt64();

        /// <summary>
        /// Extracts all digits from a string and converts them to a short value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as a short. returns null if there is no digits.</returns>
        public static short? ExtractInt16Null(this string str)
        {
            var digitsStr = new string(str.Where(char.IsDigit).ToArray());
            if (short.TryParse(digitsStr, out var result))
                return digitsStr.IsNullOrEmpty() ? null : digitsStr.ToInt16();
            else return null;
        }

        /// <summary>
        /// Extracts all digits from a string and converts them to an int value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as an int. returns null if there is no digits.</returns>
        public static int? ExtractInt32Null(this string str)
        {
            var digitsStr = new string(str.Where(char.IsDigit).ToArray());
            if (int.TryParse(digitsStr, out var result))
                return digitsStr.IsNullOrEmpty() ? null : digitsStr.ToInt32();
            else return null;
        }

        /// <summary>
        /// Extracts all digits from a string and converts them to a long value.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>The digits in a string as a long. returns null if there is no digits.</returns>
        public static long? ExtractInt64Null(this string str)
        {
            var digitsStr = new string(str.Where(char.IsDigit).ToArray());
            if (long.TryParse(digitsStr, out var result))
                return digitsStr.IsNullOrEmpty() ? null : digitsStr.ToInt64();
            else return null;
        }

        /// <summary>
        /// Checkes if a string has a valid email format.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmailValid(this string email)
        {
            email = email.Trim();
            if (email.EndsWith(".")) return false;
            try
            {
                return new MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
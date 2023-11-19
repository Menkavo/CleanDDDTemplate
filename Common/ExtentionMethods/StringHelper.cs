using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Common.ExtentionMethods
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (str == null) return true;
            return str.Replace(" ", "") == string.Empty;
        }

        public static bool IsNullOrEmpty(this string str) => str == null || str.Length == 0;

        public static bool IsNotNullOrEmpty(this string str) => str != null && str.Length != 0;

        public static string RemoveUnnecessarySpaces(this string value) => Regex.Replace(value, @"\s+", " ").Trim();

        public static short ToInt16(this string str)
        {
            if (short.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        public static int ToInt32(this string str)
        {
            if (int.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        public static long ToInt64(this string str)
        {
            if (long.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }


        public static short? ToInt16Nullable(this string str)
        {
            if (str.IsNullOrEmpty()) return null;
            if (short.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        public static int? ToInt32Nullable(this string str)
        {
            if (str.IsNullOrEmpty()) return null;
            if (int.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }

        public static long? ToInt64Nullable(this string str)
        {
            if (str.IsNullOrEmpty()) return null;
            if (long.TryParse(str, out var result)) return result;
            else throw new InvalidCastException();
        }


        public static short ExtractInt16(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt16();

        public static int ExtractInt32(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt32();

        public static long ExtractInt64(this string str) => new string(str.Where(char.IsDigit).ToArray()).ToInt64();

        public static short? ExtractInt16Null(this string str)
        {
            var intStr = new string(str.Where(char.IsDigit).ToArray());
            return intStr.IsNullOrEmpty() ? null : intStr.ToInt16();
        }

        public static int? ExtractInt32Null(this string str)
        {
            var intStr = new string(str.Where(char.IsDigit).ToArray());
            return intStr.IsNullOrEmpty() ? null : intStr.ToInt32();
        }

        public static long? ExtractInt64Null(this string str)
        {
            var intStr = new string(str.Where(char.IsDigit).ToArray());
            return intStr.IsNullOrEmpty() ? null : intStr.ToInt64();
        }

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
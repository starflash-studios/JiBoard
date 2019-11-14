using System;

namespace JiBoard {
    public static class Extensions {
        public static bool IsEmptyOrNull(this string s) => string.IsNullOrEmpty(s);
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        public static string Catch(this string s, string replacement) => s.IsEmptyOrNull() ? replacement : s;
        public static string Catch(this string s, string replacement, out bool caught) {
            if (s.IsEmptyOrNull()) {
                caught = true;
                return replacement;
            }

            caught = false;
            return s;
        }

        public static float Positive(this float a) => a < 0 ? -a : a;
        public static int Positive(this int a) => a < 0 ? -a : a;
        public static float Negative(this float a) => -a.Positive();
        public static int Negative(this int a) => -a.Positive();

        public static string Clipboard {
            get => System.Windows.Clipboard.GetText();
            set => System.Windows.Clipboard.SetText(value);
        }

        public static T Next<T>(this T src) where T : struct {
            if (!typeof(T).IsEnum) {
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");
            }

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(Arr, src) + 1;
            return (Arr.Length <= j) ? Arr[0] : Arr[j];
        }
        public static T Previous<T>(this T src) where T : struct {
            if (!typeof(T).IsEnum) {
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");
            }

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(Arr, src) - 1;
            return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
        }
    }
}

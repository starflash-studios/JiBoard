namespace JiBoard {
    public static class Extensions {
        public static bool IsEmptyOrNull(this string s) => string.IsNullOrEmpty(s);
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        public static string Catch(this string s, string replacement) => s.IsEmptyOrNull() ? replacement : s;
        public static string Catch(this string s, string replacement, out bool caught) {
            if (s.IsEmptyOrNull()) {
                caught = true;
                return replacement;
            } else {
                caught = false;
                return s;
            }
        }

        public static string Clipboard {
            get => System.Windows.Clipboard.GetText();
            set => System.Windows.Clipboard.SetText(value);
        }
    }
}

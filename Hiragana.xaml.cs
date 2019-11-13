using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JiBoard {
    public partial class MainWindow {
        public Dictionary<Hiragana, Button> importedHiragana = new Dictionary<Hiragana, Button>();
        public HiraganaType viewType;
        public bool viewSayings;
        public bool useInput;

        public MainWindow() {
            InitializeComponent();
            Width += 10;
            Height += 5;
            ManualImport();
            DebugAll();
            ChangeView(HiraganaType.Normal);
        }

        public void ManualImport() {
            Import(HbA);
            Import(HbI);
            Import(HbU);
            Import(HbE);
            Import(HbO);
            Import(HbKa);
            Import(HbKi);
            Import(HbKu);
            Import(HbKe);
            Import(HbKo);
            Import(HbSa);
            Import(HbSi);
            Import(HbSu);
            Import(HbSe);
            Import(HbSo);
            Import(HbTa);
            Import(HbTi);
            Import(HbTu);
            Import(HbTe);
            Import(HbTo);
            Import(HbNa);
            Import(HbNi);
            Import(HbNu);
            Import(HbNe);
            Import(HbNo);
            Import(HbHa);
            Import(HbHi);
            Import(HbHu);
            Import(HbHe);
            Import(HbHo);
            Import(HbMa);
            Import(HbMi);
            Import(HbMu);
            Import(HbMe);
            Import(HbMo);
            Import(HbYa);
            Import(HbYu);
            Import(HbYo);
            Import(HbRa);
            Import(HbRi);
            Import(HbRu);
            Import(HbRe);
            Import(HbRo);
            Import(HbWa);
            Import(HbWo);
            Import(HbN);
        }

        public void ChangeView(HiraganaType type, bool saying = false) {
            viewType = type;
            viewSayings = saying;
            Debug.WriteLine("Viewing: " + type + (saying ? "'s saying" : ""));
            foreach (KeyValuePair<Hiragana, Button> entry in importedHiragana) {
                entry.Value.Content = entry.Key.Get(type, saying, out bool m);
                entry.Value.Opacity = m ? 0.5 : 1;
                entry.Value.FontSize = saying ? 24 : 36;
            }
        }

        public void Import(Button button) => importedHiragana.Add(new Hiragana(button.Content.ToString()), button);

        public void DebugAll() {
            foreach (KeyValuePair<Hiragana, Button> entry in importedHiragana) {
                Debug.WriteLine(entry.Key);
            }
        }

        public void ReceiveHiragana(object sender, RoutedEventArgs e) {
            Hiragana h = importedHiragana.FirstOrDefault(x => x.Value == (Button)sender).Key;
            Debug.WriteLine("Got: " + h.ToString(true));

            string get = h.GetChar(viewType, out bool missing);
            if (missing) { get = h.Get(false); }
            Copy(get);
        }
        
        void ViewTypeCombo_SelectionChanged(object sender, RoutedEventArgs e) => ChangeView((HiraganaType)((ComboBox)sender).SelectedIndex, viewSayings);

        void ViewSayingsCheckbox_Click(object sender, RoutedEventArgs e) => ChangeView(viewType, ((CheckBox)sender).IsChecked ?? false);

        void Receive(object sender, RoutedEventArgs e) => Copy(((Button)sender).Content.ToString());

        public void Copy(string content) {
            if (content.IsEmptyOrNull()) { return; }
            if (useInput) {
                ClipTextInput.Text += content;
            } else {
                CurrentClipboard.Text = content;
                Extensions.Clipboard = content;
                Debug.WriteLine($"\t Copied '{content}' to clipboard buffer.");
            }
        }

        void UseInputCheck_Click(object sender, RoutedEventArgs e) {
            useInput = ((CheckBox)sender).IsChecked ?? false;
        }

        void ClipTextClear_Click(object sender, RoutedEventArgs e) {
            ClipTextInput.Text = "";
        }

        void ClipTextCopy_Click(object sender, RoutedEventArgs e) {
            string content = ClipTextInput.Text;
            if (content.IsEmptyOrNull()) { return; }
            CurrentClipboard.Text = content;
            Extensions.Clipboard = content;
            Debug.WriteLine($"\t Copied '{content}' to clipboard buffer.");
        }
    }

    public enum HiraganaType {
        Normal,
        Small,
        Handakuten,
        Dakuten
    }

    public struct Hiragana {
        public string norm;
        public string normS;
        public string small;
        public string smallS;
        public string han;
        public string hanS;
        public string dak;
        public string dakS;

        public Hiragana(string formatString) {
            string[] parts = formatString.Split("/"[0]);
            norm = parts[0].Split("-"[0])[0];
            normS = parts[0].Split("-"[0])[1];
            small = parts[1].Split("-"[0])[0];
            smallS = parts[1].Split("-"[0])[1];
            han = parts[2].Split("-"[0])[0];
            hanS = parts[2].Split("-"[0])[1];
            dak = parts[3].Split("-"[0])[0];
            dakS = parts[3].Split("-"[0])[1];
        }

        public Hiragana(string normal, string normalS, string small = null, string smallS = null, string handakuten = null, string handakutenS = null, string dakuten = null, string dakutenS = null) {
            norm = normal;
            normS = normalS;
            this.small = small;
            this.smallS = smallS;
            han = handakuten;
            hanS = handakutenS;
            dak = dakuten;
            dakS = dakutenS;
        }

        public string GetChar(HiraganaType type) {
            switch (type) {
                case HiraganaType.Small:
                    return small;
                case HiraganaType.Handakuten:
                    return han;
                case HiraganaType.Dakuten:
                    return dak;
                default:
                    return norm;
            }
        }

        public string GetSaying(HiraganaType type) {
            switch (type) {
                case HiraganaType.Small:
                    return smallS;
                case HiraganaType.Handakuten:
                    return hanS;
                case HiraganaType.Dakuten:
                    return dakS;
                default:
                    return normS;
            }
        }

        public string GetChar(HiraganaType type, out bool missing) {
            switch (type) {
                case HiraganaType.Small:
                    return small.Catch(norm, out missing);
                case HiraganaType.Handakuten:
                    return han.Catch(norm, out missing);
                case HiraganaType.Dakuten:
                    return dak.Catch(norm, out missing);
                default:
                    return norm.Catch(norm, out missing);
            }
        }

        public string GetSaying(HiraganaType type, out bool missing) {
            switch (type) {
                case HiraganaType.Small:
                    return smallS.Catch("-", out missing);
                case HiraganaType.Handakuten:
                    return hanS.Catch("-", out missing);
                case HiraganaType.Dakuten:
                    return dakS.Catch("-", out missing);
                default:
                    return normS.Catch("-", out missing);
            }
        }

        public string Get(HiraganaType type, bool saying) => saying ? GetSaying(type) : GetChar(type);

        public string Get(HiraganaType type, bool saying, out bool missing) => saying ? GetSaying(type, out missing) : GetChar(type, out missing);

        public string Get(bool saying) => saying ? normS : norm;

        public override string ToString() {
            string format = $"{norm} ({normS})";
            if (!small.IsEmptyOrNull()) { format += $" | {small} ({smallS})"; }
            if (!han.IsEmptyOrNull()) { format += $" | {han} ({hanS})"; }
            if (!dak.IsEmptyOrNull()) { format += $" | {dak} ({dakS})"; }
            return format;
        }

        public string ToString(bool expand) {
            string format = $"{norm} ({normS})";
            if (!small.IsEmptyOrNull()) { format += $" | {(expand ? "Small: " : "")}{small} ({smallS})"; }
            if (!han.IsEmptyOrNull()) { format += $" | {(expand ? "Handakuten: " : "")}{han} ({hanS})"; }
            if (!dak.IsEmptyOrNull()) { format += $" | {(expand ? "Dakuten: " : "")}{dak} ({dakS})"; }
            return format;
        }
    }
}

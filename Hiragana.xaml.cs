//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program. If not, see<https://www.gnu.org/licenses/>.

//Starflash Studios, hereby disclaims all copyright interest in the program 'JiBoard' (which is a portable Japanese IME) written by Cody Bock.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Octokit;

namespace JiBoard {
    public partial class MainWindow {
        public Dictionary<Hiragana, Button> importedHiragana = new Dictionary<Hiragana, Button>();
        public HiraganaType viewType;
        public bool viewSayings;
        public bool useInput;
        public bool updateRequired;
        public Language currentLang;

        public DirectoryInfo appPath;
        public FileInfo appFile;

        public static Version currentVersion = new Version(0, 2, 3);

        public MainWindow() {
            InitializeComponent();
            appFile = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            appPath = appFile.Directory;
            Width += 10;
            Height += 5;
            ManualImport();
#if DEBUG
            DebugAll();
#endif
            switch (System.Windows.Forms.InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName.ToLowerInvariant()) {
                case "en":
                    ChangeLanguage(JiBoard.Language.English);
                    DisplayLanguage.SelectedIndex = 1;
                    break;
                default:
                    ChangeLanguage(JiBoard.Language.Japanese);
                    DisplayLanguage.SelectedIndex = 0;
                    break;

            }
            ChangeView(HiraganaType.Normal);
            UpdateGrid.Visibility = Visibility.Hidden;
            Dispatcher.Invoke(CheckForUpdate);
        }

        public async Task CheckForUpdate() {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("JiBoard"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("starflash-studios", "JiBoard");
            foreach(Release release in releases) {
                Debug.WriteLine("Found release: " + release.TagName + " >> " + release.Name);
            }
            Version latest = new Version(releases[0]);
            Debug.WriteLine("Latest version: " + latest);
            Debug.WriteLine("Current version: " + currentVersion);
            updateRequired = currentVersion.IsOlder(latest);
            if (updateRequired) {
                Debug.WriteLine("Update required");
                UpdateGrid.Visibility = Visibility.Visible;
            } else {
                Debug.WriteLine("Running latest version");
                UpdateGrid.Visibility = Visibility.Hidden;
            }
            ChangeLanguage(currentLang);
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

        public void ChangeInputMethod(bool useInput) {
            this.useInput = useInput;
            ClipTextInput.IsEnabled = useInput;
            ClipTextClear.IsEnabled = useInput;
        }

        public void ChangeView(HiraganaType type, bool saying = false) {
            viewType = type;
            viewSayings = saying;
            Dispatcher.Invoke(InternalChangeView);
        }

        async Task InternalChangeView() {
            Debug.WriteLine("Viewing: " + viewType + (viewSayings ? "'s saying" : ""));
            foreach (KeyValuePair<Hiragana, Button> entry in importedHiragana) {
                entry.Value.Content = entry.Key.Get(viewType, viewSayings, out bool m);
                entry.Value.Opacity = m ? 0.5 : 1;
                entry.Value.FontSize = viewSayings ? 16 : 36;
            }
            ViewTypeCombo.SelectedIndex = (int)viewType;
            ViewSayingsCheckbox.IsChecked = viewSayings;
            await Task.Yield();
        }

        public void ChangeLanguage(Language lang) {
            currentLang = lang;
            switch (lang) {
                case JiBoard.Language.Japanese:
                    Title = "じーぼーど　»　ひらがな　【" + currentVersion + "】";
                    TransHeading.Content = "ひらがな";
                    TransHeading.FontSize = 36;
                    ViewSayingsCheckbox.Content = "はつおん";
                    ViewSayingsCheckbox.FontSize = 18;
                    ((ComboBoxItem)ViewTypeCombo.Items[0]).Content = "ひらがな";
                    ((ComboBoxItem)ViewTypeCombo.Items[1]).Content = "ちさなひらがな";
                    ((ComboBoxItem)ViewTypeCombo.Items[2]).Content = "はんだくてん";
                    ((ComboBoxItem)ViewTypeCombo.Items[3]).Content = "だくてん";
                    ViewTypeCombo.FontSize = 12;
                    TransClipboardRegion.Content = "くりぷぼっど";
                    ClipTextCopy.Content = "こぴぃ";
                    UpdateButton.Content = "いま　すぐ\nあっぷでと";
                    break;
                case JiBoard.Language.English:
                    Title = "JiBoard » Hiragana [v" + currentVersion + "]";
                    TransHeading.Content = "Hiragana";
                    TransHeading.FontSize = 32;
                    ViewSayingsCheckbox.Content = "Pronunciation";
                    ViewSayingsCheckbox.FontSize = 10;
                    ((ComboBoxItem)ViewTypeCombo.Items[0]).Content = "Normal";
                    ((ComboBoxItem)ViewTypeCombo.Items[1]).Content = "Small";
                    ((ComboBoxItem)ViewTypeCombo.Items[2]).Content = "Handakuten";
                    ((ComboBoxItem)ViewTypeCombo.Items[3]).Content = "Dakuten";
                    ViewTypeCombo.FontSize = 11;
                    TransClipboardRegion.Content = "Clipboard";
                    ClipTextCopy.Content = "Copy";
                    UpdateButton.Content = "Update Now";
                    break;
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

        void UseInputCheck_Click(object sender, RoutedEventArgs e) => ChangeInputMethod(((CheckBox)sender).IsChecked ?? false);

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

        void DisplayLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e) => ChangeLanguage((Language)((ComboBox)sender).SelectedIndex);

        void UpdateButton_Click(object sender, RoutedEventArgs e) {
            Process.Start("explorer.exe", $"/select,\"{System.Reflection.Assembly.GetEntryAssembly().Location}\"");
            Process.Start(@"https://github.com/starflash-studios/JiBoard/releases");
        }

        void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            switch (e.Key) {
                case System.Windows.Input.Key.Tab:
                    ChangeView(viewType.Next(), viewSayings);
                    e.Handled = true;
                    break;
                case System.Windows.Input.Key.OemTilde:
                    ChangeView(viewType, !viewSayings);
                    e.Handled = true;
                    break;
            }
        }
    }

    public struct Version {
        public int major;
        public int sector;
        public int minor;
        public int fix;

        public override string ToString() => $"{major}.{sector}.{minor}.{fix}";

        public Version(int maj = 0, int min = 0, int sec = 0, int typ = 0) {
            major = maj;
            sector = min;
            minor = sec;
            fix = typ;
        }

        public Version(string parse) {
            string[] split = parse.Split("."[0]);
            major = int.Parse(split[0]);
            sector = int.Parse(split[1]);
            minor = int.Parse(split[2]);
            fix = int.Parse(split[3]);
        }

        public Version(Release release) {
            string[] split = release.TagName.Split("."[0]);
            major = int.Parse(split[0]);
            sector = int.Parse(split[1]);
            minor = int.Parse(split[2]);
            fix = int.Parse(split[3]);
        }

        public Version EnforcePositive() => new Version(major.Positive(), sector.Positive(), minor.Positive(), fix.Positive());

        /// <summary>
        /// Returns false if 'other' is a higher version
        /// </summary>
        /// <param name="other"></param>
        /// <param name="levelOfScrutiny">0: Any value, 1: Security or above, 2: Minor or above, 3: Major or above</param>
        /// <returns></returns>
        public bool IsNewer(Version other, int levelOfScrutiny = 0) => !IsOlder(other, levelOfScrutiny);

        /// <summary>
        /// Returns true if 'other' is a higher version
        /// </summary>
        /// <param name="other"></param>
        /// <param name="levelOfScrutiny">0: Any value, 1: Security or above, 2: Minor or above, 3: Major or above</param>
        /// <returns></returns>
        public bool IsOlder(Version other, int levelOfScrutiny = 0) {
            switch (levelOfScrutiny) {
                case 1:
                    return other.major > major || other.sector > sector || other.minor > minor;
                case 2:
                    return other.major > major || other.sector > sector;
                case 3:
                    return other.major > major;
                default:
                    return other.major > major || other.sector > sector || other.minor > minor || other.fix > fix;
            }
        }
    }

    public enum Language {
        Japanese,
        English
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

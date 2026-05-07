using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hotel_CheckIn.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            BackupCheckBox.IsChecked = GlobalSettings.BackupEnabled;

            if (GlobalSettings.CurrentTheme == "Light Theme")
                ThemeComboBox.SelectedIndex = 1;
            else
                ThemeComboBox.SelectedIndex = 0;
        }

        private void BackupCheckBox_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.BackupEnabled = BackupCheckBox.IsChecked == true;
        }

        private void ThemeComboBox_DropDownClosed(object sender, System.EventArgs e)
        {
            if (ThemeComboBox.SelectedItem == null) return;

            string selectedTheme = (ThemeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

            if (selectedTheme == GlobalSettings.CurrentTheme)
                return;

            GlobalSettings.CurrentTheme = selectedTheme;

            // Promijeni boje
            if (selectedTheme == "Light Theme")
            {
                Application.Current.Resources["BgDark"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F1F5F9"));
                Application.Current.Resources["SidebarDark"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                Application.Current.Resources["ElementBg"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                Application.Current.Resources["TextPrimary"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0F172A"));
                Application.Current.Resources["TextSecondary"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64748B"));
            }
            else
            {
                Application.Current.Resources["BgDark"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0F172A"));
                Application.Current.Resources["SidebarDark"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#020617"));
                Application.Current.Resources["ElementBg"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E293B"));
                Application.Current.Resources["TextPrimary"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8FAFC"));
                Application.Current.Resources["TextSecondary"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8"));
            }
        }
    }
}
using System.Windows.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class ThemeSettings : UserControl {
  public ThemeSettings() {
    DataContext = new ThemeSettingsViewModel();
    InitializeComponent();
  }
}
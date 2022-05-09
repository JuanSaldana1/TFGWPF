using System.Windows.Controls;

namespace WpfApp3.View;

public partial class ColoresUserControl : UserControl {
  public ColoresUserControl() {
    InitializeComponent();
    var paletteSelector = new PaletteSelector();
    PaletteSelectorUserControl.Content = paletteSelector;
  }
}
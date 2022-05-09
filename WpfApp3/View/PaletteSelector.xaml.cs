using System.Windows.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class PaletteSelector : UserControl {
  public PaletteSelector() {
    DataContext = new PaletteSelectorViewModel();
    InitializeComponent();
  }
}
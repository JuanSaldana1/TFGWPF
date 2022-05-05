using System.Windows;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class ConsultasUserControl {
  public ConsultasUserControl() {
    InitializeComponent();
  }

  private void ButtonSelectAllProductsOnClick(object sender, RoutedEventArgs e) {
    var productViewModel = new ProductViewModel();
    ListViewResultados.DataContext = new ProductViewModel();
  }
}
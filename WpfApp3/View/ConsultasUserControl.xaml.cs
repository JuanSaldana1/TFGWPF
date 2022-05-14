using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class ConsultasUserControl {
  public ConsultasUserControl() {
    InitializeComponent();
    CategoriesDataGrid.ItemsSource = new ProductViewModel().Productos;
  }
}
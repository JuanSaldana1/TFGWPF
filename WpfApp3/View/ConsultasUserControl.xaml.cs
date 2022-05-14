using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class ConsultasUserControl {
  public ConsultasUserControl() {
    InitializeComponent();
    DataContext = new ProductViewModel();
    /*AnimalListBox.ItemsSource = new ProductViewModel().Productos;*/
  }

  /*private void ButtonSelectAllProductsOnClick(object sender, RoutedEventArgs e) {
    var productViewModel = new ProductViewModel();
    ListViewResultados.DataContext = new ProductViewModel();
  }*/

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    Debug.WriteLine($"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

    //you can cancel the dialog close:
    //eventArgs.Cancel();

    if (!Equals(eventArgs.Parameter, true))
      return;

    if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
      AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
  }
}
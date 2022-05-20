using MaterialDesignExtensions.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class ConsultasUserControl {
  ProductViewModel productoViewModel = new ProductViewModel();

  public ConsultasUserControl() {
    InitializeComponent();
    /*CategoriesDataGrid.ItemsSource = new ProductViewModel().Productos;*/
  }

  private void SearchHandler1(object sender, SearchEventArgs args) {
    productoViewModel.SearchAllSelecrQuery(args.SearchTerm);
    searchResultTextBlock1.Text = "Your are looking for '" + args.SearchTerm + "'.";
    ConsultasDataGrid.ItemsSource = new ProductViewModel().Productos;
  }
}
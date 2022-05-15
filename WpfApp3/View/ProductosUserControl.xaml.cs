using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using NLog;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;


namespace WpfApp3.View;

/// <summary>
///   Lógica de interacción para ProductosUserControl.xaml
/// </summary>
public partial class ProductosUserControl {
  private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

  public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent(
    "DialogClosing1",
    RoutingStrategy.Bubble,
    typeof(DialogClosingEventHandler),
    typeof(DialogHost)
  );

  public ProductosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new ProductViewModel();
  }

  private void Button_Click(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
  }

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show(nIe.Message);
    }

    return null;
  }

  private void ArticuloChanged(object sender, TextChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue($"BasicRatingBar value changed from {e.Changes}");
  }

  private void ButtonAddStock_OnClick(object sender, RoutedEventArgs e) {
    var viewModel = new ProductViewModel();
    SnackbarSeven.MessageQueue?.Enqueue("Has aumentado tu stock en 1");
  }

  private void ButtonRestStock_OnClick(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Has reducido tu stock en 1");
  }

  private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");
  }

  private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e) {
    Debug.WriteLine($"La valoración ha cambiado de {e.OldValue} a {e.NewValue} estrellas.");
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
    var viewModel = new ProductViewModel();
    MessageBox.Show(viewModel.Productos.Count.ToString());
    MessageBox.Show(viewModel.Productos.ToArray().ToString());
    MessageBox.Show(viewModel.SelectedProductos.ToString());
    MessageBox.Show(viewModel.SelectedProductos.Remove(new ProductoModel()).ToString());
  }

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var producto = new ProductoModel();
    var viewModel = new ProductViewModel();
    try {
      producto.ProductName = ProductNameEditText.Text;
      producto.ProductDescription = ProductDescriptionEditText.Text;
      producto.ProductPrice = Convert.ToDecimal(ProductPriceEditText.Text);
      producto.CategoryId = Convert.ToInt32(ProductCategoryIdEditText.Text);
      producto.ProductStock = Convert.ToInt32(ProductStockEditText.Text);
      producto.ProductRating = Convert.ToInt32(ProductRatingEditText.Text);
      producto.ProductImage = ProductImageEditText.Text;
      producto.PostId = Convert.ToInt32(ProductPostIdEditText.Text);
      try {
        SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(producto)
          ? "Producto insertado correctamente"
          : "Error al insertar el producto");
        viewModel.GetAllProducts();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }
}
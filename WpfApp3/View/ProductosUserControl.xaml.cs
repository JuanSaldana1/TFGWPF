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
    if (!Equals(eventArgs.Parameter, true))
      return;

    if (!string.IsNullOrWhiteSpace(ProductNameEditText.Text) ||
        !string.IsNullOrWhiteSpace(ProductDescriptionEditText.Text) ||
        !string.IsNullOrWhiteSpace(ProductPriceEditText.Text) ||
        !string.IsNullOrWhiteSpace(ProductStockEditText.Text) ||
        !string.IsNullOrWhiteSpace(ProductCategoryIdEditText.Text)) {
      try {
        var newProduct = new ProductoModel();
        var viewModel = new ProductViewModel();
        newProduct.ProductName = ProductNameEditText.Text;
        newProduct.ProductDescription = ProductDescriptionEditText.Text;
        newProduct.ProductPrice = Convert.ToDouble(ProductPriceEditText.Text);
        newProduct.CategoryId = Convert.ToInt32(ProductCategoryIdEditText.Text);
        newProduct.ProductStock = Convert.ToInt32(ProductStockEditText.Text);
        newProduct.ProductRating = Convert.ToInt32(ProductRatingEditText.Text);
        newProduct.ProductImage = ProductImageEditText.Text;
        newProduct.PostId = Convert.ToInt32(ProductPostIdEditText.Text);
        MyListView.Items.Add(new ProductoModel {
          CategoryId = Convert.ToInt32(ProductCategoryIdEditText.Text),
          ProductDescription = ProductDescriptionEditText.Text,
          ProductImage = ProductImageEditText.Text,
          ProductName = ProductNameEditText.Text,
          ProductPrice = Convert.ToDouble(ProductPriceEditText.Text),
          ProductRating = Convert.ToInt32(ProductRatingEditText.Text),
          ProductStock = Convert.ToInt32(ProductStockEditText.Text),
          PostId = Convert.ToInt32(ProductPostIdEditText.Text)
        });
        viewModel.Productos.Add(newProduct);
        MyListView.Items.Refresh();
        SnackbarSeven.MessageQueue?.Enqueue("Producto añadido correctamente");
      }
      catch (Exception e) {
        MessageBox.Show(e.Message);
        throw;
      }
    }
  }
}
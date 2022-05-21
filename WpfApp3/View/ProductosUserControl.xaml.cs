using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CRMLasDiademasDeMisHijas.View;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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

  private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e) {
    Debug.WriteLine($"La valoración ha cambiado de {e.OldValue} a {e.NewValue} estrellas.");
  }

  private void ButtonDeleteProduct_OnClick(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item.ProductId;
      var viewModel = new ProductViewModel();
      try {
        if (!id.Equals("") || !id.Equals(null)) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.DeleteMethod(id)
            ? $"Producto con id {id} eliminado correctamente"
            : $"Error al Eliminar el producto con id {id}");
        }

        MyListView.DataContext = new ProductViewModel();
        viewModel.GetProductsForView();
      }
      catch (Exception exception) {
        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
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
        if (ProductNameEditText.Text != "") {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(producto)
            ? "Producto insertado correctamente"
            : "Error al insertar el producto");
        }

        MyListView.DataContext = new ProductViewModel();
        viewModel.GetProductsForView();
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

  private void ButtonCreateReport_OnClick(object sender, RoutedEventArgs e) {
    CreateProductReport();
  }

  private static void CreateProductReport() {
    var pdfWriter = new PdfWriter("Reporte producto.pdf");
    var pdf = new PdfDocument(pdfWriter);
    var document = new Document(pdf, PageSize.A4);
    document.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
    document.SetMargins(60, 20, 55, 20);
    var parragraf = new Paragraph("Reporte de productos");
    document.Add(parragraf);
    document.Close();
  }

  private void BurronCreateQrCode_OnClick(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item.ProductId;
      var viewModel = new ProductViewModel();
      var contenido = viewModel.SelectAllFromProductId(id);
      var encoder = new QrEncoder(ErrorCorrectionLevel.H);
      var qrCode = new QrCode();
      encoder.TryEncode(contenido, out qrCode);
      var wRenderer =
        new WriteableBitmapRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Colors.Black, Colors.White);
      var wBitmap = new WriteableBitmap((int) 70, (int) 70, 70, 70, PixelFormats.Gray8, null);
      wRenderer.Draw(wBitmap, qrCode.Matrix);
      var qrCodeWindow = new QRCodeAcrylicWindow();
      qrCodeWindow.QrImage.Source = wBitmap;
      qrCodeWindow.Show();
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }
}
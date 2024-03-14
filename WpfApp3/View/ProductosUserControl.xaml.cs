using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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

  private void ButtonDeleteProduct_OnClick(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item?.ProductId;
      var viewModel = new ProductViewModel();
      try {
        if (!(id?.Equals("") || id?.Equals(null))) {
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
        if (!string.IsNullOrEmpty(ProductNameEditText.Text)) {
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
    var button = sender as Button;
    dynamic item = button?.DataContext;
    var id = item?.ProductId;
    var viewModel = new ProductViewModel();
    viewModel.SelectAllFromProductId(id);
  }

  /*private void BurronCreateQrCode_OnClick(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item.ProductId;
      var viewModel = new ProductViewModel();
      var contenido = viewModel.SelectAllFromProductId(id);
      var qrCodeGenerator = new QRCodeGenerator();
      var qrCodeData = qrCodeGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
      var qrCode = new QRCode(qrCodeData);
      var qrCodeImage = qrCode.GetGraphic(20);
      var qrCodeWindow = new QRCodeAcrylicWindow();
      qrCodeWindow.QrImage.Source = ToBitmapImage(qrCodeImage);
      /*var encoder = new QRCodeEncoder();
      encoder.QRCodeScale = 8;
      var bitmap = encoder.Encode(contenido);
      var qrCodeWindow = new QRCodeAcrylicWindow();
      var qrGenerator = new QRCodeGenerator();
      var qrCodeData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
      var qrCode = new QRCode(qrCodeData);
      var qrCodeImage = qrCode.GetGraphic(20);
      var bytes = (byte[]) (new ImageConverter()).ConvertTo(qrCodeImage, typeof(byte[]));
      var bitmapImage = ConvertByteArrayToBitMapImage(bytes);
      qrCodeWindow.Show();
      qrCodeWindow.QrImage.Source = bitmapImage;#1#

      /*var encoder = new QrEncoder(ErrorCorrectionLevel.H);
      var qrCode = new QrCode();
      encoder.TryEncode(contenido, out qrCode);
      var wRenderer =
        new WriteableBitmapRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Colors.Black, Colors.White);
      var wBitmap = new WriteableBitmap((int) 70, (int) 70, 70, 70, PixelFormats.Gray8, null);
      wRenderer.Draw(wBitmap, qrCode.Matrix);
      var qrCodeWindow = new QRCodeAcrylicWindow();
      qrCodeWindow.QrImage.Source = wBitmap;
      qrCodeWindow.Show();#1#
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }*/

  /*public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray) {
    var img = new BitmapImage();
    using (MemoryStream memStream = new MemoryStream(imageByteArray)) {
      img.BeginInit();
      img.CacheOption = BitmapCacheOption.OnLoad;
      img.StreamSource = memStream;
      img.EndInit();
      img.Freeze();
    }

    return img;
  }*/

  /*private void ButtonCreateBarCode_OnClick(object sender, RoutedEventArgs e) {
    var qrCodeWindow = new QRCodeAcrylicWindow();
    qrCodeWindow.BarcodeControl.Value = "123456789";
    qrCodeWindow.Show();
  }*/

  /*private ImageSource ToBitmapImage(Bitmap bitmap) {
    using (var memory = new MemoryStream()) {
      bitmap.Save(memory, ImageFormat.Bmp);
      memory.Position = 0;
      var bitmapImage = new BitmapImage();
      bitmapImage.BeginInit();
      bitmapImage.StreamSource = memory;
      bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
      bitmapImage.EndInit();
      return Windows.UI.Xaml.Media.ImageSource(bitmapImage);
    }
  }*/
  private void BasicRatingBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
  {
    Debug.WriteLine($"La valoración ha cambiado de {e.OldValue} a {e.NewValue} estrellas.");
  }
}
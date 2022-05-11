using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class ComentariosUserControl {
  public ComentariosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new CommentViewModel();
  }

  private void Button_Click(object sender, RoutedEventArgs e) {
    /*var updateCommentQuery =
      "UPDATE Comentarios SET Titulo = '" + EditTextCommentTitle.Text + "', Comentario = '" + ComentarioTextBox.Text + "';";*/
    SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
  }

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show(nIe.Message);
    }

    return null;
  }

  private void ButtonEliminarComentario_Click(object sender, RoutedEventArgs e) {
    MessageBox.Show(sender + "\n" + sender.GetType() + "\n" + e + "\n" + e.Source + "\n" +
                    e.Source.GetType());
    MessageBox.Show(MyListView.SelectedItem.ToString());
    MessageBox.Show(MyListView.SelectedItem.GetHashCode().ToString());
    MessageBox.Show(MyListView.SelectedItem.GetType().ToString());
    MessageBox.Show(MyListView.SelectedItems.ToString());
    MessageBox.Show(MyListView.Uid);
    MessageBox.Show(MyListView.Items.ToString());
    MessageBox.Show(MyListView.PersistId.ToString());
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      var deleteQuery = "DELETE FROM Comentarios WHERE IdComentario = '" + MyListView.SelectedItem.GetHashCode() + "';";
      var myCommand = new MySqlCommand(deleteQuery, conexionBd);
      conexionBd.Open();
      myCommand.ExecuteReader();
    }
    catch (Exception ex) {
      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
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
        var newProduct = new CommentModel();
        var viewModel = new ProductViewModel();
        /*newProduct.ProductName = ProductNameEditText.Text;
        newProduct.ProductDescription = ProductDescriptionEditText.Text;
        newProduct.ProductPrice = Convert.ToDouble(ProductPriceEditText.Text);
        newProduct.CategoryId = Convert.ToInt32(ProductCategoryIdEditText.Text);
        newProduct.ProductStock = Convert.ToInt32(ProductStockEditText.Text);
        newProduct.ProductRating = Convert.ToInt32(ProductRatingEditText.Text);
        newProduct.ProductImage = ProductImageEditText.Text;
        newProduct.PostId = Convert.ToInt32(ProductPostIdEditText.Text);*/
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
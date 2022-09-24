using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class ComentariosUserControl {
  public ComentariosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new CommentViewModel();
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

  private void ButtonDeleteComment_Click(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item?.CommentId;
      var viewModel = new CommentViewModel();
      try {
        if (!(id?.Equals("") || id?.Equals(null))) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.DeleteMethod(id)
            ? $"Comentario con id {id} eliminado correctamente"
            : $"Error al Eliminar el comentario con id {id}");
        }

        MyListView.DataContext = new CommentViewModel();
        viewModel.GetCommentsForView();
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
}
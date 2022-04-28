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
}
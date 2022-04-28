using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

/// <summary>
/// Lógica de interacción para ComentariosUserControl.xaml
/// </summary>
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

  private void CommentChanged(object sender, TextChangedEventArgs e) {
    Console.WriteLine("Comentario cambiado");
  }
}
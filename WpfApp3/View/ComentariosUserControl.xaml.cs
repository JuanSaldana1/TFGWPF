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
}
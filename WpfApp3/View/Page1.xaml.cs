using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MySql.Data.MySqlClient;

namespace WpfApp3.View;

public partial class Page1 : Page {
  public Page1() {
    InitializeComponent();
  }

  private void ToggleButton_OnChecked(object sender, RoutedEventArgs e) {
    var toogleButton = sender as ToggleButton;
    var toogleButton1 = ToggleButtonFavorito;
    SnackbarSeven.MessageQueue?.Enqueue(sender.GetHashCode().ToString());
    SnackbarSeven.MessageQueue?.Enqueue(toogleButton.Name);
    SnackbarSeven.MessageQueue?.Enqueue(toogleButton.Content.ToString());

    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      conexionBd.Open();
      var isFavouriteQuery = "Update Posts SET Favorito = true WHERE IdPost ='" + ((ToggleButton) sender).Tag + "';";
      var myCommand = new MySqlCommand(isFavouriteQuery, conexionBd);
      myCommand.ExecuteNonQuery();
      conexionBd.Close();
      SnackbarSeven.MessageQueue?.Enqueue("Ahora este post está en favoritos");
    }
    catch (Exception exception) {
      Console.WriteLine(exception);
      throw;
    }
  }
}
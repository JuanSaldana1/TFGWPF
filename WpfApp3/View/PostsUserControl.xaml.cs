using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class PostsUserControl {
  public PostsUserControl() {
    InitializeComponent();
    MyListView.DataContext = new PostViewModel();
    // PostComponentPage.DataContext = new PostViewModel();
  }

  private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) {
    // for .NET Core you need to add UseShellExecute = true
    // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
    e.Handled = true;
  }

  public void ButtonUpdateChanges_Click(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("");
  }

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show(nIe.Message);
    }

    return null;
  }

  public void PostChanged(object sender, TextChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue(e.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(e.Changes.ToString());
  }

  private void ToggleButton_OnChecked(object sender, RoutedEventArgs e) {
    var toogleButton = sender as ToggleButton;
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
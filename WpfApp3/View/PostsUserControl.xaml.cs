using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class PostsUserControl {
  public PostsUserControl() {
    InitializeComponent();
    MyListView.DataContext = new PostViewModel();
  }

  public void ButtonUpdateChanges_Click(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("");
  }

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var post = new PostModel();
    var viewModel = new PostViewModel();
    try {
      post.PostTitle = ProductNameEditText.Text;
      post.PostDescription = ProductDescriptionEditText.Text;
      post.PostUrl = ProductUrlEditText.Text;
      post.PostPublishDate = Convert.ToDateTime(PostPublishDatePicker.Text);
      post.IsFavorite = PostIsFavoriteCheckBox.IsChecked.Value;
      post.PostFirstImage = PostFirstImageEditText.Text;
      post.PostSecondImage = PostSecondImageEditText.Text;
      try {
        if (
          ProductDescriptionEditText.Text != "" && ProductNameEditText.Text != "" && ProductUrlEditText.Text != "" ||
          PostPublishDatePicker.Text != "" && PostFirstImageEditText.Text != "" && PostSecondImageEditText.Text != ""
        ) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(post)
            ? "Post Creado correctamente"
            : "Error al crear el post");
        }

        MyListView.DataContext = new PostViewModel();
        viewModel.GetPostsForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
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
      var isFavouriteQuery = "Update Posts SET IsFavorite = true WHERE PostId ='" + ((ToggleButton) sender).Tag + "';";
      var myCommand = new MySqlCommand(isFavouriteQuery, conexionBd);
      myCommand.ExecuteNonQuery();
      conexionBd.Close();
      SnackbarSeven.MessageQueue?.Enqueue("Ahora este post está en favoritos");
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }
}
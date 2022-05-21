using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using NHibernate.Util;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

/// <summary>
///   Lógica de interacción para UsuariosUserControl.xaml
/// </summary>
public partial class UsuariosUserControl {
  public static string ImagenPerfil { get; set; }

  public UsuariosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new UserViewModel();
  }


  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var usuario = new UserModel();
    var viewModel = new UserViewModel();
    try {
      usuario.Username = UserUsernameEditText.Text;
      usuario.Name = UserNameEditText.Text;
      usuario.Surname = UserSurnameEditText.Text;
      usuario.Email = UserEmailEditText.Text;
      usuario.Rol = UserRolComboBox.Text;
      usuario.Follower = (IsFollowerCheckBox.IsChecked.Value) ? "T" : "F";
      usuario.ProfilePhoto = UserProfileImageEditText.Text;
      try {
        if (UserUsernameEditText.Text != "" && UserNameEditText.Text != "" || UserSurnameEditText.Text != "" ||
            UserEmailEditText.Text != "" && UserRolComboBox.Text != "" || UserProfileImageEditText.Text != "") {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(usuario)
            ? "Usuario creado correctamente"
            : "Error al crear el usuario");
          MyListView.DataContext = new UserViewModel();
          viewModel.GetUsersForView();
        }
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

  public void ButtonSave_Click(object sender, RoutedEventArgs e) { }

  private void BtnOpenFile_Click(object sender, RoutedEventArgs e) {
    var openFileDialog = new OpenFileDialog();
    openFileDialog.DefaultExt = ".png"; // Default file extension
    openFileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg"; // Filter files by extension
    openFileDialog.ShowDialog();
    SnackbarSeven.MessageQueue?.Enqueue(openFileDialog.FileName);
    ImagenPerfil = openFileDialog.FileName;
  }

  private void ButtonDeleteUser_OnClick(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item.UserId;
      var viewModel = new UserViewModel();
      try {
        if (!id.Equals("") || !id.Equals(null)) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.DeleteMethod(id)
            ? $"Usuario con id {id} eliminado correctamente"
            : $"Error al Eliminar el usuario con id {id}");
        }

        MyListView.DataContext = new UserViewModel();
        viewModel.GetUsersForView();
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
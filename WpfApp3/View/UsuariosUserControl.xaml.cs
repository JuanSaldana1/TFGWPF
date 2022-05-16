using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
        viewModel.InsertMethod(usuario);
        MyListView.DataContext = new UserViewModel();
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }

  public void ButtonSave_Click(object sender, RoutedEventArgs e) {
    var listaUsuarios = new UserViewModel();
    var nuevoUsuario = new UserModel();
    /*nuevoUsuario.Name = EditTextUserName.Text;
    SnackbarSeven.MessageQueue?.Enqueue("Se ha creado el usuario con nombre: " + nuevoUsuario.Name);
    SnackbarSeven.MessageQueue?.Enqueue(sender.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(e.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(EditTextUserName.Text + "\n");
    listaUsuarios.ChangeName(EditTextUserName.Text, 3);
    listaUsuarios.Usuarios.Add(nuevoUsuario);*/
  }

  private void BtnOpenFile_Click(object sender, RoutedEventArgs e) {
    var openFileDialog = new OpenFileDialog();
    openFileDialog.DefaultExt = ".png"; // Default file extension
    openFileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg"; // Filter files by extension
    openFileDialog.ShowDialog();
    SnackbarSeven.MessageQueue?.Enqueue(openFileDialog.FileName);
    ImagenPerfil = openFileDialog.FileName;
  }
}
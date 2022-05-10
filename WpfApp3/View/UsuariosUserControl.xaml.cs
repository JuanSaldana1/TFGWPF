using System.Windows;
using Microsoft.Win32;
using WpfApp3.Model;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

/// <summary>
///   Lógica de interacción para UsuariosUserControl.xaml
/// </summary>
public partial class UsuariosUserControl {
  public UsuariosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new UserViewModel();
  }

  public static string ImagenPerfil { get; set; }

  public void ButtonSave_Click(object sender, RoutedEventArgs e) {
    var listaUsuarios = new UserViewModel();
    var nuevoUsuario = new UserModel();
    nuevoUsuario.Name = EditTextUserName.Text;
    SnackbarSeven.MessageQueue?.Enqueue("Se ha creado el usuario con nombre: " + nuevoUsuario.Name);
    SnackbarSeven.MessageQueue?.Enqueue(sender.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(e.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(EditTextUserName.Text + "\n");
    listaUsuarios.ChangeName(EditTextUserName.Text, 3);
    listaUsuarios.Usuarios.Add(nuevoUsuario);
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
using System.Windows;
using MaterialDesignThemes.Wpf;
using PaletteHelper = MaterialDesignExtensions.Themes.PaletteHelper;

namespace WpfApp3.View;

/// <summary>
/// Lógica de interacción para ListaYCRUD.xaml
/// </summary>
public partial class ListaYCRUD {
  public ListaYCRUD() {
    InitializeComponent();
    TextViewNombreBaseDatos.Text = "Estás editando " + MainWindow.BaseDatos;
    var productosUserControl = new ProductosUserControl();
    var usuariosUserControl = new UsuariosUserControl();
    var postsUserControl = new PostsUserControl();
    var categoriasUserControl = new CategoriasUserControl();
    var comentariosUserControl = new ComentariosUserControl();
    var pedidosUserControl = new PedidosUserControl();
    var consultasUserControl = new ConsultasUserControl();
    userControlProductos.Content = productosUserControl;
    userControlUsuarios.Content = usuariosUserControl;
    userControlPosts.Content = postsUserControl;
    userControlCategorías.Content = categoriasUserControl;
    userControlComentarios.Content = comentariosUserControl;
    userControlPedidos.Content = pedidosUserControl;
    userControlConsultas.Content = consultasUserControl;
  }

  private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
    => ModifyTheme(DarkModeToggleButton.IsChecked == true);

  private static void ModifyTheme(bool isDarkTheme) {
    var paletteHelper = new PaletteHelper();
    var theme = paletteHelper.GetTheme();

    theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
    paletteHelper.SetTheme(theme);
  }
}
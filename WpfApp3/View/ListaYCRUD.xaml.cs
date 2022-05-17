using System.Windows;
using HandyControl.Tools.Extension;
using MaterialDesignExtensions.Themes;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

/// <summary>
///   Lógica de interacción para ListaYCRUD.xaml
/// </summary>
public partial class ListaYCRUD {
  public ListaYCRUD() {
    InitializeComponent();
    DataContext = new ThemeSettingsViewModel();
    var paletteHelper = new PaletteHelper();
    TextViewNombreBaseDatos.Text = "Estás editando: " + MainWindow.BaseDatos;
    var productosUserControl = new ProductosUserControl();
    var usuariosUserControl = new UsuariosUserControl();
    var postsUserControl = new PostsUserControl();
    var categoriasUserControl = new CategoriasUserControl();
    var comentariosUserControl = new ComentariosUserControl();
    var pedidosUserControl = new PedidosUserControl();
    var consultasUserControl = new ConsultasUserControl();
    var coloresUserControl = new ColoresUserControl();
    userControlProductos.Content = productosUserControl;
    userControlUsuarios.Content = usuariosUserControl;
    userControlPosts.Content = postsUserControl;
    userControlCategorías.Content = categoriasUserControl;
    UserControlComentarios.Content = comentariosUserControl;
    userControlPedidos.Content = pedidosUserControl;
    userControlConsultas.Content = consultasUserControl;
    userControlColores.Content = coloresUserControl;
    
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
    var paleteSelector = new PaletteSelector();
    paleteSelector.Show();
  }
}
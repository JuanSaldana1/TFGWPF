using System;
using System.Collections.Generic;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using PaletteHelper = MaterialDesignExtensions.Themes.PaletteHelper;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para ListaYCRUD.xaml
    /// </summary>
    public partial class ListaYCRUD {
        public IList<string> ListaTablas { get; set; } = new List<string>();

        public ListaYCRUD() {
            InitializeComponent();
            Console.WriteLine(ListaTablas);
            TextViewNombreBaseDatos.Text = "Estás editando " + MainWindow.BaseDatos;
            var productosUserControl = new ProductosUserControl();
            var usuariosUserControl = new UsuariosUserControl();
            var postsUserControl = new PostsUserControl();
            var categoriasUserControl = new CategoriasUserControl();
            var comentariosUserControl = new ComentariosUserControl();
            var pedidosUserControl = new PedidosUserControl();
            userControlProductos.Content = productosUserControl;
            userControlUsuarios.Content = usuariosUserControl;
            userControlPosts.Content = postsUserControl;
            userControlCategorías.Content = categoriasUserControl;
            userControlComentarios.Content = comentariosUserControl;
            userControlPedidos.Content = pedidosUserControl;

            var cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" +
                                 MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" +
                                 MainWindow.BaseDatos + ";";
            //string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery = "SHOW TABLES";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();
            if (myReader.HasRows) {
                while (myReader.Read()) {
                    ListaTablas.Add(myReader.GetString(0));
                }

                Console.WriteLine(ListaTablas);
            }

            myReader.Close();
            conexionBd.Close();
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
}
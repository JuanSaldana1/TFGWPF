using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para ListaYCRUD.xaml
    /// </summary>
    public partial class ListaYCRUD {
        public IList<string> ListaTablas { get; set; } = new List<string>();

        public ListaYCRUD() {
            InitializeComponent();
            TextViewNombreBaseDatos.Text = "Estás editando " + MainWindow.BaseDatos;
            var productosUserControl = new ProductosUserControl();
            var usuariosUserControl = new UsuariosUserControl();
            userControlUsuarios.Content = usuariosUserControl;
            userControlProductos.Content = productosUserControl;

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
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
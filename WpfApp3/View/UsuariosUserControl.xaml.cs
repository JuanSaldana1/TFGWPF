using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para UsuariosUserControl.xaml
    /// </summary>
    public partial class UsuariosUserControl {
        public UsuariosUserControl() {
            InitializeComponent();
            //SpeechRecognizer speechRecognizer = new SpeechRecognizer();

            string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" + MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" + MainWindow.BaseDatos + ";";
            // string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
            var conexionBd = new MySqlConnection(cadenaConexion);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Usuarios", conexionBd);
            MySqlCommand cmdFotos = new MySqlCommand("SELECT ProfilePhoto FROM Usuarios", conexionBd);
            conexionBd.Open();
            DataTable dataTable = new DataTable();
            DataTable dataTable1 = new DataTable();
            dataTable.Load(cmd.ExecuteReader());
            dataTable1.Load(cmdFotos.ExecuteReader());
            conexionBd.Close();
        }

        public void ButtonSave_Click(object sender, RoutedEventArgs e) {
        }
    }
}
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using WpfApp3.Model;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para UsuariosUserControl.xaml
    /// </summary>
    public partial class UsuariosUserControl {
        public static string Nombre { get; set; }
        public static string Correo { get; set; }
        public static string ImagenPerfil { get; set; }
        public static string Usuario { get; set; }

        public UsuariosUserControl() {
            InitializeComponent();
            //SpeechRecognizer speechRecognizer = new SpeechRecognizer();

            string cadenaConexion = MainWindow.CadenaConexion;
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
            UserViewModel listaUsuarios = new UserViewModel();
            UserModel nuevoUsuario = new UserModel();
            nuevoUsuario.Name = EditTextUserName.Text;
            SnackbarSeven.MessageQueue?.Enqueue("Se ha creado el usuario con nombre: " + nuevoUsuario.Name);
            listaUsuarios.Usuarios.Add(nuevoUsuario);
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".png"; // Default file extension
            openFileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg"; // Filter files by extension
            openFileDialog.ShowDialog();
            SnackbarSeven.MessageQueue?.Enqueue(openFileDialog.FileName);
            ImagenPerfil = openFileDialog.FileName;
            TextBlockFileName.Text = ImagenPerfil;
        }
    }
}
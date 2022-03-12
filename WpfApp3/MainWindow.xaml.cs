using System;
using System.Windows;
using MySqlConnector;
using WpfApp3.View;

namespace WpfApp3 {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        public static string Servidor { get; set; }
        public static string Puerto { get; set; }
        public static string Usuario { get; set; }
        public static string Contrasena { get; set; }
        public static string BaseDatos { get; set; }

        private void ButtonConectar_Click(object sender, RoutedEventArgs e) {
            Servidor = EditTextServer.Text;
            Puerto = EditTextPuerto.Text;
            Usuario = EditTextUser.Text;
            Contrasena = EditTextPassword.Password;
            BaseDatos = EditTextDatabase.Text;
            var listadeBasesDeDatos = "";
            var cadenaConexion = "";
            switch (OutlinedComboBox.Text) {
                case "PostgreSQL":
                    cadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario +
                                     "; password=" + Contrasena + "; database=" + BaseDatos + ";";
                    return;
                case "MySQL":
                    cadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario +
                                     "; password=" + Contrasena + "; database=" + BaseDatos + ";";
                    return;
            }

            if (Servidor.Length == 0 || Puerto.Length == 0 || Usuario.Length == 0 || Contrasena.Length == 0 ||
                BaseDatos.Length == 0) {
                SnackbarSeven.MessageQueue?.Enqueue("Rellena todos los campos");
                SnackbarSeven.MessageQueue?.Enqueue("La opión seleccionada es " + OutlinedComboBox.Text);
            }
            else if (Servidor.Contains("A")) {
                SnackbarSeven.MessageQueue?.Enqueue("El campo servidor debe estar compuesto por números");
            }
            else {
                try {
                    var conexionBd = new MySqlConnection(cadenaConexion);
                    conexionBd.Open();
                    Console.WriteLine(conexionBd);
                    // Si ha establecido bien la conexión, cierra la ventana de login y abre la de LISTA Y CRUD
                    SnackbarSeven.MessageQueue?.Enqueue("Has entrado correctamente");
                    MySqlDataReader reader;
                    MySqlCommand command = new MySqlCommand("SHOW DATABASES", conexionBd);
                    reader = command.ExecuteReader();
                    while (reader.Read()) {
                        listadeBasesDeDatos += reader.GetString(0) + '\n';
                    }

                    MessageBox.Show(listadeBasesDeDatos);
                    ListaYCRUD listaYcrud = new ListaYCRUD();
                    listaYcrud.Show();
                    Close();
                }
                catch (MySqlException ex) {
                    SnackbarSeven.MessageQueue?.Enqueue(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    var listaYcrud = new ListaYCRUD();
                    listaYcrud.Show();
                    Close();
                }
            }
        }
    }
}
using System;
using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySqlConnector;
using WpfApp3.View;

namespace WpfApp3 {
    public partial class MainWindow {
        private string _listadeBasesDeDatos = "";
        public static string MotorBd { get; set; }
        public static string Servidor { get; set; }
        public static string Puerto { get; set; }
        public static string Usuario { get; set; }
        public static string Contrasena { get; set; }
        public static string BaseDatos { get; set; }
        public static string CadenaConexion { get; set; }

        public MainWindow() {
            InitializeComponent();
            DataContext = _listadeBasesDeDatos;
        }


        private void ButtonConectar_Click(object sender, RoutedEventArgs e) {
            MotorBd = OutlinedComboBox.Text;
            Servidor = EditTextServer.Text;
            Puerto = EditTextPuerto.Text;
            Usuario = EditTextUser.Text;
            Contrasena = EditTextPassword.Password;
            BaseDatos = EditTextDatabase.Text;

            CadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario + "; password=" +
                             Contrasena + "; database=" + BaseDatos + ";";
            switch (MotorBd) {
                case "MySQL":
                    BaseDatos = "sys";
                    CadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario +
                                     "; password=" + Contrasena + "; database=" + BaseDatos + ";";
                    return;
                case "PostgreSQL":
                    CadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario +
                                     "; password=" + Contrasena + "; database=" + BaseDatos + ";";
                    return;
            }

            if (Servidor.Length == 0 || Puerto.Length == 0 || Usuario.Length == 0 || Contrasena.Length == 0 ||
                BaseDatos.Length == 0) {
                SnackbarSeven.MessageQueue?.Enqueue("Rellena todos los campos");
                SnackbarSeven.MessageQueue?.Enqueue("La opción seleccionada es " + OutlinedComboBox.Text);
            }
            else if (Servidor.Contains("A")) {
                SnackbarSeven.MessageQueue?.Enqueue("El campo servidor debe estar compuesto por números");
            }
            else {
                try {
                    var conexionBd = new MySqlConnection(CadenaConexion);
                    conexionBd.Open();
                    Console.WriteLine(conexionBd);
                    // Si ha establecido bien la conexión, cierra la ventana de login y abre la de LISTA Y CRUD
                    SnackbarSeven.MessageQueue?.Enqueue("Has entrado correctamente");
                    MySqlDataReader reader;
                    MySqlCommand command = new MySqlCommand("SHOW DATABASES", conexionBd);
                    reader = command.ExecuteReader();
                    while (reader.Read()) {
                        _listadeBasesDeDatos += reader.GetString(0) + '\n';
                    }

                    MessageBox.Show(_listadeBasesDeDatos);
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

        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        private void ButtonConectar1_Click(object sender, RoutedEventArgs e) {
            SnackbarSeven.MessageQueue?.Enqueue(ListBoxBasesDeDatos.SelectedValue);
        }
    }
}
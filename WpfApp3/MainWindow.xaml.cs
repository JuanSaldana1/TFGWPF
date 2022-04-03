using System;
using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySqlConnector;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using WpfApp3.View;

namespace WpfApp3 {
    public partial class MainWindow {
        private string listadeBasesDeDatos = "";
        public static string MotorBd { get; set; }
        public static string Servidor { get; set; }
        public static string Puerto { get; set; }
        public static string Usuario { get; set; }
        public static string Contrasena { get; set; }
        public static string BaseDatos { get; set; }
        public static string CadenaConexion { get; set; }

        Notifier notifier = new Notifier(cfg => {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10
            );

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public MainWindow() {
            InitializeComponent();
            DataContext = listadeBasesDeDatos;
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
                            listadeBasesDeDatos += reader.GetString(0) + '\n';
                        }

                        MessageBox.Show(listadeBasesDeDatos);
                        DataContext = listadeBasesDeDatos;
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

                    return;
                case "PostgreSQL":
                    BaseDatos = "postgres";
                    CadenaConexion = "User ID=" + Usuario + "; Password=" + Contrasena + "; Host=" + Servidor +
                                     "; Port=" + Puerto + "; Database=" + BaseDatos +
                                     "; Pooling=true; Min Pool Size=0;";
                    Console.Write(CadenaConexion);
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

                    return;
            }

            if (Servidor.Length == 0 || Puerto.Length == 0 || Usuario.Length == 0 || Contrasena.Length == 0 ||
                BaseDatos.Length == 0) {
                SnackbarSeven.MessageQueue?.Enqueue("Rellena todos los campos");
                notifier.ShowInformation("Rellena todos los campos");
                notifier.ShowSuccess("Rellena todos los campos");
                notifier.ShowWarning("Rellena todos los campos");
                notifier.ShowError("Rellena todos los campos");
                SnackbarSeven.MessageQueue?.Enqueue("La opción seleccionada es " + OutlinedComboBox.Text);
                notifier.Dispose();
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

        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        private void ButtonConectar1_Click(object sender, RoutedEventArgs e) {
            SnackbarSeven.MessageQueue?.Enqueue(ListBoxBasesDeDatos.SelectedValue);
        }
    }
}
using System;
using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySqlConnector;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using WpfApp3.View;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3;

public partial class MainWindow {
  private string listadeBasesDeDatos = "";

  private Notifier notifier = new(cfg => {
    cfg.PositionProvider = new WindowPositionProvider(
      Application.Current.MainWindow,
      Corner.TopRight,
      10,
      10
    );

    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
      TimeSpan.FromSeconds(3),
      MaximumNotificationCount.FromCount(5));

    cfg.Dispatcher = Application.Current.Dispatcher;
  });

  public MainWindow() {
    InitializeComponent();
    DataContext = listadeBasesDeDatos;
  }

  public static string MotorBd { get; set; }
  public static string Servidor { get; set; }
  public static string Puerto { get; set; }
  public static string Usuario { get; set; }
  public static string Contrasena { get; set; }
  public static string BaseDatos { get; set; }
  public static string CadenaConexion { get; set; }

  public void Conectar() {
    MotorBd = OutlinedComboBox.Text;
    Servidor = EditTextServer.Text;
    Puerto = EditTextPuerto.Text;
    Usuario = EditTextUser.Text;
    Contrasena = EditTextPassword.Password;
    BaseDatos = EditTextDatabase.Text;

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
        /*CadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario + "; password=" +
                         Contrasena + "; database=" + BaseDatos + ";";*/
        switch (MotorBd) {
          case "MySQL":
            /*BaseDatos = "sys";*/
            CadenaConexion = "server=" + Servidor + "; port=" + Puerto + "; user id=" + Usuario +
                             "; password=" + Contrasena + "; database=" + BaseDatos + ";";
            try {
              var conexionBd = new MySqlConnection(CadenaConexion);
              conexionBd.Open();
              // Si ha establecido bien la conexión, cierra la ventana de login y abre la de LISTA Y CRUD
              SnackbarSeven.MessageQueue?.Enqueue("Has entrado correctamente");
              MySqlDataReader reader;
              var command = new MySqlCommand("SHOW DATABASES", conexionBd);
              reader = command.ExecuteReader();
              if (reader.HasRows)
                while (reader.Read())
                  listadeBasesDeDatos += reader.GetString(0) + '\n';

              MessageBox.Show(listadeBasesDeDatos, "Bases de datos de este puerto:",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
              DataContext = listadeBasesDeDatos;
              var listaYcrud = new ListaYCRUD();
              listaYcrud.Show();
              Close();
            }
            catch (MySqlException ex) {
              MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return;
          case "PostgreSQL":
            BaseDatos = "postgres";
            CadenaConexion = "User ID=" + Usuario + "; Password=" + Contrasena + "; Host=" + Servidor +
                             "; Port=" + Puerto + "; Database=" + BaseDatos +
                             "; Pooling=true; Min Pool Size=0;";
            try {
              var conexionBd = new MySqlConnection(CadenaConexion);
              conexionBd.Open();
              Console.WriteLine(conexionBd);
              // Si ha establecido bien la conexión, cierra la ventana de login y abre la de LISTA Y CRUD
              SnackbarSeven.MessageQueue?.Enqueue("Has entrado correctamente");
              MySqlDataReader reader;
              var command = new MySqlCommand("SHOW DATABASES", conexionBd);
              reader = command.ExecuteReader();
              while (reader.Read()) listadeBasesDeDatos += reader.GetString(0) + '\n';

              MessageBox.Show(listadeBasesDeDatos, "Bases de datos de este puerto:",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
              var listaYcrud = new ListaYCRUD();
              listaYcrud.Show();
              Close();
            }
            catch (MySqlException ex) {
              MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return;
        }
        /*var conexionBd = new MySqlConnection(CadenaConexion);
        conexionBd.Open();
        // Si ha establecido bien la conexión, cierra la ventana de login y abre la de LISTA Y CRUD
        SnackbarSeven.MessageQueue?.Enqueue("Has entrado correctamente");
        MySqlDataReader reader;
        var command = new MySqlCommand("SHOW DATABASES", conexionBd);
        reader = command.ExecuteReader();
        while (reader.Read()) {
          listadeBasesDeDatos += reader.GetString(0) + '\n';
        }

        MessageBox.Show(listadeBasesDeDatos, "Bases de datos de este puerto:",
          MessageBoxButton.OK, MessageBoxImage.Information);
        var listaYcrud = new ListaYCRUD();
        listaYcrud.Show();
        Close();*/
      }
      catch (MySqlException ex) {
        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }

  private void ButtonConectar_Click(object sender, RoutedEventArgs e) {
    Conectar();
  }

  private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");
  }
}
using MaterialDesignThemes.Wpf;
using MySqlConnector;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para ProductosUserControl.xaml
    /// </summary>
    public partial class ProductosUserControl {
        public ProductosUserControl() {
            InitializeComponent();
            string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" + MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" + MainWindow.BaseDatos + ";";
            // string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            //MySqlCommand cmd = new MySqlCommand("SELECT Name FROM Productos", conexionBD);
            conexionBd.Open();
            // List<ProductoModel> dataTable = new List<ProductoModel>();

            conexionBd.Close();
            //MyListView.DataContext = new ProductosViewModel();

            MyListView.DataContext = new ProductViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
        }

        private Action Deshacer() {
            try {

            } catch (NotImplementedException nIe) {
                _ = MessageBox.Show(nIe.Message);
            }
            return null;
        }

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof(DialogClosingEventHandler),
                typeof(DialogHost));

        private void ArticuloChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
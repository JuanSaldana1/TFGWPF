using MySqlConnector;
using System;
using System.Windows;

namespace WpfApp3 {
    internal class Connection {
        public MySqlConnection GetConexion() {
            string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" + MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" + MainWindow.BaseDatos + ";";
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            try {
                conexionBd.Open();
                return conexionBd;
            } catch (MySqlException ex) {
                _ = MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            } catch (InvalidOperationException ex) {
                _ = MessageBox.Show(ex.Message);
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
            return conexionBd;
        }
    }
}
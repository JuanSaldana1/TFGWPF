using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WpfApp3.ViewModel {
    internal class UserViewModel {
        private IList<UserViewModel> Usuarios { get; set; } = new List<UserViewModel>();

        public UserViewModel() {
            string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" +
                                    MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" +
                                    MainWindow.BaseDatos + ";";
            //string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery =
                "SELECT UserId, Username, Name, Surname, Email, Follower, ProfilePhoto FROM Usuarios";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();

            if (myReader.HasRows) {
                while (myReader.Read()) {
                }
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel {
    internal class UserViewModel {
        public IList<UserModel> Usuarios { get; set; } = new List<UserModel>();

        public UserViewModel() {
            string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" +
                                    MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" +
                                    MainWindow.BaseDatos + ";";
            //string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery =
                "SELECT UserId, Username, Name, Surname, Email, Rol, Follower, ProfilePhoto FROM Usuarios";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();

            if (myReader.HasRows) {
                while (myReader.Read()) {
                    Usuarios.Add(new UserModel() {
                        UserId = myReader.GetInt32(0),
                        Username = myReader.GetString(1),
                        Name = myReader.GetString(2),
                        Surname = myReader.GetString(3),
                        Email = myReader.GetString(4),
                        Rol = myReader.GetString(5),
                        Follower = myReader.GetString(6),
                        ProfilePhoto = myReader.GetString(7)
                    });
                }
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
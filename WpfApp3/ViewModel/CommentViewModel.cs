using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel {
    public class CommentViewModel {
        public IList<CommentModel> Comments { get; set; } = new List<CommentModel>();

        public CommentViewModel() {
            //string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" + MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" + MainWindow.BaseDatos + ";";
            string cadenaConexion = MainWindow.CadenaConexion;
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery =
                "SELECT IdComentario, Titulo, Texto, FechaPublicacion, IdUsuario, Anonimo, Usuarios.Name, Usuarios.ProfilePhoto, Usuarios.Email FROM Comentarios join Usuarios on Comentarios.IdUsuario = Usuarios.UserId";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();


            if (myReader.HasRows) {
                while (myReader.Read()) {
                    Comments.Add(new CommentModel() {
                        CommentId = myReader.GetInt32(0),
                        CommentTitle = myReader.GetString(1),
                        CommentContent = myReader.GetString(2),
                        CommentPublishDate = myReader.GetDateTime(3),
                        CommentUserId = myReader.GetInt32(4),
                        IsAnonymous = myReader.GetBoolean(5),
                        UserName = myReader.GetString(6),
                        ProfilePhoto = myReader.GetString(7),
                        UserEmail = myReader.GetString(8)
                    });
                }
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
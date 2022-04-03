using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel {
    public class PostViewModel {
        public IList<PostModel> Posts { get; set; } = new List<PostModel>();

        public PostViewModel() {
            string cadenaConexion = MainWindow.CadenaConexion;
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery =
                "SELECT IdPost, Titulo, Descripcion, FechaPublicacion, Imagen1, Imagen2 FROM Posts";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();


            if (myReader.HasRows) {
                while (myReader.Read()) {
                    Posts.Add(new PostModel() {
                        ProductId = myReader.GetInt32(0),
                        PostTitle = myReader.GetString(1),
                        PostDescription = myReader.GetString(2),
                        PostPublishDate = myReader.GetDateTime(3),
                        PostFirstImage = myReader.GetString(4),
                        PostSecondImage = myReader.GetString(5)
                    });
                }
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
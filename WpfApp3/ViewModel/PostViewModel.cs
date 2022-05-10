using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

public class PostViewModel {
  public PostViewModel() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery =
      "SELECT IdPost, Titulo, Descripcion, FechaPublicacion, Favorito, Imagen1, Imagen2 FROM Posts";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows)
      while (myReader.Read())
        Posts.Add(new PostModel {
          PostId = myReader.GetInt32(0),
          PostTitle = myReader.GetString(1),
          PostDescription = myReader.GetString(2),
          PostPublishDate = myReader.GetDateTime(3),
          IsFavorite = myReader.GetBoolean(4),
          PostFirstImage = myReader.GetString(5),
          PostSecondImage = myReader.GetString(6)
        });

    myReader.Close();
    conexionBd.Close();
  }

  public IList<PostModel> Posts { get; } = new List<PostModel>();
}
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

public class CommentViewModel {
  public CommentViewModel() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery =
      "SELECT CommentId, Title, Content, PublicationDate, Comments.UserId, IsAnonimous, Users.Name, Users.ProfilePhoto, Users.Email FROM Comments join Users on Comments.UserId = Users.UserId";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows)
      while (myReader.Read())
        Comments.Add(new CommentModel {
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

    myReader.Close();
    conexionBd.Close();
  }

  public IList<CommentModel> Comments { get; } = new List<CommentModel>();
}
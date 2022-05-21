using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

public class CommentViewModel {
  public IList<CommentModel> Comments { get; } = new List<CommentModel>();

  public CommentViewModel() {
    GetCommentsForView();
  }

  public void GetCommentsForView() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    const string mySelectQuery =
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

  public bool DeleteMethod(int commentId) {
    var isDeleted = false;
    try {
      var deleteQuery = "DELETE FROM Comments WHERE CommentId='" + commentId + "'";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(deleteQuery, conexionBd);
        command.ExecuteNonQuery();
        isDeleted = true;
        conexionBd.Close();
        GetCommentsForView(); //Actualizar la lista de productos
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        isDeleted = false;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isDeleted;
  }
}
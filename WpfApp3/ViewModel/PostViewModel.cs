﻿using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

public class PostViewModel {
  public IList<PostModel> Posts { get; } = new List<PostModel>();

  public PostViewModel() {
    GetPostsForView();
  }

  public void GetPostsForView() {
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      const string mySelectQuery =
        "SELECT PostId, Title, Description, PublicationDate, IsFavorite, Image1, Image2 FROM Posts";
      var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
      try {
        conexionBd.Open();
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();

        if (myReader.HasRows)
          while (myReader.Read())
            Posts.Add(new PostModel {
              PostId = myReader.GetInt32(0),
              PostTitle = myReader.GetString(1),
              PostDescription = myReader.GetString(2),
              PostPublishDate = myReader.GetString(3),
              IsFavorite = myReader.GetBoolean(4),
              PostFirstImage = myReader.GetString(5),
              PostSecondImage = myReader.GetString(6)
            });

        myReader.Close();
        conexionBd.Close();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }

  public bool InsertMethod(PostModel post) {
    var isInserted = false;
    try {
      var insertQuery =
        "INSERT INTO Posts (Title, Description, Link, PublicationDate, IsFavorite, Image1, Image2) values ('" +
        post.PostTitle + "','" + post.PostDescription + "','" + post.PostUrl + "','" + post.PostPublishDate + "'," +
        post.IsFavorite + ",'" + post.PostFirstImage + "','" + post.PostSecondImage + "');";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(insertQuery, conexionBd);
        command.ExecuteNonQuery();
        isInserted = true;
        conexionBd.Close();
        GetPostsForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        throw;
      }

      GetPostsForView();
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isInserted;
  }
  
  public bool DeleteMethod(int productId) {
    var isDeleted = false;
    try {
      var deleteQuery = "DELETE FROM Posts WHERE PostId='" + productId + "'";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(deleteQuery, conexionBd);
        command.ExecuteNonQuery();
        isDeleted = true;
        conexionBd.Close();
        GetPostsForView(); //Actualizar la lista de productos
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
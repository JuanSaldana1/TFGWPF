using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

internal class UserViewModel {
  public IList<UserModel> Usuarios { get; } = new List<UserModel>();
  public IList<string> Roles { get; } = new List<string>();

  public UserViewModel() {
    GetUsersForView();
  }

  public void GetUsersForView() {
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexion = new MySqlConnection(cadenaConexion);
      const string selectAllUsersQuery =
        "SELECT UserId, Username, Name, Surname, Email, Rol, Follower, ProfilePhoto FROM Users";
      var myCommand = new MySqlCommand(selectAllUsersQuery, conexion);
      try {
        conexion.Open();
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();

        if (myReader.HasRows)
          while (myReader.Read())
            Usuarios.Add(new UserModel {
              UserId = myReader.GetInt32(0),
              Username = myReader.GetString(1),
              Name = myReader.GetString(2),
              Surname = myReader.GetString(3),
              Email = myReader.GetString(4),
              Rol = myReader.GetString(5),
              Follower = myReader.GetString(6),
              ProfilePhoto = myReader.GetString(7)
            });

        myReader.Close();
        conexion.Close();

        const string selectRoles = "SELECT Rol FROM Users GROUP BY Rol";
        var rolSelectionCommand = new MySqlCommand(selectRoles, conexion);
        conexion.Open();
        MySqlDataReader myRolesReader;
        rolSelectionCommand.ExecuteReader();
        if (myReader.HasRows)
          while (myReader.Read())
            try {
              Roles.Add(myReader.GetString(0));
            }
            catch (Exception e) {
              MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
              throw;
            }

        myReader.Close();
        conexion.Close();
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

  public bool InsertMethod(UserModel usuario) {
    var isInserted = false;
    try {
      var insertQuery =
        "INSERT INTO Users (Username, Name, Surname, Email, Rol, Follower, ProfilePhoto) values ('" +
        usuario.Username + "','" + usuario.Name + "','" + usuario.Surname +
        "','" + usuario.Email + "','" + usuario.Rol + "','" + usuario.Follower +
        "','" + usuario.ProfilePhoto + "')";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(insertQuery, conexionBd);
        command.ExecuteNonQuery();
        isInserted = true;
        conexionBd.Close();
        GetUsersForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }

      GetUsersForView();
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }

    return isInserted;
  }


  public bool ChangeName(string name, int userId) {
    var cadenaConexion = MainWindow.CadenaConexion;

    try {
      var updateNameQuery =
        "UPDATE Users SET Name = '" + name + "' WHERE UserId = " + userId + ";";
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      return false;
    }

    return true;
  }
}
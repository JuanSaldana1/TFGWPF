using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

internal class UserViewModel {
  public IList<UserModel> Usuarios { get; } = new List<UserModel>();

  private readonly string stringConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto +
                                           "; user id=" +
                                           MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" +
                                           MainWindow.BaseDatos + ";";

  public UserViewModel() {
    string cadenaConexion = stringConexion;
    string selectAllUsersQuery =
      "SELECT UserId, Username, Name, Surname, Email, Rol, Follower, ProfilePhoto FROM Usuarios";
    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
    MySqlCommand myCommand = new MySqlCommand(selectAllUsersQuery, conexion);
    conexion.Open();
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
    conexion.Close();
  }

  public bool ChangeName(string name, int userId) {
    string cadenaConexion = stringConexion;

    try {
      string updateNameQuery =
        "UPDATE Usuarios SET Name = '" + name + "' WHERE UserId = " + userId;
    }
    catch (Exception e) {
      Console.WriteLine(e);
      throw;
    }

    return true;
  }
}
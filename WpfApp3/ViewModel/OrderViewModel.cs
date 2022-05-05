using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

public class OrderViewModel {
  public IList<OrderModel> Orders { get; } = new List<OrderModel>();

  public OrderViewModel() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery =
      "SELECT OrderId, O.ProductId, OrderDate, Quantity, LineId, P.Name, U.Name FROM Orders O join Productos P on O.ProductId = P.ProductId JOIN Usuarios U on U.UserId = O.UserId";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows) {
      while (myReader.Read()) {
        Orders.Add(new OrderModel {
          OrderId = myReader.GetInt32(0),
          ProductId = myReader.GetInt32(1),
          OrderDate = myReader.GetDateTime(2),
          Quantity = myReader.GetInt32(3),
          LineId = myReader.GetInt32(4),
          ProductName = myReader.GetString(5),
          UserName = myReader.GetString(6)
        });
      }
    }

    myReader.Close();
    conexionBd.Close();
  }
}
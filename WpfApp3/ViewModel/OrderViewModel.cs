using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

public class OrderViewModel {
  public IList<OrderModel> Orders { get; } = new List<OrderModel>();

  public OrderViewModel() {
    GetOrdersForView();
  }

  private void GetOrdersForView() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    const string mySelectQuery =
      "SELECT OrderId, O.ProductId, OrderDate, Quantity, LineId, P.Name, U.Name FROM Orders O join Products P on O.ProductId = P.ProductId JOIN Users U on U.UserId = O.UserId";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    try {
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
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }

  public void GetAllOrders() {
    Orders.Clear();
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      const string mySelectQuery = "SELECT * FROM Orders  ORDER BY OrderID";
      var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
      conexionBd.Open();
      MySqlDataReader myReader;
      myReader = myCommand.ExecuteReader();

      if (myReader.HasRows)
        while (myReader.Read())
          Orders.Add(new OrderModel {
            OrderId = myReader.GetInt32(0),
            ProductId = myReader.GetInt32(1),
            OrderDate = myReader.GetDateTime(2),
            Quantity = myReader.GetInt32(3),
            LineId = myReader.GetInt32(4),
            UserId = myReader.GetInt32(5),
          });

      myReader.Close();
      conexionBd.Close();
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      // throw;
    }
  }

  public bool InsertMethod(OrderModel pedido) {
    var isInserted = false;
    try {
      var insertQuery =
        "INSERT INTO Orders (ProductId, OrderDate, Quantity, LineId, UserId) values ('" +
        pedido.ProductId + "','" + pedido.OrderDate + "'," + pedido.Quantity +
        ", " + pedido.LineId + "," + pedido.UserId + ");";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(insertQuery, conexionBd);
        command.ExecuteNonQuery();
        isInserted = true;
        conexionBd.Close();
        GetOrdersForView(); //Actualizar la lista de productos
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        throw;
        isInserted = false;
      }

      GetOrdersForView(); //Actualizar la lista de productos
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isInserted;
  }
}
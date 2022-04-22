﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

public class OrderViewModel {
    public IList<OrderModel> Orders { get; set; } = new List<OrderModel>();

    public OrderViewModel() {
        string cadenaConexion = MainWindow.CadenaConexion;
        MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
        string mySelectQuery =
            "SELECT OrderId, O.ProductId, Quantity, LineId, P.Name FROM Orders O join Productos P on O.ProductId = P.ProductId";
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
        conexionBd.Open();
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();


        if (myReader.HasRows) {
            while (myReader.Read()) {
                Orders.Add(new OrderModel() {
                    OrderId = myReader.GetInt32(0),
                    ProductId = myReader.GetInt32(1),
                    Quantity = myReader.GetInt32(2),
                    LineId = myReader.GetInt32(3),
                    ProductName = myReader.GetString(4)
                });
            }
        }

        myReader.Close();
        conexionBd.Close();
    }
}
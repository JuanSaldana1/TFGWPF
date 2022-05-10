﻿using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

internal class ProductViewModel {
  public ProductViewModel() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery =
      "SELECT ProductId, Name, Description, Price, C.Nombre, Stock, Rating, Image FROM Productos JOIN Categorias C on C.CategoryId = Productos.CategoryId";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows)
      while (myReader.Read())
        Productos.Add(new ProductoModel {
          ProductId = myReader.GetInt32(0),
          ProductName = myReader.GetString(1),
          ProductDescription = myReader.GetString(2),
          ProductPrice = myReader.GetDouble(3),
          ProductCategory = myReader.GetString(4),
          ProductStock = myReader.GetInt32(5),
          ProductRating = myReader.GetInt32(6),
          ProductImage = myReader.GetString(7)
        });

    myReader.Close();
    conexionBd.Close();
  }

  public IList<ProductoModel> Productos { get; } = new List<ProductoModel>();

  public void GetAllProducts() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery =
      "SELECT * FROM Productos";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows)
      while (myReader.Read())
        Productos.Add(new ProductoModel {
          ProductId = myReader.GetInt32(0),
          ProductName = myReader.GetString(1),
          ProductDescription = myReader.GetString(2),
          ProductPrice = myReader.GetDouble(3),
          CategoryId = myReader.GetInt32(4),
          ProductStock = myReader.GetInt32(5),
          ProductImage = myReader.GetString(6),
          PostId = myReader.GetInt32(7)
        });

    myReader.Close();
    conexionBd.Close();
  }
}
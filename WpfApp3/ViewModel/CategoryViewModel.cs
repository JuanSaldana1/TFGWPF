using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

internal class CategoryViewModel {
  public CategoryViewModel() {
    GetCategoriesForView();
  }

  public void GetCategoriesForView() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    const string mySelectQuery = "SELECT CategoryId, Name FROM Categories";
    var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows) {
      while (myReader.Read()) {
        Categorias.Add(new CategoryModel {
          CategoryId = myReader.GetInt32(0),
          CategoryName = myReader.GetString(1)
        });
      }
    }

    myReader.Close();
    conexionBd.Close();
  }

  public bool InsertMethod(CategoryModel categoria) {
    var isInserted = false;
    try {
      var insertQuery =
        "INSERT INTO Categories (Name) values ('" + categoria.CategoryName + "');";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(insertQuery, conexionBd);
        command.ExecuteNonQuery();
        isInserted = true;
        conexionBd.Close();
        GetCategoriesForView(); //Actualizar la lista de categorias
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        throw;
        isInserted = false;
      }

      GetCategoriesForView(); //Actualizar la lista de categorias
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isInserted;
  }

  public IList<CategoryModel> Categorias { get; set; } = new List<CategoryModel>();
}
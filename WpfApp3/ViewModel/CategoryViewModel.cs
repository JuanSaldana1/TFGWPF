using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

internal class CategoryViewModel {
  public IList<CategoryModel> Categorias { get; } = new List<CategoryModel>();

  public CategoryViewModel() {
    GetCategoriesForView();
  }

  public void GetCategoriesForView() {
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      const string mySelectQuery = "SELECT CategoryId, Name FROM Categories";
      var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
      try {
        conexionBd.Open();
        MySqlDataReader myReader;
        try {
          myReader = myCommand.ExecuteReader();
          if (myReader.HasRows) {
            while (myReader.Read()) {
              Categorias.Add(new CategoryModel {
                CategoryId = myReader.GetInt32(0),
                CategoryName = myReader.GetString(1)
              });
            }
          }
        }
        catch (Exception e) {
          MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          throw;
        }

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
      }

      GetCategoriesForView(); //Actualizar la lista de categorias
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isInserted;
  }

  public bool DeleteMethod(int categoryId) {
    var isDeleted = false;
    try {
      var deleteQuery = "DELETE FROM Categories WHERE CategoryId='" + categoryId + "'";
      var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
      try {
        conexionBd.Open();
        var command = new MySqlCommand(deleteQuery, conexionBd);
        command.ExecuteNonQuery();
        isDeleted = true;
        conexionBd.Close();
        GetCategoriesForView(); //Actualizar la lista de productos
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        isDeleted = false;
      }

      GetCategoriesForView(); //Actualizar la lista de productos
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isDeleted;
  }
}
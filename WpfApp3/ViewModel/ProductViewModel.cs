using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using MaterialDesignDemo.Domain;
using MySql.Data.MySqlClient;
using WpfApp3.Model;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.ViewModel;

internal class ProductViewModel : ViewModelBase {
  public ObservableCollection<ProductoModel> Productos { get; set; } = new();
  public IList<ProductoModel> SelectedProductos { get; set; } = new List<ProductoModel>();

  public ProductViewModel() {
    OpenSample4DialogCommand = new AnotherCommandImplementation(OpenSample4Dialog);
    AcceptSample4DialogCommand = new AnotherCommandImplementation(AcceptSample4Dialog);
    CancelSample4DialogCommand = new AnotherCommandImplementation(CancelSample4Dialog);
    GetProductsForView();
  }

  public void GetProductsForView() {
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      const string mySelectQuery =
        "SELECT ProductId, Products.Name, Description, Price, C.Name, Stock, Rating, Image FROM Products JOIN Categories C on C.CategoryId = Products.CategoryId ORDER BY ProductId";
      var myCommand = new MySqlCommand(mySelectQuery, conexionBd);
      try {
        conexionBd.Open();
        MySqlDataReader myReader;
        Productos.Clear();
        try {
          myReader = myCommand.ExecuteReader();
          if (myReader.HasRows) {
            while (myReader.Read()) {
              Productos.Add(new ProductoModel {
                ProductId = myReader.GetInt32(0),
                ProductName = myReader.GetString(1),
                ProductDescription = myReader.GetString(2),
                ProductPrice = myReader.GetDecimal(3),
                ProductCategory = myReader.GetString(4),
                ProductStock = myReader.GetInt32(5),
                ProductRating = myReader.GetInt32(6),
                ProductImage = myReader.GetString(7)
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


  public void GetAllProducts() {
    Productos.Clear();
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      var mySelectQuery = "SELECT * FROM Products  ORDER BY ProductId";
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
            ProductPrice = myReader.GetDecimal(3),
            CategoryId = myReader.GetInt32(4),
            ProductStock = myReader.GetInt32(5),
            ProductImage = myReader.GetString(6),
            PostId = myReader.GetInt32(7)
          });

      myReader.Close();
      conexionBd.Close();
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      // throw;
    }
  }

  public bool InsertMethod(ProductoModel producto) {
    var isInserted = false;
    try {
      var insertQuery =
        "INSERT INTO Products (Name, Description, Price, CategoryId, Stock, Rating, Image, PostId) values ('" +
        producto.ProductName + "','" + producto.ProductDescription + "'," + producto.ProductPrice +
        "," + producto.CategoryId + "," + producto.ProductStock + "," + producto.ProductRating +
        ",'" + producto.ProductImage + "'," + producto.PostId + ");";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(insertQuery, conexionBd);
        command.ExecuteNonQuery();
        isInserted = true;
        conexionBd.Close();
        GetProductsForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        throw;
      }

      GetProductsForView(); //Actualizar la lista de productos
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isInserted;
  }

  public bool UpdateMethod(ProductoModel producto) {
    var isUpdated = false;
    try {
      var updateQuery =
        "UPDATE Products SET Name='" + producto.ProductName + "', Description='" + producto.ProductDescription +
        "', Price='" + producto.ProductPrice + "', CategoryId='" + producto.CategoryId + "', Stock='" +
        producto.ProductStock + "', Rating='" + producto.ProductRating + "', Image='" + producto.ProductImage +
        "', PostId='" + producto.PostId + "' WHERE ProductId='" + producto.ProductId + "'";
      try {
        var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
        conexionBd.Open();
        var command = new MySqlCommand(updateQuery, conexionBd);
        command.ExecuteNonQuery();
        isUpdated = true;
        conexionBd.Close();
        GetAllProducts(); //Actualizar la lista de productos
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        isUpdated = false;
      }

      GetProductsForView(); //Actualizar la lista de productos
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }


    return isUpdated;
  }

  public bool DeleteMethod(int productId) {
    var isDeleted = false;
    try {
      var deleteQuery = "DELETE FROM Products WHERE ProductId='" + productId + "'";
      var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
      try {
        conexionBd.Open();
        var command = new MySqlCommand(deleteQuery, conexionBd);
        command.ExecuteNonQuery();
        isDeleted = true;
        conexionBd.Close();
        GetProductsForView(); //Actualizar la lista de productos
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        isDeleted = false;
      }

      GetProductsForView(); //Actualizar la lista de productos
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }

    return isDeleted;
  }

  public List<Object> SearchAllSelecrQuery(string parameters) {
    List<Object> lista = new List<Object>();
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      var selectQuery = "";
      if (parameters == null) {
        selectQuery = "SELECT * FROM Products";
      }
      else {
        selectQuery =
          "SELECT * FROM Products JOIN Categories C on C.CategoryId = Products.CategoryId " +
          "WHERE Products.Name = '%" + parameters + "%'" + " OR Products.Description ='%" +
          parameters + "%' OR Products.Price = '%" + parameters +
          "%' OR Products.CategoryId = '%" + parameters + "%';";
      }

      var myCommand = new MySqlCommand(selectQuery, conexionBd);
      conexionBd.Open();
      MySqlDataReader myReader;
      try {
        myReader = myCommand.ExecuteReader();
        if (myReader.HasRows)
          while (myReader.Read())
            Productos.Add(new ProductoModel {
              ProductId = myReader.GetInt32(0),
              ProductName = myReader.GetString(1),
              ProductDescription = myReader.GetString(2),
              ProductPrice = myReader.GetDecimal(3),
              CategoryId = myReader.GetInt32(4),
              ProductStock = myReader.GetInt32(5),
              ProductImage = myReader.GetString(6),
              PostId = myReader.GetInt32(7)
            });
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

    return lista;
  }

  public string SelectAllFromProductId(int productId) {
    var producto = new ProductoModel();
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      var selectQuery = "SELECT * FROM Products WHERE ProductId='" + productId + "';";

      var myCommand = new MySqlCommand(selectQuery, conexionBd);
      conexionBd.Open();
      MySqlDataReader myReader;
      myReader = myCommand.ExecuteReader();
      if (myReader.HasRows) {
        try {
          while (myReader.Read()) {
            producto.ProductId = myReader.GetInt32(0);
            producto.ProductName = myReader.GetString(1);
            producto.ProductDescription = myReader.GetString(2);
            producto.ProductPrice = myReader.GetDecimal(3);
            producto.CategoryId = myReader.GetInt32(4);
            producto.ProductStock = myReader.GetInt32(5);
            producto.ProductRating = myReader.GetInt32(6);
            producto.ProductImage = myReader.GetString(7);
            producto.PostId = myReader.GetInt32(8);
          }
        }
        catch (Exception e) {
          MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          throw;
        }

        conexionBd.Close();
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }

    return producto.ToString();
  }

  #region SAMPLE 4

//pretty much ignore all the stuff provided, and manage everything via custom commands and a binding for .IsOpen
  public ICommand OpenSample4DialogCommand { get; }
  public ICommand AcceptSample4DialogCommand { get; }
  public ICommand CancelSample4DialogCommand { get; }
  private bool _isSample4DialogOpen;
  private object? _sample4Content;

  public bool IsSample4DialogOpen {
    get => _isSample4DialogOpen;
    set => SetProperty(ref _isSample4DialogOpen, value);
  }

  public object? Sample4Content {
    get => _sample4Content;
    set => SetProperty(ref _sample4Content, value);
  }

  private void OpenSample4Dialog(object? _) {
    Sample4Content = new Sample4Dialog();
    IsSample4DialogOpen = true;
  }

  private void CancelSample4Dialog(object? _) => IsSample4DialogOpen = false;

  private void AcceptSample4Dialog(object? _) {
    //pretend to do something for 3 seconds, then close
    Sample4Content = new ProgressBar();
    Task.Delay(TimeSpan.FromSeconds(3))
      .ContinueWith((t, _) => IsSample4DialogOpen = false, null,
        TaskScheduler.FromCurrentSynchronizationContext());
  }

  #endregion
}
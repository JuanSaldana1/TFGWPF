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


  public void GetAllProducts() {
    var cadenaConexion = MainWindow.CadenaConexion;
    var conexionBd = new MySqlConnection(cadenaConexion);
    var mySelectQuery = "SELECT * FROM Productos";
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

  public bool InsertMethod(ProductoModel producto) {
    var isInserted = false;
    var insertQuery =
      "INSERT INTO Productos (Name, Description, Price, CategoryId, Stock, Rating, Image, PostId) values ('" +
      producto.ProductName + "','" + producto.ProductDescription + "','" + producto.ProductPrice +
      "','" + producto.CategoryId + "','" + producto.ProductStock + "','" + producto.ProductRating  + 
      "','" + producto.ProductImage + "','" + producto.PostId + "')";
    try {
      var conexionBd = new MySqlConnection(MainWindow.CadenaConexion);
      conexionBd.Open();
      var command = new MySqlCommand(insertQuery, conexionBd);
      command.ExecuteNonQuery();
      isInserted = true;
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      isInserted = false;
    }

    return isInserted;
  }

  public List<Object> SearchAllSelecrQuery(string parameters) {
    List<Object> lista = new List<Object>();
    try {
      var cadenaConexion = MainWindow.CadenaConexion;
      var conexionBd = new MySqlConnection(cadenaConexion);
      var selectQuery = "";
      if (parameters == null) {
        selectQuery = "SELECT * FROM Productos";
      }
      else {
        selectQuery =
          "SELECT * FROM Productos JOIN Categorias C on C.CategoryId = Productos.CategoryId " +
          "WHERE Productos.Name = '%" + parameters + "%'" + " OR Productos.Description ='%" +
          parameters + "%' OR Productos.Price = '%" + parameters +
          "%' OR Productos.CategoryId = '%" + parameters + "%';";
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
              ProductPrice = myReader.GetDouble(3),
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
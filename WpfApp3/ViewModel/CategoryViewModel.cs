using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfApp3.Model;

namespace WpfApp3.ViewModel;

internal class CategoryViewModel {
  public IList<CategoryModel> Categorias { get; set; } = new List<CategoryModel>();

  public CategoryViewModel() {
    var cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" +
                         MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" +
                         MainWindow.BaseDatos + ";";
    //string cadenaConexion = "server=192.168.1.208; port=3306; user id=Usuario; password=Lvepv.js12; database=LasDiademasDeMisHijas;";
    MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
    string mySelectQuery = "SELECT CategoryId, Name FROM Categorias";
    MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
    conexionBd.Open();
    MySqlDataReader myReader;
    myReader = myCommand.ExecuteReader();

    if (myReader.HasRows) {
      while (myReader.Read()) {
        Categorias.Add(new CategoryModel());
      }
    }

    myReader.Close();
    conexionBd.Close();
  }
}
using System.Collections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WpfApp3.Model;

namespace WpfApp3.ViewModel {

    internal class ProductViewModel {
        public IList<ProductoModel> Productos { get; set; } = new List<ProductoModel>();
        public ProductViewModel() {
            //string cadenaConexion = "server=" + MainWindow.Servidor + "; port=" + MainWindow.Puerto + "; user id=" + MainWindow.Usuario + "; password=" + MainWindow.Contrasena + "; database=" + MainWindow.BaseDatos + ";";
            string cadenaConexion = MainWindow.CadenaConexion;
            MySqlConnection conexionBd = new MySqlConnection(cadenaConexion);
            string mySelectQuery =
                "SELECT ProductId, Name, Description, Price, C.Nombre, Stock, Image FROM Productos JOIN Categorias C on C.CategoryId = Productos.CategoryId";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, conexionBd);
            conexionBd.Open();
            MySqlDataReader myReader;
            myReader = myCommand.ExecuteReader();


            if (myReader.HasRows) {
                while (myReader.Read()) {
                    Productos.Add(new ProductoModel() {
                        ProductId = myReader.GetInt32(0),
                        ProductName = myReader.GetString(1),
                        ProductDescription = myReader.GetString(2),
                        ProductPrice = myReader.GetDouble(3),
                        ProductCategory = myReader.GetString(4),
                        ProductStock = myReader.GetInt32(5),
                        //ProductCategory = category,
                        ProductImage = myReader.GetString(6)
                    });
                }
            }

            myReader.Close();
            conexionBd.Close();
        }
    }
}
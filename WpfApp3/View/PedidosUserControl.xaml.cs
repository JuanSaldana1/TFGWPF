using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfApp3.Model;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class PedidosUserControl {
  public PedidosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new OrderViewModel();
  }

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var pedido = new OrderModel();
    var viewModel = new OrderViewModel();
    try {
      pedido.ProductId = Convert.ToInt32(OrderProductIdEditText.Text);
      pedido.OrderDate = Convert.ToDateTime(OrderDatePicker.Text);
      pedido.Quantity = Convert.ToInt32(OrderQuantityEditText.Text);
      pedido.LineId = Convert.ToInt32(ProductCategoryIdEditText.Text);
      pedido.UserId = Convert.ToInt32(ProductStockEditText.Text);
      try {
        SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(pedido)
          ? "Producto insertado correctamente"
          : "Error al insertar el producto");
        viewModel.GetAllOrders();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    viewModel.GetAllOrders();
  }

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show(nIe.Message);
    }

    return null;
  }
}
using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class PedidosUserControl {
  public PedidosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new OrderViewModel();
  }

  private void ButtonDeleteOrder_Click(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item?.OrderId;
      var viewModel = new OrderViewModel();
      try {
        if (!(id?.Equals("") || id?.Equals(null))) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.DeleteMethod(id)
            ? $"Pedido con id {id} eliminado correctamente"
            : $"Error al Eliminar el pedido con id {id}");
        }

        MyListView.DataContext = new OrderViewModel();
        viewModel.GetOrdersForView();
      }
      catch (Exception exception) {
        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
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
          ? "Pedido creado correctamente"
          : "Error al crear el pedido");
        MyListView.DataContext = new OrderViewModel();
        viewModel.GetOrdersForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }
}
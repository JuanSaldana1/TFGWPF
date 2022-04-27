using System;
using System.Windows;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

public partial class PedidosUserControl {
    public PedidosUserControl() {
        InitializeComponent();
        MyListView.DataContext = new OrderViewModel();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
    }

    private Action Deshacer() {
        try { }
        catch (NotImplementedException nIe) {
            _ = MessageBox.Show(nIe.Message);
        }

        return null;
    }
}
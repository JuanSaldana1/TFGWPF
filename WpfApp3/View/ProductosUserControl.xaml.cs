using MaterialDesignThemes.Wpf;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Model;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para ProductosUserControl.xaml
    /// </summary>
    public partial class ProductosUserControl {
        public ProductosUserControl() {
            InitializeComponent();
            MyListView.DataContext = new ProductViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
        }

        private Action Deshacer() {
            try {
            }
            catch (NotImplementedException nIe) {
                _ = MessageBox.Show(nIe.Message);
            }
            return null;
        }

        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent(
            "DialogClosing1",
            RoutingStrategy.Bubble,
            typeof(DialogClosingEventHandler),
            typeof(DialogHost)
        );

        private void ArticuloChanged(object sender, TextChangedEventArgs e) {
        }
    }
}
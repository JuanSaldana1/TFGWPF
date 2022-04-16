using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para ProductosUserControl.xaml
    /// </summary>
    public partial class ProductosUserControl {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ProductosUserControl() {
            InitializeComponent();
            MyListView.DataContext = new ProductViewModel();
            try {
                Logger.Info("Hello world");
                Console.ReadKey();
            }
            catch (Exception ex) {
                Logger.Error(ex, "Goodbye cruel world");
            }
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

        public static readonly RoutedEvent DialogClosingEvent = EventManager.RegisterRoutedEvent(
            "DialogClosing1",
            RoutingStrategy.Bubble,
            typeof(DialogClosingEventHandler),
            typeof(DialogHost)
        );

        private void ArticuloChanged(object sender, TextChangedEventArgs e) {
            Console.WriteLine("Artículo cambiado");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
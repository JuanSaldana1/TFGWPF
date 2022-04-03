using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;


namespace WpfApp3.View {
    public partial class CategoriasUserControl : UserControl {
        public CategoriasUserControl() {
            InitializeComponent();
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

        private void CategoryChanged(object sender, TextChangedEventArgs e) { }
    }
}
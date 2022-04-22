using System;
using System.Windows;
using System.Windows.Controls;
using MessageBox = ModernWpf.MessageBox;


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
                _ = MessageBox.Show("This is a test text!", "Some title", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
            }

            return null;
        }

        private void CategoryChanged(object sender, TextChangedEventArgs e) { }
    }
}
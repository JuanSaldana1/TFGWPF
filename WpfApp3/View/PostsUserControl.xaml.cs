using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    public partial class PostsUserControl : UserControl {
        public PostsUserControl() {
            InitializeComponent();
            MyListView.DataContext = new PostViewModel();
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

        private void PostChanged(object sender, TextChangedEventArgs e) { }
    }
}
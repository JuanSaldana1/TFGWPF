using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp3.ViewModel;

namespace WpfApp3.View {
    /// <summary>
    /// Lógica de interacción para PostsUserControl.xaml
    /// </summary>
    public partial class PostsUserControl {
        public PostsUserControl() {
            InitializeComponent();
            MyListView.DataContext = new PostViewModel();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
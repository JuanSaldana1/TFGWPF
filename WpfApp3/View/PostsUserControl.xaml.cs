using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

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

  public void ButtonUpdateChanges_Click(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("");
  }

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show(nIe.Message);
    }

    return null;
  }

  public void PostChanged(object sender, TextChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue(e.ToString());
    SnackbarSeven.MessageQueue?.Enqueue(e.Changes.ToString());
  }
}
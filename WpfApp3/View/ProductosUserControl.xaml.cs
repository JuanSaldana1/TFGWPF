using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp3.ViewModel;

namespace WpfApp3.View;

/// <summary>
/// Lógica de interacción para ProductosUserControl.xaml
/// </summary>
public partial class ProductosUserControl {
  private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

  public ProductosUserControl() {
    InitializeComponent();
    MyListView.DataContext = new ProductViewModel();
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
    SnackbarSeven.MessageQueue?.Enqueue($"BasicRatingBar value changed from {e.Changes}");
  }

  private void ButtonAddStock_OnClick(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Has aumentado tu stock en 1");
  }

  private void ButtonRestStock_OnClick(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Has reducido tu stock en 1");
  }

  private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");
  }

  private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
    => Debug.WriteLine($"La valoración ha cambiado de {e.OldValue} a {e.NewValue} estrellas.");
}
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class CategoriasUserControl {
  public CategoriasUserControl() {
    InitializeComponent();
    /*CategoriesListBox.DataContext = new CategoryViewModel();*/
    CategoriesDataGrid.ItemsSource = new CategoryViewModel().Categorias;
  }

  /*private void Button_Click(object sender, RoutedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Hello world! Showing message for seconds.", "hola", Deshacer(), false);
  }*/

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show("This is a test text!", "Some title", MessageBoxButton.YesNoCancel,
        MessageBoxImage.Question);
    }

    return null;
  }

  /*private void CategoryChanged(object sender, TextChangedEventArgs e) {
    SnackbarSeven.MessageQueue?.Enqueue("Artículo cambiado");
  }*/

  /*private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    Debug.WriteLine($"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

    //you can cancel the dialog close:
    //eventArgs.Cancel();

    if (!Equals(eventArgs.Parameter, true))
      return;

    if (!string.IsNullOrWhiteSpace(CategoryNameEditText.Text))
      CategoriesListBox.Items.Add(CategoryNameEditText.Text.Trim());
  }*/

  /*private void DeleteCategoryButton_OnClick(object sender, RoutedEventArgs e) {
    var selectedItem = CategoriesListBox.SelectedItem;
    if (selectedItem == null)
      return;

    CategoriesListBox.Items.Remove(selectedItem);
  }*/
}
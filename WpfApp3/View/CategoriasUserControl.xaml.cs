using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WpfApp3.Model;
using WpfApp3.ViewModel;
using MessageBox = ModernWpf.MessageBox;

namespace WpfApp3.View;

public partial class CategoriasUserControl {
  public CategoriasUserControl() {
    InitializeComponent();
    CategoriesListBox.DataContext = new CategoryViewModel();
  }

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var category = new CategoryModel();
    var viewModel = new CategoryViewModel();
    try {
      category.CategoryName = CategoryNameEditText.Text;
      try {
        if (!string.IsNullOrEmpty(CategoryNameEditText.Text)) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.InsertMethod(category)
            ? "Categoría Creada correctamente"
            : "Error al crear la categoría");
        }

        CategoriesListBox.DataContext = new CategoryViewModel();
        viewModel.GetCategoriesForView();
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        throw;
      }
    }
    catch (Exception e) {
      MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
      throw;
    }
  }

  private void ButtonDeleteCategory_Click(object sender, RoutedEventArgs e) {
    try {
      var button = sender as Button;
      dynamic item = button?.DataContext;
      var id = item?.CategoryId;
      var viewModel = new CategoryViewModel();
      try {
        if (!(id?.Equals("") || id?.Equals(null))) {
          SnackbarSeven.MessageQueue?.Enqueue(viewModel.DeleteMethod(id)
            ? $"Categoría con id {id} eliminada correctamente"
            : $"Error al Eliminar la categoría con id {id}");
        }

        CategoriesListBox.DataContext = new CategoryViewModel();
        viewModel.GetCategoriesForView();
      }
      catch (Exception exception) {
        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        throw;
      }
    }
    catch (Exception exception) {
      MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      throw;
    }
  }
}
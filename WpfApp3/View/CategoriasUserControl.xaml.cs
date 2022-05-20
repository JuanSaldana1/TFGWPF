using System;
using System.Diagnostics;
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

  private Action Deshacer() {
    try { }
    catch (NotImplementedException nIe) {
      _ = MessageBox.Show("This is a test text!", "Some title", MessageBoxButton.YesNoCancel,
        MessageBoxImage.Question);
    }

    return null;
  }

  private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) {
    var category = new CategoryModel();
    var viewModel = new CategoryViewModel();
    try {
      category.CategoryName = CategoryNameEditText.Text;
      try {
        if (CategoryNameEditText.Text != "") {
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
}
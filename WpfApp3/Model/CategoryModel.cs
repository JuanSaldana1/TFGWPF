using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model;

internal class CategoryModel {
  private int categoryId;
  private string categoryName;

  public int CategoryId {
    get => categoryId;
    set {
      categoryId = value;
      OnPropertyChanged("CategoryId");
    }
  }

  public string CategoryName {
    get => categoryName;
    set {
      categoryName = value;
      OnPropertyChanged("CategoryName");
    }
  }

  #region INotifyPropertyChanged Members

  public event PropertyChangedEventHandler PropertyChanged;

  private void OnPropertyChanged(string propertyName) {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
    if (!Equals(field, newValue)) {
      field = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      return true;
    }

    return false;
  }

  private IEnumerable _ventas;

  public IEnumerable Ventas {
    get => _ventas;
    set => SetProperty(ref _ventas, value);
  }

  #endregion
}
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model;

public class OrderModel {
  private int orderId;
  private int productId;
  private DateTime orderDate;
  private int quantity;
  private int lineId;
  private string productName;
  private string userName;

  public int OrderId {
    get => orderId;
    set {
      orderId = value;
      OnPropertyChanged("OrderId");
    }
  }

  public int ProductId {
    get => productId;
    set {
      productId = value;
      OnPropertyChanged("ProductId");
    }
  }

  public DateTime OrderDate {
    get => orderDate;
    set {
      orderDate = value;
      OnPropertyChanged("OrderDate");
    }
  }

  public int Quantity {
    get => quantity;
    set {
      quantity = value;
      OnPropertyChanged("Quantity");
    }
  }

  public int LineId {
    get => lineId;
    set {
      lineId = value;
      OnPropertyChanged("LineId");
    }
  }

  public string ProductName {
    get => productName;
    set {
      productName = value;
      OnPropertyChanged("ProductName");
    }
  }
  
  public string UserName {
    get => userName;
    set {
      userName = value;
      OnPropertyChanged("UserName");
    }
  }

  #region INotifyPropertyChanged Members

  public event PropertyChangedEventHandler PropertyChanged;

  private void OnPropertyChanged(string propertyName) {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  private void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
    if (!Equals(field, newValue)) {
      field = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  private IEnumerable orders;

  public IEnumerable Orders {
    get => orders;
    set => SetProperty(ref orders, value);
  }

  #endregion
}
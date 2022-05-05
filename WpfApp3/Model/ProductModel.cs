﻿using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model;

internal class ProductoModel : INotifyPropertyChanged {
  private int productId;
  private string productName;
  private string productDescription;
  private double productPrice;
  private int categoryId;
  private string productCategory;
  private int productStock;
  private int productRating;
  private string productImage;
  private int postId;

  public int ProductId {
    get => productId;
    set {
      productId = value;
      OnPropertyChanged("ProductId");
    }
  }

  public string ProductName {
    get => productName;
    set {
      productName = value;
      OnPropertyChanged("ProductName");
    }
  }

  public string ProductDescription {
    get => productDescription;
    set {
      productDescription = value;
      OnPropertyChanged("ProductDescription");
    }
  }

  public double ProductPrice {
    get => productPrice;
    set {
      productPrice = value;
      OnPropertyChanged("ProductPrice");
    }
  }

  public int CategoryId {
    get => categoryId;
    set {
      categoryId = value;
      OnPropertyChanged("CategoryId");
    }
  }

  public int ProductStock {
    get => productStock;
    set {
      productStock = value;
      OnPropertyChanged("ProductStock");
    }
  }

  public string ProductCategory {
    get => productCategory;
    set {
      productCategory = value;
      OnPropertyChanged("ProductCategory");
    }
  }

  public string ProductImage {
    get => productImage;
    set {
      productImage = value;
      OnPropertyChanged("ProductImage");
    }
  }

  public int PostId {
    get => postId;
    set {
      postId = value;
      OnPropertyChanged("PostId");
    }
  }

  public int ProductRating {
    get => productRating;
    set {
      productRating = value;
      OnPropertyChanged("ProductRating");
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

  private IEnumerable ventas;

  public IEnumerable Ventas {
    get => ventas;
    set => SetProperty(ref ventas, value);
  }

  #endregion
}
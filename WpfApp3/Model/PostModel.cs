using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model;

public class PostModel {
  private int idPost;
  private string title;
  private string description;
  private string url;
  private DateTime publishedAt;
  private bool isFavorite;
  private string image1;
  private string image2;

  public int PostId {
    get => idPost;
    set {
      idPost = value;
      OnPropertyChanged("PostId");
    }
  }

  public string PostTitle {
    get => title;
    set {
      title = value;
      OnPropertyChanged("PostTitle");
    }
  }

  public string PostDescription {
    get => description;
    set {
      description = value;
      OnPropertyChanged("PostDescription");
    }
  }

  public string PostUrl {
    get => url;
    set {
      url = value;
      OnPropertyChanged("PostUrl");
    }
  }

  public DateTime PostPublishDate {
    get => publishedAt.Date;
    set {
      publishedAt = value.Date;
      OnPropertyChanged("PostPublishDate");
    }
  }

  public bool IsFavorite {
    get => isFavorite;
    set {
      isFavorite = value;
      OnPropertyChanged("IsFavourite");
    }
  }

  public string PostFirstImage {
    get => image1;
    set {
      image1 = value;
      OnPropertyChanged("PostFirstImage");
    }
  }

  public string PostSecondImage {
    get => image2;
    set {
      image2 = value;
      OnPropertyChanged("PostSecondImage");
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

  private System.Collections.IEnumerable posts;

  public System.Collections.IEnumerable Posts {
    get => posts;
    set => SetProperty(ref posts, value);
  }

  #endregion
}
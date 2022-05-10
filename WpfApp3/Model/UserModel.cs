using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model; 

internal class UserModel : INotifyPropertyChanged {
  private string email;
  private string follower;
  private string name;
  private string profilePhoto;
  private string rol;
  private string surname;
  private int userId;
  private string username;

  public int UserId {
    get => userId;
    set {
      userId = value;
      OnPropertyChanged("UserId");
    }
  }

  public string Username {
    get => username;
    set {
      username = value;
      OnPropertyChanged("Username");
    }
  }

  public string Name {
    get => name;
    set {
      name = value;
      OnPropertyChanged("Name");
    }
  }

  public string Email {
    get => email;
    set {
      email = value;
      OnPropertyChanged("Email");
    }
  }

  public string Surname {
    get => surname;
    set {
      surname = value;
      OnPropertyChanged("Surname");
    }
  }

  public string Follower {
    get => follower;
    set {
      follower = value;
      OnPropertyChanged("Follower");
    }
  }

  public string Rol {
    get => rol;
    set {
      rol = value;
      OnPropertyChanged("Rol");
    }
  }

  public string ProfilePhoto {
    get => profilePhoto;
    set {
      profilePhoto = value;
      OnPropertyChanged("ProfilePhoto");
    }
  }

  #region INotifyPropertyChanged Members

  public event PropertyChangedEventHandler PropertyChanged;

  private void OnPropertyChanged(string propertyName) {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
    if (Equals(field, newValue)) return false;

    field = newValue;
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    return true;
  }

  private IEnumerable ventas;

  public IEnumerable Ventas {
    get => ventas;
    set => SetProperty(ref ventas, value);
  }

  #endregion
}
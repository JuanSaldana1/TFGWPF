using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model {
    internal class UserModel : Connection {
        private int userId;
        private string username;
        private string name;
        private string email;
        private string surname;
        //private enum rol;
        private bool follower;
        private string profilePhoto;

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
        public bool Follower {
            get => follower;
            set {
                follower = value;
                OnPropertyChanged("Follower");
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
            if (!Equals(field, newValue)) {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private System.Collections.IEnumerable ventas;

        public System.Collections.IEnumerable Ventas { get => ventas; set => SetProperty(ref ventas, value); }
        #endregion
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model {
    internal class CategoryModel {
        public int CategoryId {
            get => CategoryId;
            set {
                CategoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }
        public int CategoryName {
            get => CategoryName;
            set {
                CategoryName = value;
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

        private System.Collections.IEnumerable _ventas;

        public System.Collections.IEnumerable Ventas { get => _ventas; set => SetProperty(ref _ventas, value); }
        #endregion
    }
}
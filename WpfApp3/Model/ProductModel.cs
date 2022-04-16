using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model {
    internal class ProductoModel : Connection {
        private int productId;
        private string productName;
        private string productDescription;
        private double productPrice;
        private string productCategory;
        private int productStock;
        private string productImage;

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

        public string ProductImage {
            get => productImage;
            set {
                productImage = value;
                OnPropertyChanged("ProductImage");
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
        //internal CategoryModel ProductCategory { get => productCategory; set => productCategory = value; }

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

        private System.Collections.IEnumerable ventas;

        public System.Collections.IEnumerable Ventas {
            get => ventas;
            set => SetProperty(ref ventas, value);
        }

        #endregion
    }
}
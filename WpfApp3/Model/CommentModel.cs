using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.Model {
    public class CommentModel {
        private int commentId;
        private string commentTitle;
        private string commentContent;
        private DateTime commentPublishDate;
        private int userId;
        private bool isAnonymous;
        private string userName;
        private string userEmail;
        private string profilePhoto;

        public int CommentId {
            get => commentId;
            set {
                commentId = value;
                OnPropertyChanged("CommentId");
            }
        }

        public string CommentTitle {
            get => commentTitle;
            set {
                commentTitle = value;
                OnPropertyChanged("CommentTitle");
            }
        }

        public string CommentContent {
            get => commentContent;
            set {
                commentContent = value;
                OnPropertyChanged("CommentContent");
            }
        }

        public DateTime CommentPublishDate {
            get => commentPublishDate;
            set {
                commentPublishDate = value;
                OnPropertyChanged("CommentPublishDate");
            }
        }

        public int CommentUserId {
            get => userId;
            set {
                userId = value;
                OnPropertyChanged("CommentUserId");
            }
        }

        public bool IsAnonymous {
            get => isAnonymous;
            set {
                isAnonymous = value;
                OnPropertyChanged("IsAnonymous");
            }
        }

        public string UserName {
            get => userName;
            set {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        
        public string UserEmail {
            get => userEmail;
            set {
                userEmail = value;
                OnPropertyChanged("UserEmail");
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
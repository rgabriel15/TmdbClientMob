using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TmdbClientMob.ViewModels
{
    /// <summary>
    /// Base ViewModel class to all ViewModel classes.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Constants
        //Page length when json returns collection.
        protected const byte PageSize = 20;
        #endregion

        #region Properties
        bool isBusy = false;
        /// <summary>
        /// Flag to page loading.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                var ret = false;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => ret = isBusy);
                return ret;
            }
            set
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    SetProperty(ref isBusy, value));
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

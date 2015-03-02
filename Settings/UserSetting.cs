using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Settings
{
	public class UserSetting : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		protected bool SetProperty<T> (ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Object.Equals (storage, value)) {
				return false;
			}

			storage = value;
			OnPropertyChanged (propertyName);
			return true;
		}

		private string _companyName;
		public string companyName 
		{ 
			get { return _companyName; } 
			set { SetProperty (ref _companyName, value); }
		}

		private double _pi;
		public double pi 
		{ 
			get { return _pi; } 
			set { SetProperty (ref _pi, value); }
		}

		private bool _onOff;
		public bool onOff 
		{ 
			get { return _onOff; } 
			set { SetProperty (ref _onOff, value); }
		}			
	}
}


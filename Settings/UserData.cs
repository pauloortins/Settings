using System;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace Settings
{
	public static class UserData
	{
		public static object GetPropertyValue(string propertyName)
		{
			return UserSetting.GetType().GetProperties().SingleOrDefault(pi => pi.Name == propertyName).GetValue(userSetting, null);
		}

		public static void SetPropertyValue<T>(string propertyName, T value)
		{
			UserSetting.GetType().GetProperties().SingleOrDefault(pi => pi.Name == propertyName).SetValue(userSetting, value);
		}

		private static string pUserSettingsFile;

		private static UserSetting userSetting;

		private static UserSetting CreateNewSettings() 
		{
			var userSetting = new UserSetting ();
			userSetting.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				Serialiser.XmlSerializeObject(userSetting, UserSettingsFile);
			};

			Serialiser.XmlSerializeObject(userSetting, UserSettingsFile);
			return userSetting;
		}

		private static UserSetting CreateSettingsFromFile()
		{
			var userSetting = Serialiser.XmlDeserializeObject<UserSetting>(UserSettingsFile);
			userSetting.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				Serialiser.XmlSerializeObject(userSetting, UserSettingsFile);
			};

			return userSetting;
		}

		public static UserSetting UserSetting
		{
			get
			{
				if (userSetting == null)
				{
					if (File.Exists(UserSettingsFile))
					{
						userSetting = CreateSettingsFromFile ();
					}
					else
					{
						userSetting = CreateNewSettings ();
					}
				}

				return userSetting;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value is null!");
				}
				// end if

				userSetting = value;
				if (File.Exists(UserSettingsFile))
				{
					File.Delete(UserSettingsFile);
				}

				Serialiser.XmlSerializeObject(userSetting, UserSettingsFile);
			}
		}

		public static string UserSettingsFile
		{
			get
			{
				if (string.IsNullOrEmpty(pUserSettingsFile))
				{
					pUserSettingsFile = Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "UserSettings.xml");
				}

				return pUserSettingsFile;
			}
		}

		public static string Company
		{
			get
			{
				return UserSetting.companyName;
			}

			set
			{
				UserSetting.companyName = value;
			}
		}

		public static double Pi
		{
			get
			{
				return UserSetting.pi;
			}

			set
			{
				UserSetting.pi = value;
			}
		}

		public static bool OnOff
		{
			get
			{
				return UserSetting.onOff;
			}

			set
			{
				UserSetting.onOff = value;
			}
		}
	}
}


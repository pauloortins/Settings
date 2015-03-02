using System;
using System.IO;
using System.Xml.Serialization;

namespace Settings
{
	public class Serialiser
	{
		public static T XmlDeserializeObject<T>(string filePath)
		{
			using (var sr = new StreamReader(filePath))
			{
				var xmlSer = new XmlSerializer(typeof(T));
				return (T)xmlSer.Deserialize(sr);
			}
		}

		public static void XmlSerializeObject<T>(T obj, string filePath)
		{
			using (var sw = new StreamWriter(filePath))
			{
				var xmlSer = new XmlSerializer(typeof(T));
				xmlSer.Serialize(sw, obj);
			}
		}
	}
}


using System;
using System.ComponentModel;
using System.Configuration;

namespace Recon.Shared.Config
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting))
            {
                throw new Exception(key);
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
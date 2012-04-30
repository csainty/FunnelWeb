using System;
using System.Configuration;

namespace FunnelWeb.Settings
{
    public class AppHarborBootstrapSettings : IBootstrapSettings
    {
        public string Get(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        public void Set(string name, string value)
        {
            throw new Exception("Please use the AppHarbor configuration variables to change settings.");
        }

        public bool ConfigFileMissing()
        {
            return false;
        }
    }
}
using ApplicationCore.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace ApplicationCore.Concrete
{
    public class ConfigSettings : IConfigSettings
    {
        IConfiguration _config;

        public ConfigSettings(IConfiguration Config)
        {
            _config = Config;
        }
        public string OrderDBConnString => Encoding.UTF8.GetString(Convert.FromBase64String(_config["OrderDatabaseSettings:ConnectionString"]) );
        public string OrderDBName
        {
            get
            {
                return _config["OrderDatabaseSettings:DatabaseName"];
            }
        }
        public string OrderDBCollection
        {
            get
            {
                return _config["OrderDatabaseSettings:CollectionName"];
            }
        }

        public string BootstrapServer
        {
            get
            {
                return _config["Kafka:BootstrapServer"];
            }
        }
            
}
}

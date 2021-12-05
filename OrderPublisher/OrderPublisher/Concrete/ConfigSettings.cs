using ApplicationCore.Abstract;
using Microsoft.Extensions.Configuration;
using OrderPublisher.Abstract;
using System;
using System.Text;

namespace OrderPublisher.Concrete
{
    public class ConfigSettings : IConfigSettings
    {
        IConfiguration _config;

        public ConfigSettings(IConfiguration Config)
        {
            _config = Config;
        }
        public string OrderDBConnString
        {
            get
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(_config.GetValue<string>("OrderDatabaseSettings:ConnectionString")));
            }
        }
        public string OrderDBName
        {
            get
            {
                return _config.GetValue<string>("OrderDatabaseSettings:DatabaseName");
            }
        }
        public string OrderDBCollection
        {
            get
            {
                return _config.GetValue<string>("OrderDatabaseSettings:CollectionName");
            }
        }

        public string BootstrapServer
        {
            get
            {
                return _config.GetValue<string>("Kafka:BootstrapServer");
            }
        }
            
}
}

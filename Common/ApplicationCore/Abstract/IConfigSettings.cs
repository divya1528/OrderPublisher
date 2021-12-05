
namespace ApplicationCore.Abstract
{
  public interface IConfigSettings
    {
        public string OrderDBConnString { get; }
        public string OrderDBName { get;  }
        public string OrderDBCollection { get;  }
        public string BootstrapServer { get;  }
    }
}

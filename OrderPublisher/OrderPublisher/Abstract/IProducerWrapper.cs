
using System.Threading.Tasks;

namespace OrderPublisher.Abstract
{
    public interface IProducerWrapper
    {
        Task WriteMessageAsync(string message);
    }
}

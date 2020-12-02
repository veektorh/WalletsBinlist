using RabbitMQ.Client;

namespace BinlistApi.Interfaces.Managers
{
    public interface IRabbitMqManager
    {
        IModel Connect();
    }
}
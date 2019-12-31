using System.Threading.Tasks;

namespace KBS.Messenger.Hubs {
    public interface IGatewayClient {
        Task ResponseProxy(string guid, string result);
    }
}

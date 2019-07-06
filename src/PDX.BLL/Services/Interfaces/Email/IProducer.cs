using System.Threading.Tasks;
using PDX.BLL.Model;

namespace PDX.BLL.Services.Interfaces.Email {
    public interface IProducer {
        Task RegisterEvent (string json);
        Task RegisterEvent (Event eEvent);
        string ToJson (object obj);
    }
}
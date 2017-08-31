using System.Threading.Tasks;

namespace Collectively.Tools.RedisSync.Framework
{
    public interface ISynchronizer
    {
        Task SynchronizeAsync();
    }
}
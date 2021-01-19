using System.Threading.Tasks;

namespace Rocky.Infra.Data.Persistence.Initialize
{
    public interface IDatabaseInitializer
    {
        void Initialize();
        Task InitializeAsync();
    }
}
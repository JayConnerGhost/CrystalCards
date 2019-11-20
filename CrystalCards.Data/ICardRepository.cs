
using System.Threading.Tasks;

namespace CrystalCards.Data
{
    public interface ICardRepository
    {
        Task<OpsStatus> Delete(int id);
    }
}
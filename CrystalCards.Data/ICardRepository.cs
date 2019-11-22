
using System.Threading.Tasks;
using CrystalCards.Models;

namespace CrystalCards.Data
{
    public interface ICardRepository
    {
        Task<OpsStatus> Delete(int id);
        Task<Card> Get(int id);
    }
}
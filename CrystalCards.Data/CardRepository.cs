using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrystalCards.Data
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OpsStatus> Delete(int id)
        {
            var target = await _context.Cards
                .Include(c => c.Points)
                .Include(c => c.ActionPoints)
                .Include(c => c.Links)

                .FirstOrDefaultAsync(x => x.Id == id);
            if (target == null)
            {
                return OpsStatus.TargetNotFound;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return OpsStatus.Failed;
            }

            return OpsStatus.Success;
        }
    }
}
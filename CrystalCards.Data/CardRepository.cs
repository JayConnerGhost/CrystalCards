using System;
using System.Threading.Tasks;
using CrystalCards.Models;
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
                _context.Remove(target);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return OpsStatus.Failed;
            }

            return OpsStatus.Success;
        }

        public async Task<Card> Get(int id)
        {
            var result = await _context.Cards
                .Include(x => x.Points)
                .Include(x => x.ActionPoints)
                .Include(x => x.Links)
                .FirstOrDefaultAsync(x => x.Id == id);
         return result;

        }

        public async Task<Card> Update(Card card)
        { 
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Add(Card card, string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName);
            var userEntity = _context.Users.Update(user);
            user.Cards.Add(card);

            await _context.SaveChangesAsync();
            return card;
        }
    }
}
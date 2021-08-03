using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Classes
{
    public class MainRepository : IMainRepository
    {
        private MainContext _context;

        public MainRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<QueuePlace> GetNextQueuePlace(QueuePlace currentQueuePlace)
        {
            if (_context.QueuePlaces.Count() == 0) return null;
            List<QueuePlace> sortedQueuePlaces = await _context.QueuePlaces.Where(x => x.EnteredAt.Year == 1).ToListAsync();
            sortedQueuePlaces.Sort((a, b) => a.EnqueuedAt.CompareTo(b.EnqueuedAt));
            if (currentQueuePlace == null)
            {
                if (sortedQueuePlaces.Count() > 0)
                    return sortedQueuePlaces[0];
                else
                    return null;
            }

            int index = sortedQueuePlaces.FindIndex(x => x.Id == currentQueuePlace.Id);

            if (index + 1 >= sortedQueuePlaces.Count()) return null;
            else return sortedQueuePlaces[index + 1];
        }

        public async Task Dequeue()
        {
            QueuePlace currentQueuePlace = await GetCurrentUser();
            QueuePlace nextQueuePlace = await GetNextQueuePlace(currentQueuePlace);

            if(currentQueuePlace != null)
            {
                currentQueuePlace.IsCurrent = false;
                _context.QueuePlaces.Update(currentQueuePlace);
            }

            if(nextQueuePlace != null)
            {
                nextQueuePlace.IsCurrent = true;
                nextQueuePlace.EnteredAt = DateTime.Now;
                _context.QueuePlaces.Update(nextQueuePlace);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Enqueue(QueuePlace enqueue)
        {
            _context.QueuePlaces.Add(enqueue);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<QueuePlace> GetCurrentQueue()
        {
            return _context.QueuePlaces.Where(x => x.EnteredAt.Year == 1).ToArray();
        }

        public async Task<QueuePlace> GetCurrentUser()
        {
            return await _context.QueuePlaces.FirstOrDefaultAsync(x => x.IsCurrent == true);
        }
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMainRepository
    {
        public Task Dequeue();

        public Task Enqueue(QueuePlace enqueue);

        public IEnumerable<QueuePlace> GetCurrentQueue();

        public Task<QueuePlace> GetCurrentUser();
    }
}

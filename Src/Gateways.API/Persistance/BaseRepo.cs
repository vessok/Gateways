using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Persistance {
    public abstract class BaseRepo {
        protected readonly AppDbContext _context;

        public BaseRepo(AppDbContext context) {
            _context = context;
        }

        public async Task SaveAllAsync() {
            await _context.SaveChangesAsync();
        }

        public void DiscardAll() {
            _context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }
    }
}

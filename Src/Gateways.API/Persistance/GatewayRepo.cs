using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gateways.Model;
using System;
using System.Linq;

namespace Gateways.Persistance {
    public interface IGatewayRepo {
        Task<IEnumerable<Gateway>> FindAsync(QueryGateway query);
        Task<Gateway> FindByIdAsync(string id);
        Task CreateAync(Gateway gateway);
        void Update(Gateway gateway);
        void Delete(Gateway gateway);
        Task SaveAllAsync();
    }

    public class GatewayRepo : BaseRepo, IGatewayRepo {
        public GatewayRepo(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Gateway>> FindAsync(QueryGateway queryGateway) {
            var q = _context.Gateways.Include(p => p.Devices).AsNoTracking();
            if (!String.IsNullOrEmpty(queryGateway.Name)) {
                q = q.Where(e => e.Name == queryGateway.Name);
            }
            return await q.ToListAsync();
        }

        public async Task<Gateway> FindByIdAsync(string Id) {
            var res = Guid.TryParse(Id, out Guid guid);
            if (!res) return null;
            return await _context.Gateways.FindAsync(guid);
        }

        public async Task CreateAync(Gateway gateway) {
            await _context.Gateways.AddAsync(gateway);
        }

        public void Update(Gateway gateway) {
            _context.Gateways.Update(gateway);
        }

        public void Delete(Gateway gateway) {
            _context.Gateways.Remove(gateway);
        }
    }
}

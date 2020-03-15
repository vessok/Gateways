using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Gateways.Model;

namespace Gateways.Persistance {
    public interface IDeviceRepo {
        Task<IEnumerable<Device>> FindAsync(QueryDevice queryDevice);
        Task<Device> FindByIdAsync(int id);
        Task CreateAync(Device device);
        void Update(Device device);
        void Delete(Device device);
        Task SaveAllAsync();
    }

    public class DeviceRepo : BaseRepo, IDeviceRepo {
        public DeviceRepo(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Device>> FindAsync(QueryDevice queryDevice) {
            var q = _context.Devices.Include(d => d.Gateway).AsNoTracking();
            
            if (queryDevice.Id.HasValue) {
                q = q.Where(d => d.Id == queryDevice.Id);
            }
            
            if (!String.IsNullOrEmpty(queryDevice.Vendor)) {
                q = q.Where(d => d.Vendor == queryDevice.Vendor);
            }

            return await q.ToListAsync();
        }
        
        public async Task<Device> FindByIdAsync(int Id) {
            return await _context.Devices.FindAsync(Id);
        }

        public async Task CreateAync(Device device) {
            await _context.Devices.AddAsync(device);
        }

        public void Update(Device device) {
            _context.Devices.Update(device);
        }

        public void Delete(Device device) {
            _context.Devices.Remove(device);
        }
    }
}

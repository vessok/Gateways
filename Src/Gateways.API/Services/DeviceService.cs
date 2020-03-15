using Gateways.Model;
using Gateways.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Services {
    public interface IDeviceService {
        Task<IEnumerable<Device>> FindAsync(QueryDevice queryDevice);
        Task<DeviceResponse> CreateAsync(Device device);
        Task<DeviceResponse> UpdateAsync(int Id, Device device);
        Task<DeviceResponse> DeleteAsync(int Id);
    }

    public class DeviceService : IDeviceService {
        private readonly IDeviceRepo _deviceRepo;
        private readonly IGatewayRepo _gatewayRepo;

        public DeviceService(IDeviceRepo deviceRepo, IGatewayRepo gatewayRepo) {
            _deviceRepo = deviceRepo;
            _gatewayRepo = gatewayRepo;
        }
        public Task<IEnumerable<Device>> FindAsync(QueryDevice queryDevice) {
            return _deviceRepo.FindAsync(queryDevice);
        }

        public async Task<DeviceResponse> CreateAsync(Device device) {
            try {
                var gateway = _gatewayRepo.FindByIdAsync(device.GatewayId.ToString()).Result;
                if (gateway != null && gateway.DeviceNumb >= 10) {
                    return new DeviceResponse("No more than 10 devices allowed per gateway.");
                } else {
                    gateway.DeviceNumb++;
                    _gatewayRepo.Update(gateway);
                    await _deviceRepo.CreateAync(device);
                    await _deviceRepo.SaveAllAsync();
                }
                return new DeviceResponse(device);
            } catch (Exception ex) {
                return new DeviceResponse($"An error occurred : {ex.Message}"); //handle concurency here
            }
        }

        public async Task<DeviceResponse> DeleteAsync(int Id) {
            var existing = await _deviceRepo.FindByIdAsync(Id);

            if (existing == null)
                return new DeviceResponse("Gateway not found.");

            try {
                _deviceRepo.Delete(existing);
                await _deviceRepo.SaveAllAsync();

                return new DeviceResponse(existing);
            } catch (Exception ex) {
                return new DeviceResponse($"An error occurred : {ex.Message}");
            }
        }

        

        public async Task<DeviceResponse> UpdateAsync(int Id, Device device) {
            var existing = await _deviceRepo.FindByIdAsync(Id);

            if (existing == null)
                return new DeviceResponse("Device not found.");

            existing.Vendor = device.Vendor;
            existing.Status = device.Status;

            try {
                _deviceRepo.Update(existing);
                await _deviceRepo.SaveAllAsync();

                return new DeviceResponse(existing);
            } catch (Exception ex) {
                return new DeviceResponse($"An error occurred : {ex.Message}");
            }
        }
    }
}

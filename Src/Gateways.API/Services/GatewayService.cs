using Gateways.Model;
using Gateways.Persistance;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateways.Services {
    public interface IGatewayService {
        Task<IEnumerable<Gateway>> FindAsync(QueryGateway query);
        Task<GatewayResponse> CreateAsync(Gateway gateway);
        Task<GatewayResponse> UpdateAsync(string Id, Gateway gateway);
        Task<GatewayResponse> DeleteAsync(string Id);
    }

    public class GatewayService : IGatewayService {
        private readonly IGatewayRepo _gatewayRepo;

        public GatewayService(IGatewayRepo gatewayRepo) {
            _gatewayRepo = gatewayRepo;
        }

        public Task<IEnumerable<Gateway>> FindAsync(QueryGateway query) {
            return _gatewayRepo.FindAsync(query);
        }

        public async Task<GatewayResponse> CreateAsync(Gateway gateway) {
            try {
                await _gatewayRepo.CreateAync(gateway);
                await _gatewayRepo.SaveAllAsync();

                return new GatewayResponse(gateway);
            } catch (Exception ex) {
                return new GatewayResponse($"An error occurred : {ex.Message}");
            }
        }

        public async Task<GatewayResponse> DeleteAsync(string Id) {
            var existing = await _gatewayRepo.FindByIdAsync(Id);

            if (existing == null)
                return new GatewayResponse("Gateway not found.");

            try {
                _gatewayRepo.Delete(existing);
                await _gatewayRepo.SaveAllAsync();

                return new GatewayResponse(existing);
            } catch (Exception ex) {
                return new GatewayResponse($"An error occurred : {ex.Message}");
            }
        }

        public async Task<GatewayResponse> UpdateAsync(string Id, Gateway gateway) {
            var existing = await _gatewayRepo.FindByIdAsync(Id);

            if (existing == null)
                return new GatewayResponse("Gateway not found.");

            existing.Name = gateway.Name;
            existing.IP = gateway.IP;

            try {
                _gatewayRepo.Update(existing);
                await _gatewayRepo.SaveAllAsync();

                return new GatewayResponse(existing);
            } catch (Exception ex) {
                return new GatewayResponse($"An error occurred : {ex.Message}");
            }
        }
    }
}

using OptiFuelMaui.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiFuelMaui.Services
{
    public class ApiService
    {
        private readonly RestClient _client;
        public ApiService()
        {
            _client = new RestClient("http://192.168.4.216:5232/api"); 
        }

        private RestRequest CreateRequest(string resource, Method method, object body = null)
        {
            var request = new RestRequest(resource, method);
            if (body != null)
            {
                request.AddJsonBody(body);
            }
            return request;
        }

        public async Task<List<Planning>> GetPlanningsAsync()
        {
            try
            {
                var request = CreateRequest("planning", Method.Get);
                var response = await _client.ExecuteAsync<List<Planning>>(request);
                if (response.IsSuccessful)
                {
                    return response.Data ?? new List<Planning>();
                }
                else
                {
                    Console.WriteLine($"Error fetching plannings: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPlanningsAsync: {ex.Message}");
            }
            return new List<Planning>();
        }

        public async Task<Planning> GetPlanningAsync(Guid id)
        {
            var request = CreateRequest($"planning/{id}", Method.Get);
            var response = await _client.ExecuteAsync<Planning>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            return null;
        }

        public async Task<bool> AddPlanningAsync(Planning planning)
        {
            var request = CreateRequest("planning", Method.Post, planning);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        public async Task<bool> EditPlanningAsync(Guid id, Planning planning)
        {
            var request = CreateRequest($"planning/{id}", Method.Put, planning);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        public async Task<bool> DeletePlanningAsync(Guid planningId)
        {
            var request = CreateRequest($"planning/{planningId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }


    }
}

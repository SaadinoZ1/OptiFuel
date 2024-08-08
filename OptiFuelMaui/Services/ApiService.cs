using OptiFuelMaui.Dtos;
using OptiFuelMaui.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OptiFuelMaui.Services
{
    public class ApiService
    {
        private readonly RestClient _client;
        public ApiService()
        {     
            _client = new RestClient("http://192.168.1.2:5232/api");
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
                    Console.WriteLine($"Successfully fetched {response.Data?.Count ?? 0} plannings.");
                    return response.Data;
                }
                else
                {
                    Console.WriteLine($"Error fetching plannings: {response.ErrorMessage}");
                    return new List<Planning>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPlanningsAsync: {ex.Message}");
                return new List<Planning>();
            }
        }

        public async Task<Planning> GetPlanningAsync(Guid id)
        {
            var request = CreateRequest($"planning/{id}", Method.Get);
            var response = await _client.ExecuteAsync<Planning>(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine($"Successfully fetched planning with ID {id}.");
                return response.Data;
            }
            else
            {
                Console.WriteLine($"Error fetching planning with ID {id}: {response.ErrorMessage}");
                return null;
            }
        }

        public async Task<Planning> AddPlanningAsync(Planning planning)
        {
            try
            {
                var request = CreateRequest("planning", Method.Post, planning);
                var response = await _client.ExecuteAsync<Planning>(request);
                if (response.IsSuccessful && response.Data != null)
                {
                    Console.WriteLine("Planning added successfully.");
                    return response.Data;
                }
            else
            {
                Console.WriteLine($"Error adding planning: {response.Content}");
                    return null;
            }
        }
                catch (Exception ex)
        {
            Console.WriteLine($"Exception in AddPlanningAsync: {ex.Message}");
            return null ;
        }
        }

        public async Task<Planning> EditPlanningAsync(Guid id, Planning planning)
        {
            try
            {
                var request = CreateRequest($"planning/{id}", Method.Put, planning);
                var response = await _client.ExecuteAsync<Planning>(request);
                Console.WriteLine($"EditPlanningAsyncResponse: {response.Content} ");
                if (response.IsSuccessful)
                {
                    Console.WriteLine("EditPlanningAsync: Planning updated successfully.");
                    return response.Data;
                }
                else
                {
                    Console.WriteLine($"EditPlanningAsync: Error: {response.ErrorMessage}");
                    Console.WriteLine($"EditPlanningAsync: Status: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in EditPlanningAsync: {ex.Message}");
                return null;
            }
}

        public async Task<bool> DeletePlanningAsync(Guid planningId)
        {
            var request = CreateRequest($"planning/{planningId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }


        public async Task<List<Centre>> GetCentresAsync()
        {
            try
            {
                var request = CreateRequest("centre", Method.Get);
                var response = await _client.ExecuteAsync<List<Centre>>(request);
                if (response.IsSuccessful)
                {
                    return response.Data ?? new List<Centre>();
                }
                else
                {
                    Console.WriteLine($"Error fetching centres: {response.ErrorMessage}");
                    return new List<Centre>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetCentresAsync: {ex.Message}");
                return new List<Centre>();

            }
        }

        public async Task<bool> AddValidationBLAsync(ValidationBLDto validationBLDto)
        {
            try
            {
                var request = new RestRequest("api/ValidationBL/AddValidationBL", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var jsonBody = JsonSerializer.Serialize(validationBLDto);
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

                Console.WriteLine($"Sending request: {jsonBody}");

                var response = await _client.ExecuteAsync(request);

                Console.WriteLine($"Response status: {response.StatusCode}");
                Console.WriteLine($"Response content: {response.Content}");

                if (!response.IsSuccessful)
                {
                    Console.WriteLine($"Error: {response.Content}");
                }
                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddValidationBLAsync: {ex.Message}");
                return false;
            }
        }

    }
}

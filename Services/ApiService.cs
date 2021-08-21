using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using aec_mvc_entity_framework.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace aec_mvc_entity_framework.Services 
{
    public class ApiService
    {
      public static async Task<List<Api>> GetApi()
      {
          HttpClient http = new HttpClient();

          var response = await http.GetAsync($"{Program.ApiHost}/api/candidatos");
          if(response.IsSuccessStatusCode)
          {
              var resultado = response.Content.ReadAsStringAsync().Result;
              var Api = JsonConvert.DeserializeObject<List<Api>>(resultado);
              return Api;
          }
          return new List<Api>();
      }
    }
}
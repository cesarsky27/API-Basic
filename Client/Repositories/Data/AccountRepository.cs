using API.Models;
using API.ViewModel;
using Client.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository <Account, String>
    {
    private readonly string request;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient httpClient;
    private readonly Address address;
        public AccountRepository(Address address, string request = "accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        //internal Task Auth(LoginVM login)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<JWTokenVM> Auth(LoginVM login)
        {
           JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }
    }
}

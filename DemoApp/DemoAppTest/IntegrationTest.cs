using DemoApp.Interfaces.Managers;
using DemoApp.Interfaces.Services;
using DemoApp.Managers;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using DemoApp;

namespace DemoAppTest
{
    [TestClass]
    public class IntegrationTest
    {

        private HttpClient httpClient { get; set; }
        public IntegrationTest()
        {
            var appfactory = new WebApplicationFactory<Startup>();
            httpClient = appfactory.CreateClient();
        }


        [TestMethod]
        public async Task IncrementCountTest()
        {
            var http = new HttpClient();
            var rewe = await http.GetAsync("https://google.com");

            var response = await httpClient.GetAsync("api/binlist/count");

            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();

            return;
        }


    }
}

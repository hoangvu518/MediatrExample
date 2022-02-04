using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Shared
{
    //public class TestServerFixture: IClassFixture<SqliteWebApplicationFactory<Program>>
    //{
    //    public readonly HttpClient HttpClient;
    //    private readonly SqliteWebApplicationFactory<Program> _factory;
    //    public TestServerFixture(SqliteWebApplicationFactory<Program> factory)
    //    {
    //        _factory = factory;
    //        HttpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
    //        {
    //            AllowAutoRedirect = false
    //        });
    //    }
    //}

    public class TestServerFixture: IDisposable
    {
        public readonly HttpClient HttpClient;
        private readonly SqliteWebApplicationFactory<Program> _factory;
        public TestServerFixture()
        {
            _factory = new SqliteWebApplicationFactory<Program>();
            HttpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
            _factory.Dispose();
        }
    }
}

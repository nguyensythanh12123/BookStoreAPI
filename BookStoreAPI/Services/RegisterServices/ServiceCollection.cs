using BookStoreAPI.Model;
using BookStoreAPI.Services.Dapper;
using BookStoreAPI.Services.Interfaces;
using BookStoreAPI.Services.Repositories;
using Microsoft.Extensions.Configuration;

namespace BookStoreAPI.Services.RegisterServices
{
    public static class ServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBook, BookRepository>();
            services.AddScoped<List<BookModel>>(sp => new List<BookModel>());
            return services;
        }
    }
}

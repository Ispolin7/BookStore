using BookStore.Controllers.RequestModels;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using BookStore.Services.Services;
using BookStore.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Common.Extensions
{
    public static class DIServicesExtension
    {
        /// <summary>
        /// Service collection extension for DI services
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>updated service collection</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILineItemService, LineItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }

        /// <summary>
        /// Service collection extension for DI validators
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>updated service collection</returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Author>, AuthorValidator>();
            services.AddScoped<IValidator<Book>, BookValidator>();
            services.AddScoped<IValidator<Order>, OrderValidator>();
            services.AddScoped<IValidator<Review>, ReviewValidator>();
            services.AddScoped<IValidator<BookAuthorsRequest>, BookAuthorValidator>();
            services.AddScoped<IValidator<DiscountRequest>, DiscountValidator>();

            return services;
        }
    }
}

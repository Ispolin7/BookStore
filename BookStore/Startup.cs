using AutoMapper;
using BookStore.Common.Extensions;
using BookStore.Controllers.Filters;
using BookStore.Controllers.RequestModels;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using BookStore.Services;
using BookStore.Services.Auth;
using BookStore.Services.Interfaces;
using BookStore.Services.Services;
using BookStore.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddAutoMapper();

            // Add DB Contexts.
            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;               
            })
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();

            // Add services.
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILineItemService, LineItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAccountService, AccountService>();

            // Add Validators.
            services.AddScoped<IValidator<Author>, AuthorValidator>();
            services.AddScoped<IValidator<Book>, BookValidator>();
            services.AddScoped<IValidator<Order>, OrderValidator>();
            services.AddScoped<IValidator<Review>, ReviewValidator>();
            services.AddScoped<IValidator<BookAuthorsRequest>, BookAuthorValidator>();
            services.AddScoped<IValidator<DiscountRequest>, DiscountValidator>();

            // Add Authentication
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new JWTValidationParameters();
                    });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.ConfigureExceptionHandler();                
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

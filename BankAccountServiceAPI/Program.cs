
using BankAccountServiceAPI.Infrastructure.CurrenciesSupport;
using BankAccountServiceAPI.Infrastructure.MockCustomerVerification;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using Swashbuckle.AspNetCore.SwaggerUI;
using BankAccountServiceAPI.Common.Behaviors;
using FluentValidation;
using MediatR;
using BankAccountServiceAPI.MiddleWare;

namespace BankAccountServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //«аглушки
            builder.Services.AddSingleton<IMockBankAccountRepository, MockBankAccountRepository>();
            builder.Services.AddSingleton<IMockCustomerVerification, MockCustomerVerification>();
            builder.Services.AddSingleton<ICurrencyService, CurrencyService>();

            // Add services to the container.
            
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue; //CascadeMode.Continue - провер€ть все правила.
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop; //дл€ одного правила(RuleFor) возвращать только первую ошибку.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Account Service API",
                    Description = "ћикросервис дл€ управлени€ банковскими счетами"
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Account API V1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

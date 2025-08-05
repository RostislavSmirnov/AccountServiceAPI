
using System.Reflection;
using BankAccountServiceAPI.Infrastructure.CurrenciesSupport;
using BankAccountServiceAPI.Infrastructure.MockCustomerVerification;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using BankAccountServiceAPI.Common.Behaviors;
using BankAccountServiceAPI.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI
{
    /// <summary>
    /// ����� ����������� �������, � ����� ������������
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //����� � �������� ������
                    options.Authority = "http://keycloak:8080/realms/bank-realm";
                    //��� ������ ������� � Keycloak. ������ ��������� � Client ID.
                    options.Audience = "account";
                    //��������� ���������� HTTPS ��� ��������� ����������
                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddAuthorization();

            //��������
            builder.Services.AddSingleton<IMockBankAccountRepository, MockBankAccountRepository>();
            builder.Services.AddSingleton<IMockCustomerVerification, MockCustomerVerification>();
            builder.Services.AddSingleton<ICurrencyService, CurrencyService>();

            // Add services to the container.
            
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue; //CascadeMode.Continue - ��������� ��� �������.
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop; //��� ������ �������(RuleFor) ���������� ������ ������ ������.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            builder.Services.AddSwaggerGen(options =>
            {
                // ReSharper disable once RedundantNameQualifier ��������� ������� ����, ��� ��������.
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Account Service API",
                    Description = "����������� ��� ���������� ����������� �������"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    //�������� ��� UI
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    //��� ���������, � ������� ����� ������������ �����
                    Name = "Authorization",
                    //������������ ������ (� ���������)
                    In = ParameterLocation.Header,
                    //��� �����
                    Type = SecuritySchemeType.Http,
                    //����� ��������������
                    Scheme = "bearer",
                    //������ ������
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();


            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //}
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Account API V1");
                options.RoutePrefix = "swagger";
            });


            //app.UseHttpsRedirection();
            
            app.UseCors(myAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

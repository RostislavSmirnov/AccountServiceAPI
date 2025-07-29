using AccountServiceAPI.Infrastructure.Currencies;
using AccountServiceAPI.Infrastructure.CustomerVerification;
using AccountServiceAPI.Infrastructure.Persistence;
using AccountServiceAPI.Infrastructure.ValidationBehavior;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;
using AccountServiceAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

//заглушки
builder.Services.AddSingleton<IAccountRepository, InMemoryBankAccountrepository>();
builder.Services.AddSingleton<ICustomerVerificationService, FakeCustomerVerificationService>();
builder.Services.AddSingleton<ICurrencyService, FakeCurrencyService>();

//MediatR и FluentValidation 


builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Account Service API",
        Description = "Микросервис для управления банковскими счетами"
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

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
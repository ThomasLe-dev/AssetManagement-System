using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.Service;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Infrastructure;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using AutoMapper;
using System.Text;
using System.Text.Json;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/// <summary>
/// Khởi tạo một instance mới của ExceptionMiddleware
/// </summary>
/// <param name="next">Delegate tiếp theo trong pipeline</param>
/// Created by: LB.Thành (16/07/2023)
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
            var errorsString = string.Join("; ", errors);

            var baseException = new BaseException()
            {
                ErrorCode = 400,
                UserMessage = "Lỗi nhập từ người dùng",
                DevMessage = "Lỗi nhập từ người dùng",
                TraceId = "",
                MoreInfo = "",
                Errors = errorsString
            };

            var result = new BadRequestObjectResult(baseException);
            result.ContentTypes.Add("application/json; charset=utf-8");

            return result;
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
        options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.WriteIndented = false;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration["ConnectionString"] ;
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IFixedAssetManager, FixedAssetManager>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<MISA.WebFresher052023.HCSN.Domain.Interface.IFixedAssetCategory, FixedAssetCategoryRepository>();
builder.Services.AddScoped<MISA.WebFresher052023.HCSN.Application.Interface.IFixedAssetCategoryService, FixedAssetCategoryService>();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyCors");
app.UseMiddleware<ExceptionMiddleware>();

app.Run();

using Microsoft.Extensions.Options;
using Utility.Project.Business.Middleware;
using Utility.Project.Business.Service.Abstraction.Mongo;
using Utility.Project.Business.Service.Concrete.Mongo;
using Utility.Project.Core.Data.Abstraction.Mongo;
using Utility.Project.Core.Data.Concrete.Mongo;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.AppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

//Configure
builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));




//IOptions Configure
builder.Services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);




//Service Configure
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseExceptionMiddleware<CustomExceptionMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

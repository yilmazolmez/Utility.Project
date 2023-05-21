using Microsoft.Extensions.Options;
using Utility.Project.API.Modules;
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

//Settings
builder.Services.AddSettings(configuration);



builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//DataAccess
builder.Services.AddDataAccess(configuration);




//IOptions Configure
builder.Services.AddOptionsConfigure(configuration);




//Service Configure
builder.Services.AddServices(configuration);


 

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

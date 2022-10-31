//Step 2 - Generate the C# models:
//Scaffold-DbContext "Server=.\sqlexpress;Database=ToDoAPI;Trusted_Connection=true;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

//Step 3 - add using statement
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Step 10 - Add Cors functionality to determine what websites can access the data in this application
//CORS stands for CROSS ORIGIN RESOURCE SHARING. By Default, browsers use this to block websites from requesting data unless
// that website has permission to do so. This code below determines what websites have access to CORS with this API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Step 4 - Add the Database Connection
//This will initialize th databse connection to be used in the controllers
builder.Services.AddDbContext<ToDoAPI.Models.ToDoAPIContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAPIDB"));
    });

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

//added to use cors
app.UseCors();

app.Run();

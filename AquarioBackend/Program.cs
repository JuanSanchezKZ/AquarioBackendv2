using AquarioBackend.Data;
using AquarioBackend.Mappings;
using AquarioBackend.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AquarioBackendDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AquarioBackendConnectionString")));
builder.Services.AddScoped<IForumThreadRepository, SQLForumThreadRepository>(); 
builder.Services.AddScoped<IReplyRepository, SQLReplyRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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

app.Run();

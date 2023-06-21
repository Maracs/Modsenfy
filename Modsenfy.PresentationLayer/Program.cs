using Microsoft.EntityFrameworkCore;
using Modsenfy.BusinessAccessLayer.Extentions;
using Modsenfy.BusinessAccessLayer.Services;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.PresentationLayer.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<ArtistRepository>();

builder.Services.AddScoped<TrackRepository>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserInfoRepository>();
builder.Services.AddScoped<ImageRepository>();
builder.Services.AddScoped<ImageTypeRepository>();
builder.Services.AddScoped<RequestRepository>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<AlbumService>();

builder.Services.AddScoped<UserService>();



builder.Services.AddControllers()
	.AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
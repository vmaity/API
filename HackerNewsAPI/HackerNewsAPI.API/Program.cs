using HackerNewsAPI.API.Profiles;
using HackerNewsAPI.API.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IHackerNewsService, HackerNewsService>();
builder.Services.AddRefitClient<IHackerNewsApi>().ConfigureHttpClient(httpClient =>
           httpClient.BaseAddress = new Uri(builder.Configuration["HackerNewsBaseApi"]));

builder.Services.AddAutoMapper(typeof(HackerNewsProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using BookStore.Core.Service;
using BookStore.Entity.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<BookRepository>();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<AuthorRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
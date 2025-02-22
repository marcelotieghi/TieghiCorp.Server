using TieghiCorp.Infra;
using TieghiCorp.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddUseCasesServices();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
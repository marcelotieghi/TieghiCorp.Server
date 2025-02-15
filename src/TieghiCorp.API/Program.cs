using TieghiCorp.API.Endpoint;
using TieghiCorp.Infra;
using TieghiCorp.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddUseCasesServices();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("TieghiCorpCorsPolicy", corsBuilder =>
        corsBuilder.WithOrigins(
            builder.Configuration["FrontendUrl"] ?? string.Empty,
            builder.Configuration["BackendUrl"] ?? string.Empty)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("TieghiCorpCorsPolicy");
app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();
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
    options.AddPolicy("corsPolicy", corsBuilder =>
        corsBuilder.WithOrigins("http://localhost:5011")
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

app.UseCors("corsPolicy");
app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();
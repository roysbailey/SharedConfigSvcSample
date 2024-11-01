using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/trainingtypeconfig/{domain}/{trainingType}", async (string domain, string trainingType) =>
{
    var config = await LoadConfigurationAsync(domain, trainingType);
    return config;
})
.WithName("GetConfig")
.WithOpenApi();

app.Run();


async Task<object?> LoadConfigurationAsync(string domain, string trainingType)
{
    var filePath = $"{domain}-{trainingType}.json";
    if (!File.Exists(filePath))
    {
        throw new FileNotFoundException($"Configuration file not found: {filePath}");
    }

    var json = await File.ReadAllTextAsync(filePath);
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true // Allows flexibility with JSON property casing
    };

    return JsonSerializer.Deserialize<object>(json, options);
}

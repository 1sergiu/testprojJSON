using TestProjJSON.Data;
using TestProjJSON.Data.Services;
using JsonFlatFileDataStore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddScoped<ClassifierService>();
builder.Services.AddScoped<EntityService>();

// Add the JsonFlatFileDataStore service
string filePath = builder.Configuration.GetSection("DataInitialization:FilePath").Value;
builder.Services.AddSingleton<IDataStore, DataStore>(serviceProvider => new DataStore(filePath));


// Configure Swagger/OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{	
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


var dataInitialization = new DataInitialization(app.Services, filePath);
dataInitialization.InitializeData();

app.Run();
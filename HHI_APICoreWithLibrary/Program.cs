
using HandHeldInventory_LibraryCore.Db;
using HandHeldInventory_LibraryCore.DbSprocs;

var builder = WebApplication.CreateBuilder(args);
var MyAllowedSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddSingleton(new ConnectionStringData
{
    SqlConnectionName = "Default"
});
builder.Services.AddSingleton<IDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IHHInventory_LoadListsSP, HHInventory_LoadListsSP>();
builder.Services.AddSingleton<IHHInventory_GetCylinderSP, HHInventory_GetCylinderSP>();
builder.Services.AddSingleton<IHHInventory_UpdatePhysicalInventoryTblSP, HHInventory_UpdatePhysicalInventoryTblSP>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowedSpecificOrigins
        , builder =>
        {
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(MyAllowedSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen();



builder.Services
    .AddDbContext<Persistence.DatabaseContext>
    (options => options.UseSqlServer(connectionString:
    builder.Configuration.GetConnectionString(name: nameof(Persistence.DatabaseContext))));
// **************************************************



builder.Services.AddScoped
    <Domain.Aggregates.CategoryProducts.ICategoryProductRepository,
    Persistence.CategoryProducts.CategoryProductQueryRepository>();


// **************************************************

builder.Services.AddTransient
    (serviceType: typeof(Dtat.Logging.ILogger<>),
    implementationType: typeof(Dtat.Logging.NLogAdapter<>));

// **************************************************
//TODO
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(assembly: typeof(Program).Assembly));


// **************************************************
//builder.Services.AddTransient<Persistence.IUnitOfWork, Persistence.UnitOfWork>();
// **************************************************



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

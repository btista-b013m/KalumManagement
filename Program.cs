<<<<<<< HEAD
using KalumManagement;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => {
    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(5);
});
// Add services to the container.
var startUp = new Startup(builder.Configuration);
startUp.ConfigureServices(builder.Services);
var app = builder.Build();

startUp.Configure(app, app.Environment);
app.Run();

/*
builder.Services.AddControllers();
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
*/
=======
using kalumAutManagement;

    public class Program
    {
        public static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
>>>>>>> 8c782d89149ff6cca6be42a8f6889018b2794a80

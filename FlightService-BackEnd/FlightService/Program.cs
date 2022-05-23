using Microsoft.Extensions.Configuration;

namespace FlightServiceEF
{
    public class Program
    {
        static AppSettings appSettings = new AppSettings();
        //static readonly MyFlightContext context = new MyFlightContext();
        public static void Main(string[] args)
        {
            //var flights = ContextBoundObject.Flights.ToList();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            //var context = new FlightServiceContext(appSettings.ConnectionString);
            var flightServiceFactory = new FlightServiceContextFactory(appSettings.ConnectionString);
            var blankParams = new string[] { };
            var flightServiceContext = flightServiceFactory.CreateDbContext(blankParams);
            CreateDbIfNotExists(flightServiceContext);
        }

        private static void CreateDbIfNotExists(FlightServiceContext ctx)
        {
            try
            {
                //ctx.Database.EnsureDeleted();
                DBInitializer.Initialize(ctx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occurred: {ex.Message}");
            }

        }
    }
}


//Scaffold-DbContext 'Data Source=DESKTOP-EVNC5PC;Initial Catalog=FlightServiceDB; Integrated Security=True; Trusted_Connection=True' Microsoft.EntityFrameworkCore.SqlServer
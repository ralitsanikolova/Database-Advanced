using System;

namespace P03_FootballBetting.Data
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<FootballBettingContext>();

                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        private static object CreateWebHostBuilder(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
}

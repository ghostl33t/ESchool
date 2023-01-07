using EmailService;
using EmailService.Classes;
using EmailService.Interface;
using Microsoft.EntityFrameworkCore;

using server.EmailService.Configuration;
using server.ServerConnection;
using System;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        services.AddDbContext<DBMain>(options =>
        {
            var mainConnectionString = hostContext.Configuration.GetConnectionString("DBConnection");
            if (mainConnectionString != null)
            {
                mainConnectionString = mainConnectionString.Replace("_MAIN_", "_MAIN_" + DateTime.Now.Year.ToString());
                options.UseSqlServer(mainConnectionString);
            }
            else
            {
                Console.WriteLine("ERROR: Unable to connect to SQL server(Main)");
            }
        });


        services.Configure<MAILConfiguration>(hostContext.Configuration.GetSection("MailSettings"));
        services.AddHostedService<Worker>().AddSingleton<IGradesEmail, GradesEmail>();
    })
    .Build();

await host.RunAsync();

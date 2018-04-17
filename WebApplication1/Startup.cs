using System;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            BackgroundJob.Enqueue(() => Console.WriteLine("Getting Started with HangFire!"));

            //BackgroundJob.Schedule(() => Console.WriteLine("This background job would execute after a delay."), TimeSpan.FromMilliseconds(1000));

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("This job will execute once in every minute"), Cron.Daily);

            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello, "));
            BackgroundJob.ContinueWith(id, () => Console.WriteLine("world!"));

            app.UseHangfireDashboard(); 

            //this will display a login page when adding suffix /hangfire to the website url
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new MyAuthorizationFilter() }
            //});

            //BackgroundJob.Schedule(
            //    () => Console.WriteLine("Hello, world"),
            //    TimeSpan.FromDays(1));

            //multiple dashboards
            //var storage1 = new SqlServerStorage("Connection1");
            //var storage2 = new SqlServerStorage("Connection2");
            //app.UseHangfireDashboard("/hangfire1", new DashboardOptions(), storage1);
            //app.UseHangfireDashboard("/hangfire2", new DashboardOptions(), storage2);


            //BackgroundJob.Enqueue<EmailSender>(x => x.Send(13, "Hello!"));

            app.UseHangfireServer();


        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                // In case you need an OWIN context, use the next line, `OwinContext` class
                // is the part of the `Microsoft.Owin` package.
                var owinContext = new OwinContext(context.GetOwinEnvironment());

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                return owinContext.Authentication.User.Identity.IsAuthenticated;


                //for ASP.NET Core environments
                //var httpContext = context.GetHttpContext();
                //return httpContext.User.Identity.IsAuthenticated;
            }
        }


    }
}

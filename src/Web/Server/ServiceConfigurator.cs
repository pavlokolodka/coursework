using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;

namespace Web.Server
{
    public class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserService>();
            services.AddSingleton<ReviewService>();
            services.AddSingleton<PropertyService>();
            services.AddSingleton<BookingService>();
            services.AddSingleton<IDao<User>, UserJSONDao>();
            services.AddSingleton<IDao<Review>, ReviewJSONDao>();
            services.AddSingleton<IDao<Property>, PropertyJSONDao>();
            services.AddSingleton<IDao<Booking>, BookingJSONDao>();

            services.AddSingleton<AuthService>();       
                     
        }
    }
}

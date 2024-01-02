//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OriginalLanguage.Common.Settings;
//public static class CorsConfiguration
//{
//    public static IServiceCollection AddAppCors(this IServiceCollection services)
//    {
//        services.AddCors();
//        //services.AddCors(builder =>
//        //{
//        //    builder.AddDefaultPolicy(pol =>
//        //    {
//        //        pol.AllowAnyHeader();
//        //        pol.AllowAnyMethod();
//        //        pol.AllowAnyOrigin();
//        //    });
//        //});
//        return services;
//    }

//    public static void UseAppCors(this WebApplication app)
//    {
//        var mainSettings = app.Services.GetRequiredService<MainSettings>();
//        var origins = mainSettings.AllowedOrigins?
//            .Split(',', ';')
//            .Select(x => x.Trim())
//            .Where(x => !string.IsNullOrEmpty(x)).ToArray();

//        app.UseCors(pol =>
//        {
//            pol.AllowAnyHeader();
//            pol.AllowAnyMethod();
//            pol.AllowCredentials();
//            if (origins?.Length > 0)
//                pol.WithOrigins(origins);
//            else
//                pol.SetIsOriginAllowed(origin => true);

//            pol.WithExposedHeaders("Content-Disposition");
//        });
//    }
//}

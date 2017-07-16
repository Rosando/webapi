using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CountingKs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Food",
                routeTemplate: "api/nutrition/foods/{foodid}",
                defaults: new { controller = "Foods", foodid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FoodMeasures",
                routeTemplate: "api/nutrition/foods/{foodid}/measures/{measureid}",
                defaults: new { controller = "Measures", measureid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Diaries", 
                routeTemplate: "api/user/diares/{diaresid}",
                defaults: new { controller = "Diaries", diaresid = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "DiariesDetails",
                routeTemplate: "api/user/diares/{diaresid}/entries/{detailid}",
                defaults: new { controller = "Diaries", detailid = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
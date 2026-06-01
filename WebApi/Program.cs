// using Microsoft.AspNetCore.Components.Web;
// using Serilog;

using System.Text.Json;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File("logs/myApp.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// builder.Host.UseSerilog();

// builder.Services.AddEndpointsApiExplorer();  // ← Add this
// builder.Services.AddSwaggerGen();
// builder.Services.AddControllers();
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();
// builder.Services.AddHttpLogging((o) => { });

// builder.Services.AddSingleton<IMyService, MyService>();
// builder.Services.AddScoped<IMyService, MyService>();
// builder.Services.AddTransient<IMyService, MyService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
});
var app = builder.Build();

var samplePerson = new Person { UserName = "Alice", UserAge = 30 };

app.MapGet("/", () => "I am Root!");
app.MapGet("/manual-json", () =>
{
    var jsonString = JsonSerializer.Serialize(samplePerson);
    return TypedResults.Text(jsonString, "application/json");
});

app.MapGet("/custom-serializer", () =>
{
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var customJsonString = JsonSerializer.Serialize(samplePerson, options);

    return TypedResults.Text(customJsonString, "application/json");
});

app.MapGet("/json", () =>
{
    return TypedResults.Json(samplePerson);
});

app.MapGet("/auto", () =>
{
    return samplePerson;
});

app.MapGet("/xml", () =>
{
    var xmlSerializer = new XmlSerializer(typeof(Person));
    var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, samplePerson);
    var xmlOutput = stringWriter.ToString();
    return TypedResults.Text(xmlOutput, "application/xml");
});

// app.Use(async (context, next) =>
// {
//     try
//     {
//         await next();
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Global exception caught: {ex}");
//         context.Response.StatusCode = 500;
//         await context.Response.WriteAsync("An unexpected error occured. Please try again later.");
//     }
// });

// app.Use(async (context, next) =>
// {
//     var myService = context.RequestServices.GetRequiredService<IMyService>();
//     myService.LogCreation("First Middleware");
//     await next.Invoke();
// });
// app.Use(async (context, next) =>
// {
//     var myService = context.RequestServices.GetRequiredService<IMyService>();
//     myService.LogCreation("Second Middleware");
//     await next.Invoke();
// });

// app.MapGet("/", (IMyService myService) =>
// {
//     myService.LogCreation("Root");

//     return Results.Ok("Check the console for service creation logs");
// });

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.MapGet("/", () => "hello world");
// app.UseRouting();
// app.MapControllers();

app.Run();

public class Person
{
    required public string UserName { get; set; }
    required public int UserAge { get; set; }
}

// public interface IMyService
// {
//     void LogCreation(string message);
// }

// public class MyService : IMyService
// {
//     private readonly int _serviceId;

//     public MyService()
//     {
//         _serviceId = new Random().Next(100000, 999999);
//     }

//     public void LogCreation(string message)
//     {
//         Console.WriteLine($"{message} - Service ID: {_serviceId}");
//     }
// }

// app.UseHttpLogging();

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Logic before");
//     await next.Invoke();
//     Console.WriteLine("Logic after");
// });

// var blogs = new List<Blog>
// {
//     new Blog { Title = "My first Post", Body = "This is my first post"},
//     new Blog { Title = "My second Post", Body = "This is my second post"},
// };

// app.MapGet("/blogs", () =>
// {
//     return blogs;
// });

// app.MapGet("/blogs/{id}", (int id) =>
// {
//     if (id < 0 || id >= blogs.Count)
//     {
//         return Results.NotFound();
//     }
//     else
//     {
//         return Results.Ok(blogs[id]);
//     }
// });

// app.MapPost("/blogs", (Blog blog) =>
// {
//     blogs.Add(blog);
//     return Results.Created($"/blogs/{blogs.Count - 1}", blog);
// });

// app.MapDelete("/blogs/{id}", (int id) =>
// {
//     if (id < 0 || id >= blogs.Count)
//     {
//         return Results.NotFound();
//     }
//     else
//     {
//         var blog = blogs[id];
//         blogs.RemoveAt(id);
//         return Results.NoContent();
//     }
// });

// app.MapPut("/blogs/{id}", (int id, Blog blog) =>
// {
//     if (id < 0 || id >= blogs.Count)
//     {
//         return Results.NotFound();
//     }
//     else
//     {
//         blogs[id] = blog;
//         return Results.Ok(blog);
//     }
// });

// app.MapGet("/blogs/{id:int:min(0)}", (int id) =>
// {
//     return $"Product: ID {id}";
// });

// app.MapGet("/blogs/report/{year?}", (int? year = 2016) =>
// {
//     return $"Product: ID {year}";
// });

// app.MapGet("/files/{*filePath}", (string filePath) =>
// {
//     return filePath;
// });

// app.MapGet("/search", (string? q, int page = 1) =>
// {
//     return $"Searching for {q} on page {page}";
// });

// app.MapGet("/blogs/{category}/{blogId:int?}/{*extraPath}", (string category, int? blogId, string? extraPath, bool isLiked = true) =>
// { 
//     // function
// });

// public class Blog
// {
//     public required string Title { get; set; }    
//     public required string Body { get; set; }    
// }
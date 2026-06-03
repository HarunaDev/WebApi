using Microsoft.AspNetCore.OpenApi;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog { Title = "My first Post", Body = "This is my first post"},
    new Blog { Title = "My second Post", Body = "This is my second post"},
};

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello world");

app.MapGet("/blogs", () =>
{
    return blogs;
});

app.MapGet("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(blogs[id]);
    }
}).WithOpenApi(operation =>
{
    operation.Parameters[0].Description = "The ID of the blog to retreive.";
    operation.Summary = "Get single blog";
    operation.Description = "Returns a single blog";

    return operation;
});

app.MapPost("/blogs", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

app.MapDelete("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        var blog = blogs[id];
        blogs.RemoveAt(id);
        return Results.NoContent();
    }
});

app.MapPut("/blogs/{id}", (int id, Blog blog) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        blogs[id] = blog;
        return Results.Ok(blog);
    }
});

// app.MapGet("/blogs/{id:int:min(0)}", (int id) =>
// {
//     return $"Product: ID {id}";
// });

app.MapGet("/blogs/report/{year?}", (int? year = 2016) =>
{
    return $"Product: ID {year}";
});

app.MapGet("/files/{*filePath}", (string filePath) =>
{
    return filePath;
});

app.MapGet("/search", (string? q, int page = 1) =>
{
    return $"Searching for {q} on page {page}";
});

app.MapGet("/blogs/{category}/{blogId:int?}/{*extraPath}", (string category, int? blogId, string? extraPath, bool isLiked = true) =>
{ 
    // function
});

app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
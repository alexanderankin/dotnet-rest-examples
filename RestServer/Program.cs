// See https://aka.ms/new-console-template for more information

using Common;

Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseKestrel();
builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();

app.MapGet("/", () => "Hello From Server!");

#region apples

var apples = new List<Apple>();

// apples resources GET/POST + GET/PUT/DELETE
app.MapGet("/apples", () => apples);
app.MapPost("/apples", (Apple apple) =>
{
    apples.Add(apple);
    return Results.Ok(apple);
});

app.MapGet("/apples/{id}", (string id) =>
{
    var apple = apples.Find(a => a.Name == id);
    return apple == null ? Results.NotFound() : Results.Ok(apple);
});

app.MapPut("/apples/{id}", (string id, Apple apple) =>
{
    var found = apples.Find(a => a.Name == id);
    if (found == null)
        return Results.NotFound();

    found.Name = apple.Name;
    return Results.Ok(found);
});

app.MapDelete("/apples/{id}", (string id) =>
{
    var found = apples.Find(a => a.Name == id);
    if (found == null)
        return Results.NotFound();
    apples.Remove(found);
    return Results.Ok(found);
});

#endregion

app.Run();

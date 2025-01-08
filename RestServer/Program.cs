// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel();

var app = builder.Build();

app.MapGet("/", () => "Hello From Server!");

app.Run();

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());

var app = builder.Build();
await app.RunAsync();

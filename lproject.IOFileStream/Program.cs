using Carter;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCarter();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapCarter();

try
{
    await app.RunAsync();
}
finally
{
    //
}

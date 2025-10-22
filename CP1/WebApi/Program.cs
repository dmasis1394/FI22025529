using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc; // ← AGREGAR ESTA LINEA

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/", (HttpContext context) =>
{
    // Obtener el parámetro xml del header con valor por defecto false
    var xmlHeader = context.Request.Headers["xml"].FirstOrDefault();
    bool useXml = bool.TryParse(xmlHeader, out bool xmlValue) && xmlValue;

    if (useXml)
    {
        // Retornar en formato XML
        var serializer = new XmlSerializer(typeof(List<object>));
        using var writer = new StringWriter();
        serializer.Serialize(writer, list);
        return Results.Text(writer.ToString(), "application/xml");
    }
    else
    {
        // Retornar en formato JSON (por defecto)
        return Results.Ok(list);
    }
});

app.MapPut("/", ([FromForm] int quantity, [FromForm] string type) =>
{
    // Validaciones para PUT
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });
    }

    if (type != "int" && type != "float")
    {
        return Results.BadRequest(new { error = "'type' must be either 'int' or 'float'" });
    }

    var random = new Random();
    if (type == "int")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.Next());
        }
    }
    else if (type == "float")
    {
        for (; quantity > 0; quantity--)
        {
            list.Add(random.NextSingle());
        }
    }

    return Results.Ok(new { message = $"{quantity} elements added successfully" });
}).DisableAntiforgery();

app.MapDelete("/", ([FromForm] int quantity) =>
{
    // Validaciones para DELETE
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });
    }

    if (quantity > list.Count)
    {
        return Results.BadRequest(new { error = $"Cannot remove {quantity} elements. List only has {list.Count} elements" });
    }

    list.RemoveRange(0, quantity);
    return Results.Ok(new { message = $"{quantity} elements removed successfully" });
}).DisableAntiforgery();

app.MapPatch("/", () =>
{
    // Limpiar toda la lista
    list.Clear();
    return Results.Ok(new { message = "List cleared successfully" });
});

app.Run();
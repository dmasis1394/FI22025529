using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Helper methods
string[] SplitWords(string text) => text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
string JoinWords(string[] words) => string.Join(" ", words);

bool TryGetXmlFormat(HttpRequest request, out bool xml)
{
    xml = false;
    return request.Headers.TryGetValue("xml", out var xmlHeader) && 
           bool.TryParse(xmlHeader, out xml);
}

IResult CreateResponse(string original, string modified, bool xml)
{
    if (xml)
    {
        var result = new { ori = original, newText = modified };
        var serializer = new XmlSerializer(result.GetType());
        using var writer = new StringWriter();
        serializer.Serialize(writer, result);
        return Results.Content(writer.ToString(), "application/xml");
    }
    
    return Results.Ok(new { ori = original, newText = modified });
}

IResult CreateErrorResponse(string message, bool xml)
{
    if (xml)
    {
        var errorResponse = new { error = message };
        var serializer = new XmlSerializer(errorResponse.GetType());
        using var writer = new StringWriter();
        serializer.Serialize(writer, errorResponse);
        return Results.BadRequest(writer.ToString());
    }
    
    return Results.BadRequest(new { error = message });
}

// Endpoints

// GET / - Redirect to Swagger UI (OBLIGATORIO según enunciado)
app.MapGet("/", () => Results.Redirect("/swagger"));

// POST /include/{position}?value={word}
app.MapPost("/include/{position}", async (int position, string value, HttpRequest request) =>
{
    // Validations
    if (position < 0)
        return CreateErrorResponse("'position' must be 0 or higher", TryGetXmlFormat(request, out var xml1));
    
    if (string.IsNullOrWhiteSpace(value))
        return CreateErrorResponse("'value' cannot be empty", TryGetXmlFormat(request, out var xml2));
    
    if (!request.HasFormContentType)
        return CreateErrorResponse("'text' form parameter is required", TryGetXmlFormat(request, out var xml3));
    
    var form = await request.ReadFormAsync();
    var text = form["text"].ToString();
    
    if (string.IsNullOrWhiteSpace(text))
        return CreateErrorResponse("'text' cannot be empty", TryGetXmlFormat(request, out var xml4));
    
    // Process text
    var words = SplitWords(text);
    var wordsList = words.ToList();
    
    // If position is beyond word count, add to the end
    if (position >= wordsList.Count)
    {
        wordsList.Add(value);
    }
    else
    {
        wordsList.Insert(position, value);
    }
    
    var newText = JoinWords(wordsList.ToArray());
    return CreateResponse(text, newText, TryGetXmlFormat(request, out var xml));
})
.WithName("IncludeWord");

// PUT /replace/{length}?value={replacement}
app.MapPut("/replace/{length}", async (int length, string value, HttpRequest request) =>
{
    // Validations
    if (length <= 0)
        return CreateErrorResponse("'length' must be greater than 0", TryGetXmlFormat(request, out var xml1));
    
    if (string.IsNullOrWhiteSpace(value))
        return CreateErrorResponse("'value' cannot be empty", TryGetXmlFormat(request, out var xml2));
    
    if (!request.HasFormContentType)
        return CreateErrorResponse("'text' form parameter is required", TryGetXmlFormat(request, out var xml3));
    
    var form = await request.ReadFormAsync();
    var text = form["text"].ToString();
    
    if (string.IsNullOrWhiteSpace(text))
        return CreateErrorResponse("'text' cannot be empty", TryGetXmlFormat(request, out var xml4));
    
    // Process text
    var words = SplitWords(text);
    var newWords = words.Select(word => word.Length == length ? value : word).ToArray();
    var newText = JoinWords(newWords);
    
    return CreateResponse(text, newText, TryGetXmlFormat(request, out var xml));
})
.WithName("ReplaceWords");

// DELETE /erase/{length}
app.MapDelete("/erase/{length}", async (HttpRequest request, int length) =>
{
    // Validations
    if (length <= 0)
        return CreateErrorResponse("'length' must be greater than 0", TryGetXmlFormat(request, out var xml1));
    
    if (!request.HasFormContentType)
        return CreateErrorResponse("'text' form parameter is required", TryGetXmlFormat(request, out var xml2));
    
    var form = await request.ReadFormAsync();
    var text = form["text"].ToString();
    
    if (string.IsNullOrWhiteSpace(text))
        return CreateErrorResponse("'text' cannot be empty", TryGetXmlFormat(request, out var xml3));
    
    // Process text
    var words = SplitWords(text);
    var newWords = words.Where(word => word.Length != length).ToArray();
    var newText = JoinWords(newWords);
    
    return CreateResponse(text, newText, TryGetXmlFormat(request, out var xml));
})
.WithName("EraseWords");

app.Run();
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts => {
    // configure swagger page 
    var title = "Our Verioned API";
    var description = "This is a Web API that demonstrates versioning";
    var terms = new Uri("https://localhost:7253/terms"); // link to terms and serice for API
    var license = new OpenApiLicense()
    {
        Name = "This is my full license information or a link to it"
    };
    var contact = new OpenApiContact()
    {
        Name = "Marc Moxey Helpdesk",
        Email = "Help@gmail.com",
        Url = new Uri("https://www.IAmTimCorey.com")
    };

    opts.SwaggerDoc("v1", new OpenApiInfo
     {
        Version = "v1",
        Title = $"{title} v1",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact

    });

    opts.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact

    });
});
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new(1, 0);
    opts.ReportApiVersions = true;
}).AddApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV"; // incidate major minior and patch verison
    opts.SubstituteApiVersionInUrl = true; // tell swagger about differnt version
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

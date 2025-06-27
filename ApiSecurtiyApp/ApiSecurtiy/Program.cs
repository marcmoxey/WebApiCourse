using Microsoft.AspNetCore.Authorization;
using System.Text;
using ApiSecurtiy.Constants; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(opts =>
{
    // custom policy base on claims
    opts.AddPolicy(PolicyConstants.MustHaveEmployeeId, policy =>
    {
        policy.RequireClaim("employeeId");
    });


    opts.AddPolicy(PolicyConstants.MustBeTheOwner, policy =>
    {
        // can add mulitply things in a policy 
        // policy.RequireUserName("mmoxey");
        policy.RequireClaim("title", "Business Owner");
    });

    opts.AddPolicy(PolicyConstants.MustBeVeteranEmployee, policy =>
    {
        policy.RequireClaim("employeeId", "E001", "E002", "E003");
    });

    // if not other policy applied still need the user to be authenticated
    // lock down the app
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddAuthentication("Bearer")
    // allow user to send the token back
    .AddJwtBearer(opts =>
    {
       // validate token
        opts.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authentication:secretKey")))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

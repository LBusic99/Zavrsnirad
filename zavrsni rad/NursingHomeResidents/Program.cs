using Microsoft.EntityFrameworkCore;
using NursingHomeResidents.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(sgo => {
    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Nursing Home Residents API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "busic.luka1@gmail.com",
            Name = "Luka Busic"
        },
        Description = "Ovo je dokumentacija za Nursing Home Residents API",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Edukacijska licenca"
        }
    };
    sgo.SwaggerDoc("v1", o);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddCors(opcije => {
    opcije.AddPolicy("CorsPolicy",
        builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<NursingHomeContext>(o =>
    o.UseSqlServer(
        builder.Configuration.GetConnectionString(name: "NursingHomeContext")
    )
);

var app = builder.Build();

app.UseSwagger(opcije => {
    opcije.SerializeAsV2 = true;
});
app.UseSwaggerUI(opcije => {
    opcije.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
});
app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseDefaultFiles();
app.UseDeveloperExceptionPage();
app.MapFallbackToFile("index.html");
app.Run();

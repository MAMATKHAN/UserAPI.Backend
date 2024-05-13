using System.Reflection;
using UserAPI.Application;
using UserAPI.Application.Common.Mappings;
using UserAPI.Application.Interfaces;
using UserAPI.Persistence;
using UserAPI.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAutoMapper(config =>
{
	config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
	config.AddProfile(new AssemblyMappingProfile(typeof(IUserDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddControllers();
builder.Services.AddCors(option =>
{
	option.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyHeader();
		policy.SetIsOriginAllowed((host) => true);
		policy.AllowAnyMethod();
		policy.AllowAnyOrigin();
	});
});
builder.Services.AddSwaggerGen(config =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	config.IncludeXmlComments(xmlPath);
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	var context = serviceProvider.GetRequiredService<UserDbContext>();
	DbInitializer.Initialize(context);
}
app.UseSwagger();
app.UseSwaggerUI(config =>
{
	config.RoutePrefix = string.Empty;
	config.SwaggerEndpoint("swagger/v1/swagger.json", "Witcher API");
});
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();

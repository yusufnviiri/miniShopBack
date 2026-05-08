using Contracts;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Repository.context;
using saccoshop;
using saccoshop.ContextFactory;
using saccoshop.Extensions;
using Services;
using Services.Hubs;
using Services.Redis;
var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config.txt"));


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigurePasswordHash();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.Configure<ApiBehaviorOptions>(options =>{options.SuppressModelStateInvalidFilter = true;});
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSlugService();
builder.Services.AddMemoryCache();
builder.Services.AddProductSearch(builder.Configuration);
builder.Services.AddTradeSearch();
builder.Services.AddUserSearch();
builder.Services.AddUserGroupSearch();





//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.AddDistributedMemoryCache();
//}
//else
//{
//    builder.Services.ConfigureRedisService();

//}



builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration("JwtSettings")
    .Validate(s => !string.IsNullOrWhiteSpace(s.SecretKey),
        "JWT Secret Key is not configured.")
    .ValidateOnStart();

builder.Services.ConfigureJwt(builder.Configuration);



//builder.Services.ConfigureActiveUserContext();
builder.Services.ConfigureActiveUserProfileService();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("SmsSettings"));



if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient<ISmsSender, AfricasTalkingSmsSender>();

    //builder.Services.AddSingleton<ISmsSender, FakeSmsSender>();
}
else
{

    builder.Services.AddHttpClient<ISmsSender, AfricasTalkingSmsSender>();
}


builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
builder.Services.AddHostedService<ImageProcessingWorker>();
builder.Services.AddSingleton<IBackGroundTradeImageJobQueue, BackGroundTradeImageJobQueue>();
builder.Services.AddHostedService<TradeImageProcessingWorker>();
//builder.Services.AddScoped<RedisCacheService>();
builder.Services.AddSignalR();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor|ForwardedHeaders.XForwardedProto
});
app.UseCors("CorsPolicy");
app.MapHub<ProductHub>("/hubs/product");
app.MapHub<TradeHub>("/hubs/trade");

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    await context.Database.MigrateAsync();   // MUST COME FIRST
//    await CategoryRuntimeSeeder.SeedAsync(context);
//}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

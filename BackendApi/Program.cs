using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.BAL.StorageFile;
using Saritasa.BAL.User;
using Saritasa.Data.EF;
using Saritasa.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LocalDBContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase"))
            );
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<LocalDBContext>()
                .AddDefaultTokenProviders();

builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStorageFileService, StorageFileService>();

// Add Accessor for access current user
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.RoutePrefix = string.Empty;
        option.DocumentTitle = "Saritasa Swagger";
    }
    );
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

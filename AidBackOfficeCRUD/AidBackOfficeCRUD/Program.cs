using AidBackOfficeCRUD.Controllers;
using AidBackOfficeCRUD.Data;
using AidBackOfficeCRUD.Interfaces;
using AidBackOfficeCRUD.Models;
using AidBackOfficeCRUD.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AidBackOfficeCRUDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalString") ?? throw new InvalidOperationException("Connection string 'AidBackOfficeCRUDContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddHttpClient();

builder.Services.AddScoped<HomeController>();
builder.Services.AddIdentityCore<MyUser>(options => { });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/Home/Login");

builder.Services.AddIdentityCore<MyUser>(options => { });


var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

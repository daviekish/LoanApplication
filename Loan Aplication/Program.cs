
using DomainLayer;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<LoanApplicationDBContext>();
builder.Services.AddScoped<Loans>();
builder.Services.AddScoped<LoanScheduleViewModel>();
builder.Services.AddScoped<Banks>();

builder.Services.AddDbContext <LoanApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient <IUserService<Loans>, UserService<Loans>>();

builder.Services.AddTransient <IRepository<Loans>, Repository<Loans>>();

builder.Services.AddTransient <ILoanService, LoanService>();

builder.Services.AddTransient <IRepository<LoanScheduleViewModel>, Repository<LoanScheduleViewModel>>();

builder.Services.AddTransient <IBankDetailsService, BankDetailsService>();

builder.Services.AddTransient <IRepository<Banks>, Repository<Banks>>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using online_store;
using online_store.Repository;
using online_store.Repository.Interfaces;
using online_store.Seeder;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(connection));

// Add services to the container.

builder.Services.AddTransient<IDataService,DataService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IItemOrderRepository, ItemOrderRepository>();
builder.Services.AddScoped<ICadastroRepository, CadastroRepository>();


builder.Services.AddControllersWithViews()
    .AddJsonOptions(_ => _.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSession();//for cart[it saves items on the cart temporarily] so i can keep shopping
builder.Services.AddDistributedMemoryCache();//for cart[it saves items on the cart temporarily] so i can keep shopping
builder.Services.AddHttpContextAccessor();//for cart[it saves items on the cart temporarily] so i can keep shopping

var app = builder.Build();

//seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationContext>();
    var dataService = new DataService(context);
    dataService.SeederProducts();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();//for cart[it saves items on the cart temporarily] so i can keep shopping

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Carrossel}/{codigo?}");//,
    //constraints: new {codigo = "^(?!codigo$).*"});//omit the word codigo in the url

app.Run();

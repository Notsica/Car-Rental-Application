using Car_Rent.App;
using Car_Rent.Business.Classes;
using Car_Rent.Data.Classes;
using Car_Rent.Data.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IData, CollectionData>();
builder.Services.AddSingleton<BookingProcessor>();

await builder.Build().RunAsync();

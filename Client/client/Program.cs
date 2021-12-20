using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BDSA-Project.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BDSA-Project.ServerAPI"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://13d30085-420a-453d-83d8-50a87d6129d0/API.Access");
    options.ProviderOptions.LoginMode = "redirect";
});

builder.Services.AddOidcAuthentication(options =>
{
   builder.Configuration.Bind("Auth0", options.ProviderOptions);
   options.ProviderOptions.ResponseType = "code";
   options.ProviderOptions.DefaultScopes.Add("email");

   options.ProviderOptions.ClientId = "e076a6ac-1fa1-4b9a-b47f-6e9ff3a2f2ee";
   options.ProviderOptions.Authority = "https://localhost:5222/authentication/login-callback";
});

await builder.Build().RunAsync();

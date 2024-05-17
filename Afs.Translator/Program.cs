using Afs.Translator;
using Afs.Translator.Data;
using Afs.Translator.FunTranslations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITranslationClient>(_ =>
{
    bool isStubApi = bool.Parse(builder.Configuration["StubApi:IsActive"]);
    if (isStubApi)
    {
        return new TranslationClient(new HttpClientStub(() => Constants.ExampleSuccessResponseMessage));
    }
    return new TranslationClient(new HttpClientWrapper());
}
    );

builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<TranslatorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TranslatorDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TranslatorDbContext>();
    dbContext.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var supportedCultures = new[] { "en" };
var localisationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localisationOptions);

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Translator}/{action=Index}");

app.Run();

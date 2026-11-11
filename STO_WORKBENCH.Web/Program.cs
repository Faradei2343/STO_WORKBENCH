using STO_WORKBENCH.Web.Components;
using STO_WORKBENCH.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<WebSettingsStore>();
builder.Services.AddSingleton<DownloadRegistry>();
builder.Services.AddSingleton<BurnerStateService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

// Скачивание сгенерированных файлов (xlsx, pdf и т.д.)
var downloads = app.Services.GetRequiredService<DownloadRegistry>();
app.MapGet("/download/{id}", (string id) =>
{
    var entry = downloads.Get(id);
    if (entry is null)
        return Results.NotFound("Файл не найден или ссылка устарела.");

    return Results.File(entry.Bytes, entry.ContentType, entry.FileName);
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

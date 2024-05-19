using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using semestralni_Navratilova.Data;
using semestralni_Navratilova.Hubs;
using semestralni_Navratilova.LibraryBooks;
using semestralni_Navratilova.LibraryBorrowings;
using semestralni_Navratilova.LibraryUsers;
using semestralni_Navratilova.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorContextMenu();
builder.Services.AddBlazoredModal();
builder.Services.AddSignalR();
builder.Services.AddScoped<LibraryContext>();
builder.Services.AddScoped<LibraryUsers>();
builder.Services.AddScoped<LibraryBooks>();
builder.Services.AddScoped<LibraryBorrowings>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<UserHub>("/userhub");
app.MapHub<BookHub>("/bookhub");
app.MapHub<BorrowingHub>("/borrowinghub");
app.MapFallbackToPage("/_Host");

app.Run();

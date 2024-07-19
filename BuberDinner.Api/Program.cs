using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}


var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    //app.Map("/error", (HttpContext httpContext) =>
    //{
    //    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>().Error;

    //    return Results.Problem();
    //});
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
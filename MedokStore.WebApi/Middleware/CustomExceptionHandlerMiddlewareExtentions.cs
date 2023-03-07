namespace MedokStore.Identity.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UserCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
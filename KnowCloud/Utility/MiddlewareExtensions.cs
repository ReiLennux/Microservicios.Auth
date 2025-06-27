namespace KnowCloud.Utility
{
    public static class MiddlewareExtensions
    {
        public static void ConfigurationMiddleware(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStatusCodePagesWithRedirects("/Account/Denied");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}"

            );

            app.MapStaticAssets();

        }
    }
}

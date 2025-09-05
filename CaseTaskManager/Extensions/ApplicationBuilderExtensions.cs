namespace CaseTaskManager.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApplicationDefaults(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // UseRouting and MapControllers are handled by WebApplication under the hood,
            // but we can still be explicit if needed:
            app.MapControllers();
        }
    }
}

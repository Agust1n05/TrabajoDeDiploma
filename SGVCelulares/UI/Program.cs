using QuestPDF.Infrastructure;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // Montos y fechas siempre en formato argentino (coma decimal), sin depender de la configuración regional de Windows.
            var argentina = System.Globalization.CultureInfo.GetCultureInfo("es-AR");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = argentina;
            System.Globalization.CultureInfo.CurrentCulture = argentina;

            QuestPDF.Settings.License = LicenseType.Community;
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmLogin());
        }
    }
}
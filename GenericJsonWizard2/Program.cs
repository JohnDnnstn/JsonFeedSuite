using GenericJsonSuite.EtlaToolbelt.Forms;
using GenericJsonWizard.Forms;

namespace GenericJsonWizard
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
            ApplicationConfiguration.Initialize();

            ChosenData.LoadFeedList();

            WelcomeForm welcomeForm = new(ChosenData.WelcomeData);
            var btn = FormResult.BACK;
            while (btn == FormResult.BACK)
            {
                btn = (FormResult)welcomeForm.ShowDialog();
            }
        }
    }
}
using Bosch.VideoSDK;
using System;
using System.Windows.Forms;

namespace MicrosoftCognitiveServiceExample
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      try
      {
          Core core = (Core)new CoreClass();
          core.Startup();
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run((Form)new UserInterface());
          core.Shutdown(false);
      }
      catch(Exception ex)
      {
          // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
          // returns a folder that is usually allowed to write files to
          string filePath = System.IO.Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               "MyApplication_CrashInfo.txt");
          System.IO.File.WriteAllText(filePath, ex.ToString());
      }

    }
  }
}

// Decompiled with JetBrains decompiler
// Type: CSharpRuntimeCameo.Program
// Assembly: CSharpRuntimeCameo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EE9C72-FB3C-47D2-86EA-48DB053A5B3C
// Assembly location: C:\Users\user\Downloads\x86\x86\CSharpRuntimeCameo.exe

using Bosch.VideoSDK;
using System;
using System.Windows.Forms;

namespace CSharpRuntimeCameo
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Core core = (Core) new CoreClass();
      core.Startup();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new MainWindow());
      core.Shutdown(false);
    }
  }
}

using System.Management.Automation;
using Terminal.Gui;
using AnyPackage.Tui;

namespace AnyPackage.Commands
{
    [Cmdlet("Show", "AnyPackage")]
    public class ShowAnyPackageCommand : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            Application.Init();
            Application.QuitKey = Key.Esc;

            try
            {
                Application.Run<AnyPackageView>();
            }
            finally
            {
                Application.Shutdown();
            }
        }
    }
}

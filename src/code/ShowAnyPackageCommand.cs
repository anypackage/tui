// Copyright (c) Thomas Nieto - All Rights Reserved
// You may use, distribute and modify this code under the
// terms of the MIT license.

using System.Management.Automation;

using AnyPackage.Tui;

using Terminal.Gui;

namespace AnyPackage.Commands;

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

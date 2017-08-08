Go to "Start" >> "All Programs" >> "Microsoft Visual Studio 2013" >> "Visual Studio Tools" then click "Developer Command Prompt for VS2013".

Type the following command:

cd <physical location of your TestWindowService.exe file>

in my case it is :

cd D:\IsysTFS\iBand\iBandAPI\iBand\iBand\BillingWindowsService\bin\Debug



Type the following two commands in the "Developer Command Prompt for VS2013" to uninstall the TestWindowService.exe.

cd <physical location of your BillingWindowsService.exe file>
and press Enter. In my case it is:
cd D:\IsysTFS\iBand\iBandAPI\iBand\iBand\BillingWindowsService\bin\Debug
cd D:\IsysTFS\iBand\iBandAPI\iBand\iBand\BillingWindowsService\bin\Debug

INSTALL:
InstallUtil.exe BillingWindowsService.exe

UNINSTALL:
InstallUtil.exe /u BillingWindowsService.exe
And press enter.
After executing the preceding commands, the TestWindowService will be uninstalled from your computer.
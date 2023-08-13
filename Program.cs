using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

int timeOut = 100;
string pname = string.Empty;

void prtUsage()
{
    Console.WriteLine("Usage: Auto_Shutdown.exe process_name [-t=shutdown_delay_in_sec]\n");
    return;
}

if (args.Length <= 1 || args.Length > 2)
{
    prtUsage();
    return;
}
else
{
    try
    {
        foreach (var arg in args)
        {
            if (arg.Contains("="))
            {
                string[] spArg = arg.Split("=");
                if (spArg[0] == "t")
                {
                    timeOut = int.Parse(spArg[1]);
                }
            }
            else
                pname = arg;
        }
    }
    catch
    {
        prtUsage();
        return;
    }

    Process[] process = Process.GetProcessesByName(pname);

    if(process.Length < 1 )
    {
        Console.WriteLine("invaild process name\n");
        return;
    }

    Console.WriteLine("setup trigger event: shutdown in "+timeOut+"s after "+pname+" exited.\n");
    process[0].WaitForExit();

    Process.Start(@"shutdown.exe", "-s -t " + timeOut);
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Api {
  public class Program {
    public static void Main(string[] args) {
      //dotnet publish -c release -r win-x64 --output bin/dist/win
      // AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve1;
      //AssemblyLoadContext.Default.Resolving += Default_Resolving;
      BuildWebHost(args).Run();
    }

    // private static Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2) {
    //   return null;
    // }

    private static Assembly CurrentDomain_AssemblyResolve1(object sender, ResolveEventArgs args) {
      return Resolve(@"C:\ksofos\Development\Glx\Baseline\Sources\Output\Debug", args);
    }

    public static Assembly Resolve(string glxOutputDebugPath, ResolveEventArgs args) {
      string assemblyName;
      if (args.RequestingAssembly == null) {
        assemblyName = AssemblyLoadContext.GetAssemblyName(args.Name).Name + ".dll";
      }
      else {
        assemblyName = args.Name.Split(new[] { ',' })[0] + ".dll";
      }
      var asm = SearchInSubFolders(glxOutputDebugPath, assemblyName);
      return asm;
    }

    private static Assembly SearchInSubFolders(string dirPath, string assemblyName) {
      var dirInfo = new DirectoryInfo(dirPath);
      var fileExists = dirInfo.EnumerateFiles(assemblyName);
      if (fileExists.Count() > 0) {
        string fullPath = Path.Combine(dirPath, assemblyName);
        var xx = Assembly.LoadFrom(fullPath);
        return xx;
      }

      var directories = Directory.GetDirectories(dirPath);
      foreach (var dir in directories) {

        dirInfo = new DirectoryInfo(dir);
        fileExists = dirInfo.EnumerateFiles(assemblyName);
        if (fileExists.Count() > 0) {
          string fullPath = Path.Combine(dir, assemblyName);
          var xx = Assembly.LoadFrom(fullPath);
          return xx;
        }

        var asm = SearchInSubFolders(dirInfo.FullName, assemblyName);
        if (asm != null) {
          return asm;
        }
      }
      return null;
    }

    public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AutomationToolkit.Core.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace AutomationToolkit.Infrastructure.Compiler;

public class ExecutronCompiler : ICompiler
{
static Action<string> Write = Console.WriteLine;

public bool HasError { get; set; }

public string Result { get; set; }

public void Compile(string sourceCode)
{
  string codeToCompile = @"
            using System;
            namespace RoslynCompileSample
            {
                public class Writer
                {
                    public string Stringify()
                    {
                        #sourceCode#
                    }
                }
            }";
  codeToCompile = codeToCompile.Replace("#sourceCode#", sourceCode);
  Write("Parsing the code into the SyntaxTree");
  SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

  string assemblyName = Path.GetRandomFileName();
  MetadataReference[] references = new MetadataReference[]
  {
            MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location)
  };

  Write("Compiling ...");
  CSharpCompilation compilation = CSharpCompilation.Create(
      assemblyName,
      syntaxTrees: new[] { syntaxTree },
      references: references,
      options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

  using (var ms = new MemoryStream())
  {
    EmitResult result = compilation.Emit(ms);

    if (!result.Success)
    {
      HasError = true;

      Write("Compilation failed!");
      IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
          diagnostic.IsWarningAsError ||
          diagnostic.Severity == DiagnosticSeverity.Error);

      foreach (Diagnostic diagnostic in failures)
      {
        var error = $"\t{diagnostic.Id}, {diagnostic.GetMessage()}";
        Console.Error.WriteLine(error);
        Result += Environment.NewLine + error;
      }
    }
    else
    {
      Write("Compilation successful! Now instantiating and executing the code ...");
      ms.Seek(0, SeekOrigin.Begin);

      Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
      var type = assembly.GetType("RoslynCompileSample.Writer");
      var instance = assembly.CreateInstance("RoslynCompileSample.Writer");
      var meth = type.GetMember("Stringify").First() as MethodInfo;
      Result = Convert.ToString(meth.Invoke(instance, null));
    }

    //public void Compile(string sourceCode) {
    //    Assembly compiledAssembly = CompileSourceCodeDom(sourceCode);
    //    if (compiledAssembly == null) return;

    //    try {
    //        Type type = compiledAssembly.GetType("Executron");
    //        var instance = compiledAssembly.CreateInstance("Executron");
    //        MethodInfo methodInfo = type.GetMethod("Execute");
    //        var res = methodInfo.Invoke(instance, null);
    //        Result = Convert.ToString(res);
    //    }
    //    catch (Exception ex) {
    //        Result = ex.ToString();
    //        HasError = true;
    //    }
    //}

    //private Assembly CompileSourceCodeDom(string sourceCode) {
    //    CodeDomProvider csharpCodeProvider = new CSharpCodeProvider();
    //    var cp = new CompilerParameters();

    //    // Add system assemblies
    //    Assembly executingAssembly = Assembly.GetExecutingAssembly();
    //    cp.ReferencedAssemblies.Add(executingAssembly.Location);

    //    foreach (AssemblyName assemblyName in executingAssembly.GetReferencedAssemblies()) {
    //        cp.ReferencedAssemblies.Add(Assembly.Load(assemblyName).Location);
    //    }

    //    // Add external assemblies
    //    //IEnumerable<string> references = GetReferences();
    //    //foreach (string reference in references) {
    //    //    cp.ReferencedAssemblies.Add(reference);
    //    //}
    //    cp.ReferencedAssemblies.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlOM.dll"));

    //    cp.GenerateExecutable = false;
    //    cp.GenerateInMemory = true;

    //    CompilerResults cr = csharpCodeProvider.CompileAssemblyFromSource(cp, sourceCode);

    //    if (cr.Errors.HasErrors) {
    //        Result = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(p => p.ToString()));
    //        HasError = true;
    //        return null;
    //    }

    //    return cr.CompiledAssembly;
    //}

    //private IEnumerable<string> GetReferences() {
    //    List<string> assemblies = new List<string>();

    //    string assembliesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assemblies");
    //    Directory.CreateDirectory(assembliesPath);
    //    var files = Directory.GetFiles(assembliesPath, "*.dll");
    //    assemblies.AddRange(files);

    //    return assemblies;
    //}
  }

}
}

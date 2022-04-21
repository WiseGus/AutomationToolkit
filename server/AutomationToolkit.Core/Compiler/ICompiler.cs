namespace Core.Compiler
{
    public interface ICompiler
    {
        bool HasError { get; set; }
        string Result { get; set; }

        void Compile(string sourceCode);
    }
}

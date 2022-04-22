using System.Threading.Tasks;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Services;

public interface IGenerateProjectsService
{
Task Generate(Preset value);
}

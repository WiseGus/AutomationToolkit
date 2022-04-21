using Core.Model;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
  public interface IGenerateProjectsService
  {
    Task Generate(Preset value);
  }
}

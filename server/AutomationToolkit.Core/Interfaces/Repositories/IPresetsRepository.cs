using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
  public interface IPresetsRepository
  {
    Task<IEnumerable<Preset>> Get();
    Task Save(IEnumerable<Preset> data);
  }
}

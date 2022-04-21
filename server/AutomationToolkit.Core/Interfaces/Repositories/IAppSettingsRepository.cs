using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
  public interface IAppSettingsRepository
  {
    Task<IEnumerable<Keyword>> Get();
    Task Save(IEnumerable<Keyword> data);
  }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Repositories;

public interface IAppSettingsRepository
{
Task<IEnumerable<Keyword>> Get();
Task Save(IEnumerable<Keyword> data);
}

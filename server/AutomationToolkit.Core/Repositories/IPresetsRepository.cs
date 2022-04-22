using System.Collections.Generic;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Repositories;

public interface IPresetsRepository
{
    Task<IEnumerable<Preset>> Get();
    Task Save(IEnumerable<Preset> data);
}

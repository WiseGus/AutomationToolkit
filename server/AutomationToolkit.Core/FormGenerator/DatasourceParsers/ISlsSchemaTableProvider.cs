using System;
using System.Collections.Generic;
using System.Text;

namespace Core.FormGenerator.DatasourceParsers
{
  public interface ISlsSchemaTableProvider
  {
    object GetSchemaTable();
  }
}

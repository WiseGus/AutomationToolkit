using System;

namespace Api.Tests
{
  class DummyModel
  {
    public Guid ID { get; set; }
    public bool Check { get; set; }
    public int Number { get; set; }
    public string Text { get; set; } = "";
  }
}

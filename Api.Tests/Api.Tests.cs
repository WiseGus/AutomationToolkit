using Api.Util.FormGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Api.Tests
{
  [TestClass]
  public class FormGeneratorTests
  {
    [TestMethod]
    public void POCOParserPropsCountFromType()
    {
      var parser = new POCOParser(typeof(DummyModel));
      var res = parser.Parse();
      Assert.AreEqual(4, res.Count());
    }

    [TestMethod]
    public void POCOParserPropsCountFromAssemblyPath()
    {
      var asmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Api.Tests.dll");
      var parser = new POCOParser(asmPath, "Api.Tests.DummyModel");
      var res = parser.Parse();
      Assert.AreEqual(4, res.Count());
    }

    [TestMethod]
    public void POCOParserProps()
    {
      var obj = new DummyModel();
      var parser = new POCOParser(obj.GetType());
      var res = parser.Parse();

      Assert.AreEqual(nameof(obj.ID), res.ElementAt(0).Name);
      Assert.AreEqual(obj.ID.GetType().Name, res.ElementAt(0).DataType);

      Assert.AreEqual(nameof(obj.Check), res.ElementAt(1).Name);
      Assert.AreEqual(obj.Check.GetType().Name, res.ElementAt(1).DataType);

      Assert.AreEqual(nameof(obj.Number), res.ElementAt(2).Name);
      Assert.AreEqual(obj.Number.GetType().Name, res.ElementAt(2).DataType);

      Assert.AreEqual(nameof(obj.Text), res.ElementAt(3).Name);
      Assert.AreEqual(obj.Text.GetType().Name, res.ElementAt(3).DataType);
    }
  }
}

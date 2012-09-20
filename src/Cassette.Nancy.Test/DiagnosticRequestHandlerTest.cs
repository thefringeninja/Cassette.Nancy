using System;
using CsQuery;
using Nancy.Testing;
using Xunit;

namespace Cassette.Nancy.Test
{
  public class DiagnosticRequestHandlerTest
  {
    [Fact]
    public void DiagnosticPageIsReturned()
    {
      var browser = new Browser(new NonOptimizingBootstrapper());
      var response = browser.Get("/_cassette", with => with.HttpRequest());
      Console.Write(response.Body.AsString());

      var document = CQ.Create(response.Body.AsString());
      var title = document.Find("head title").First();

      Assert.NotNull(title);

    }
  }
}
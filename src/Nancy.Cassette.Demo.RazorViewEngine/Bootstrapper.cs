﻿using Utility.Logging.NLog;

namespace Nancy.Cassette.Demo.RazorViewEngine
{
  public class Bootstrapper : DefaultNancyBootstrapper
  {
    public Bootstrapper()
    {
      //CassetteStartup.ShouldOptimizeOutput = true;
      CassetteStartup.Logger = new NLogLoggerFactory().GetCurrentClassLogger();
    }
  }
}
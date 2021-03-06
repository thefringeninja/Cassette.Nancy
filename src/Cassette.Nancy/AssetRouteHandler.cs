﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Nancy;
using Nancy.Responses;

namespace Cassette.Nancy
{
  internal class AssetRouteHandler : CassetteRouteHandlerBase
  {
    public AssetRouteHandler(IBundleContainer bundleContainer, string handlerRoot)
      : base(bundleContainer, handlerRoot)
    {
    }

    public override Response ProcessRequest(NancyContext context)
    {
      if (!context.Request.Url.Path.StartsWith(HandlerRoot, StringComparison.InvariantCultureIgnoreCase))
      {
        return null;
      }

      var path = Regex.Match(string.Concat("~", context.Request.Url.Path.Remove(0, HandlerRoot.Length)), @"^[^\?]*").Value;
      
      var bundles = BundleContainer.FindBundlesContainingPath(path).ToList();
      if (bundles == null)
      {
        //if (Logger != null)
        //  Logger.Error("AssetRouteHandler.ProcessRequest : Bundle not found for path '{0}'", context.Request.Url.Path);
        return null;
      }

      IAsset asset;
      Bundle bundle;
      if (!bundles.TryGetAssetByPath(path, out asset, out bundle))
      {
        //if (Logger != null)
        //  Logger.Error("AssetRouteHandler.ProcessRequest : Asset not found for path '{0}'", context.Request.Url.Path);
        return null;
      }

      var response = new StreamResponse(asset.OpenStream, bundle.ContentType);
      //if (Logger != null)
      //  Logger.Trace("AssetRouteHandler.ProcessRequest : Returned response for '{0}'", context.Request.Url.Path);
      return response;
    }
  }
}
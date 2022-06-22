using Microsoft.Extensions.Primitives;

using Yarp.ReverseProxy.Configuration;

namespace Tempus.Proxy;

public class YarpConfig : IProxyConfigProvider
{
  private readonly ILogger<YarpConfig> _logger;
  private readonly IConfiguration _config;
  private readonly IProxyConfig _proxyConfig;

  public YarpConfig(ILogger<YarpConfig> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
    _proxyConfig = Configure();
  }

  readonly string[] ServiceNames = new[]
  {
    "workers",
    "customers",
    "worktypes",
    "timebills"
  };

  private RouteConfig[] Routes()
  {
    var routes = new List<RouteConfig>();
    foreach(var name in ServiceNames)
    {
      routes.Add(MakeRoute(name));
    }
    return routes.ToArray();
  }

  private ClusterConfig[] Clusters()
  {
    var clusters = new List<ClusterConfig>();
    foreach (var name in ServiceNames)
    {
      clusters.Add(MakeCluster(name));
    }
    return clusters.ToArray();
  }

  private IProxyConfig Configure()
  {
    return new InMemoryProxyConfig(Routes(), Clusters());
  }

  public IProxyConfig GetConfig()
  {
    return _proxyConfig;
  }


  private RouteConfig MakeRoute(string routeName)
  {
    return new RouteConfig()
    {
      RouteId = $"{routeName}Route",
      ClusterId = $"{routeName}Cluster",
      CorsPolicy = "tempus",
      Match = new RouteMatch()
      {
        Path = $"/api/{routeName}/{{**catch-all}}"
      }
    };
  }

  private ClusterConfig MakeCluster(string clusterName)
  {
    return new ClusterConfig()
    {
      ClusterId = $"{clusterName}Cluster",
      Destinations = new Dictionary<string, DestinationConfig>()
      {
        { $"cluster/{clusterName}",
          new DestinationConfig()
          {
            Address = _config.GetServiceUri($"tempus-{clusterName}")!.AbsoluteUri
          }
        }
      }
    };
  }

  private class InMemoryProxyConfig : IProxyConfig
  {
    private readonly IReadOnlyList<RouteConfig> _routes;
    private readonly IReadOnlyList<ClusterConfig> _clusters;
    private readonly CancellationChangeToken _changeToken;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    public InMemoryProxyConfig(IReadOnlyList<RouteConfig> routes,
      IReadOnlyList<ClusterConfig> clusters)
    {
      _routes = routes;
      _clusters = clusters;
      _changeToken = new CancellationChangeToken(_cts.Token);
    }

    public IReadOnlyList<RouteConfig> Routes => _routes;
    public IReadOnlyList<ClusterConfig> Clusters => _clusters;
    public IChangeToken ChangeToken => _changeToken;
  }
}

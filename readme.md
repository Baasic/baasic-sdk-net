# Baasic .NET SDK

Baasic .NET SDK provides core functionality for building web and mobile applications on [Baasic](http://www.baasic.com/). To get more information about Baasic REST API end-points please visit [Developer Center](http://dev.baasic.com/api/reference/home).

## Dependencies

Baasic .NET SDK library has the following dependencies:

* [Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/microsoft.aspnet.webapi.client/) version 5.2.2
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) version 6.0.8

__Note: Ninject is not hard dependency, it can be installed as separate NuGet__
* [Ninject](https://www.nuget.org/packages/Ninject/) version 3.2.2.0
* [Ninject.Extensions.Conventions](https://www.nuget.org/packages/ninject.extensions.conventions/) version 3.2.0.0

## Application Configuration

Baasic .NET SDK needs be initialized using _ClientConfiguration_ with the following code:

```csharp
/// <summary>
/// Dependency Injection Module containing Baasic Client bindings.
/// </summary>
public class AppClientConfiguration : ClientConfiguration
{
    #region Fields

    private const string apiKey = "<api-key>";

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AppClientConfiguration" /> class.
    /// </summary>
    /// <param name="tokenHandler">The token handler.</param>
    public AppClientConfiguration(ITokenHandler tokenHandler)
        : base(apiKey, tokenHandler)
    {
    }

    #endregion Constructors
}

...

/// <summary>
/// Dependency Injection Module containing Baasic Client bindings.
/// </summary>
public partial class DIModule : IDIModule
{
    #region Methods

    /// <summary>
    /// Load dependency injection bindings.
    /// </summary>
    /// <param name="dependencyResolver"></param>
    public virtual void Load(IDependencyResolver dependencyResolver)
    {
        #region Configuration
        
        dependencyResolver.Register<Baasic.Client.Configuration.IClientConfiguration, AppClientConfiguration>();

        #endregion Configuration
    }

    #endregion Methods
}
```

**Note:** _To obtain a Baasic Application Identifier please create your application on [Baasic Registration](https://dashboard.baasic.com/register/) page._

Baasic .NET SDK has a built-in rudimentary DI container, if you want to use more robust DI container like [Ninject](http://www.ninject.org/) please install [Baasic.Client.Ninject]
(https://www.nuget.org/packages/Baasic.Client.Ninject/).

If you want to use Baasic .NET SDK inside the ASP.NET Web application then you need to install [Baasic Client WebHost](https://www.nuget.org/packages/Baasic.Client.WebHost/) in order to automatically setup Baasic token handling.

## Issue reporting

Before you create a new issue, please make sure it hasn't already been reported. In case it already exists simply add a quick _"+1"_ or _"I have the same problem"_ to the existing issue thread.

## Other

* Help us write the documentation
* Looking for something else? Get in [touch](https://groups.google.com/forum/#!forum/baasic-baas)..
namespace Microshaoft
{
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using System.IO;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Batch;
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            var httpServer = new HttpServer(httpConfiguration);
            httpConfiguration
                .Routes
                .MapHttpBatchRoute
                    (
                        routeName: "syncBatch"
                        , routeTemplate: "api/batch"
                        , batchHandler: new DefaultHttpBatchHandler(httpServer)
                    );
            httpConfiguration
                .Routes
                .MapHttpBatchRoute
                    (
                        routeName: "asyncBatch"
                        , routeTemplate: "api/asyncbatch"
                        , batchHandler: new DefaultHttpBatchHandler(httpServer)
                                            {
                                                ExecutionOrder = BatchExecutionOrder.NonSequential
                                            }
                    );
            appBuilder
                    .UseWebApi(httpConfiguration);
            appBuilder
                    .UseFileServer
                        (
                            new FileServerOptions()
                            {
                                 FileSystem = new PhysicalFileSystem(@"..\..\")
                                , EnableDirectoryBrowsing = true
                            }
                        );
        }
    }
}

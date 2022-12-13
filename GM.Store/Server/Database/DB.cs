using GM.Store.Server.Config;
using Raven.Client.Documents;
using Raven.Embedded;

namespace GM.Store.Server.Database
{
    public class DB
    {
    }
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        static string clientId = "1425";
        private static IDocumentStore store = null;

        public readonly ServiceConfiguration _serviceConfig;
        public DocumentStoreHolder(ServiceConfiguration serviceConfig)
        {
            _serviceConfig = serviceConfig;
            clientId = _serviceConfig.AppId.ToString();
        }
        public IDocumentStore Store
        {
            get
            {
                if (store == null)
                    store = CreateStore();


                return store;
            }
        }

        private static IDocumentStore CreateStore()
        {
            var serverOptions = new ServerOptions()
            {
                DataDirectory = $"{Directory.GetCurrentDirectory()}/CitymapiaPOS",
                ServerUrl = "http://127.0.0.1:60959/",
            };
            EmbeddedServer.Instance.StartServer(serverOptions);

            //var instance = EmbeddedServer.Instance.GetDocumentStore(new DatabaseOptions($"GMStore-{clientId}")
            //{
            //    Conventions =
            //    {
            //        FindIdentityProperty = prop => prop.Name == "DocumentID"
            //}
            //});
            var instance = EmbeddedServer.Instance.GetDocumentStore($"GMStore-{clientId}");
          //  instance.Conventions.FindIdentityProperty = prop => prop.Name == "DocumentID";
            return instance;
            
        }
    }
}

using System;
using Nest;

namespace Nest_Test
{
    public static class ElasticClientFactory 
    {
        private static IElasticClient client = CreateInitialClient();

        private static IElasticClient CreateInitialClient()
        {
            var uri = new Uri("http://localhost:9200");
            var connectionSetting = new ConnectionSettings(uri)
                    .DefaultIndex("people");
            connectionSetting.EnableDebugMode();
            return new ElasticClient(connectionSetting);
        }

        public static IElasticClient CreateElasticClient()
        {
            return client; 
        }


    }


}

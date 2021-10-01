using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest_Test
{
    public static class SampleQueries
    {
        private static IElasticClient client = ElasticClientFactory.CreateElasticClient();

        public static ISearchResponse<T> BoolQuerySample1<T>(string index) where T : class
        {
            QueryContainer query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = "about",
                        Query = "Labore"
                    }

                }
            };

           return client.Search<T>(s => s
                .Index(index)
                .Query(q => query));
        }

        public static void BoolQuerySample2(string index)
        {
            var response = client.Search<Person>(s => s
                .Index(index)
                .Query(q => q
                    .Bool(b => b
                        .Must(must => must
                            .Match(match => match
                                .Field(p => p.About)
                                .Query("Labore"))))));
        }
    }
}

using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest_Test
{
    internal class Importer<T> where T : class
    {
        private readonly IElasticClient client;

        public Importer()
        {
            this.client = ElasticClientFactory.CreateElasticClient();
        }

        public void Import(IEnumerable<T> documents, string index)
        {
            var bulk = CreateBulk(documents, index);
            client.Bulk(bulk);
        }

        private BulkDescriptor CreateBulk(IEnumerable<T> documents, string index)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var document in documents)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(index)
                    .Document(document)
                );
            }
            return bulkDescriptor;
        }
    }
}

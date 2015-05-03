using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolrNet.Attributes;

namespace solr.net_test
{
    class Product
    {
        [SolrUniqueKey("id")]
        public long Id { get; set; }

        [SolrField("solrname")]
        public string Name { get; set; }

        [SolrField("address")]
        public string Address { get; set; }

        [SolrField("phone")]
        public long Phone { get; set; }

        [SolrField("class")]
        public IList<string> Class { get; set; }

        [SolrField("addtime")]
        public DateTime Addtime { get; set; }

        [SolrField("price")]
        public decimal Price { get; set; }

        [SolrField("description")]
        public string Description { get; set; }
    }
}

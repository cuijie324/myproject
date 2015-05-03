using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;
using SolrNet.Mapping.Validation;

namespace solr.net_test
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("starting");
            //Startup.Init<Product>("http://localhost:8080/solr/");
            Startup.Init<Product>("http://192.168.121.130:8090/solr/collection2");

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Product>>();

            //验证映射
            IList<ValidationResult> mismatches = solr.EnumerateValidationResults().ToList();
            var errors = mismatches.OfType<ValidationError>();
            var warnings = mismatches.OfType<ValidationWarning>();
            foreach (var error in errors)
                Console.WriteLine("Mapping error: " + error.Message);
            foreach (var warning in warnings)
                Console.WriteLine("Mapping warning: " + warning.Message);
            if (errors.Any())
                throw new Exception("Mapping errors, aborting");


            //var results = solr.Query(SolrQuery.All);

            //Console.WriteLine(results.NumFound);

            //results = solr.Query(new SolrQueryByField("solrname", "cuiji*"));
            //Console.WriteLine(results.NumFound);

            //first doc
            Product product = new Product
            {
                Name = "cheng",
                Address = "shantou",
                Class = new[] {"cute", "smart"},
                Phone = 130,
                Addtime = DateTime.Now,
                Price = 100,
                Id = 101
            };

            solr.Add(product);

            //seconde doc
            product.Id = 102;
            product.Price = 90;
            product.Addtime = DateTime.Now.AddDays(-5);
            product.Description = "cheng";
            solr.Add(product);

            //third doc
            product.Id = 110;
            product.Name = "zhang";
            product.Price = 90;
            product.Address = "guangxi";
            product.Class = new[] {"talk"};
            product.Addtime = DateTime.Now.AddDays(-1);
            product.Description = "";
            solr.Add(product);

            //forth doc
            product.Id = 210;
            product.Name = "cui";
            product.Price = 70;
            product.Address = "guangzhou";
            product.Class = new[] {"none"};
            product.Addtime = DateTime.Now.AddDays(-2);
            product.Description = "chengge";
            product.Phone = 150;
            solr.Add(product);

            solr.BuildSpellCheckDictionary();
            solr.Commit();

            //solr.Commit();
            //results = solr.Query(SolrQuery.All);
            // Console.WriteLine(results.NumFound);

            //var result = solr.Query(new SolrQuery("id:210"));
            //var result = solr.Query(new SolrQueryByField("id","210"));
            //var result = solr.Query(new SolrQueryByRange<decimal>("price", 80, 100));
            //var result = solr.Query(new SolrQueryInList("id", "110", "210"));

            //var q = new SolrQuery("id:210") && new SolrQuery("class:none");

            //var q = new SolrQuery("id:110") + new SolrQuery("class:none");
            //var q = !new SolrQuery("id:110");
            //var result = solr.Query(q);

            var lessthan70 = new SolrQueryByRange<decimal>("price", 0, 70);
            var morethan70 = new SolrQueryByRange<string>("price", "71", "*");
            //var result = solr.Query(SolrQuery.All, new QueryOptions
            //{
            //    //指定返回字段
            //    Fields = new[] {"solrname", "price"},
            //    OrderBy = new[] {new SortOrder("price", Order.ASC)},
            //    Start = 0,
            //    Rows = 2,
            //    ExtraParams = new Dictionary<string, string> {{"timeAllowed", "100"}},
            //    //Facet搜索
            //    //Facet = new FacetParameters()
            //    //{
            //    //    Queries = new[] {new SolrFacetFieldQuery("solrname")}
            //    //}
            //    //Facet = new FacetParameters()
            //    //{
            //    //    Queries = new []
            //    //    {
            //    //        new SolrFacetDateQuery("addtime",DateTime.Now.AddDays(-10),DateTime.Now,"+2DAY")
            //    //        {
            //    //            HardEnd = true,
            //    //            Other = new[] {FacetDateOther.After, FacetDateOther.Before}
            //    //        }
            //    //    }
            //    //}
            //    //Facet = new FacetParameters()
            //    //{
            //    //    Queries = new []
            //    //    {
            //    //        new SolrFacetQuery(lessthan70),new SolrFacetQuery(morethan70)  
            //    //    }
            //    //}
            //});

            //var result = solr.Query(new SolrQueryByField("solrname","ipo"), new QueryOptions
            //{
            //    //Fields = new[] { "solrname", "price" },
            //    OrderBy = new[] { new SortOrder("price", Order.ASC) },
            //    Start = 0,
            //    Rows = 2,
            //    ExtraParams = new Dictionary<string, string> { { "timeAllowed", "100" } },

            //    //高亮
            //    Highlight = new HighlightingParameters()
            //    {
            //        Fields = new[] { "solrname" }
            //    },
            //    //more like this
            //    MoreLikeThis = new MoreLikeThisParameters(new []{"description","class"})
            //    {
            //        MinDocFreq = 1,
            //        MinTermFreq = 1
            //    },
            //    //拼写检查
            //    SpellCheck = new SpellCheckingParameters{Collate = true}
            //});

            var result = solr.Query(new SolrQueryByField("solrname", "cheng"), new QueryOptions
            {
                //Fields = new[] { "solrname", "price" },
                OrderBy = new[] {new SortOrder("price", Order.ASC)},
                Start = 0,
                Rows = 5,
                ExtraParams = new Dictionary<string, string> {{"timeAllowed", "100"}},
                Fields = new[] {"*", "score"},
                //统计
                //Stats = new StatsParameters()
                //{
                //    //Facets = new[] {"id"},
                //    FieldsWithFacets = new Dictionary<string, ICollection<string>>()
                //    {
                //        {"id", new List<string> {"price","solrname"}},
                //        {"price",new List<string>(){"solrname"}}
                //    }
                //},
                //分组
                //Grouping = new GroupingParameters()
                //{
                //    Fields = new[] { "solrname", "phone" }
                //}
            });

            //core admin
            const string solrUrl = "http://192.168.121.130:8090/solr";
            ISolrHeaderResponseParser headParser = ServiceLocator.Current.GetInstance<ISolrHeaderResponseParser>();
            ISolrStatusResponseParser statusPrParser = ServiceLocator.Current.GetInstance<ISolrStatusResponseParser>();
            ISolrCoreAdmin solrCoreAdmin = new SolrCoreAdmin(new SolrConnection(solrUrl), headParser, statusPrParser);

            IList<CoreResult> coreStatus = solrCoreAdmin.Status();

            foreach (var group in result.Grouping)
            {
                Console.WriteLine("Grouping count for document: '{0}': {1}", group.Key, group.Value.Groups.Count);
            }

            //统计结构
            foreach (var kv in result.Stats)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Field {0}: ", kv.Key);

                var s = kv.Value;
                Console.WriteLine("Min: {0}", s.Min);
                Console.WriteLine("Max: {0}", s.Max);
                Console.WriteLine("Sum of squares: {0}", s.SumOfSquares);
                foreach (var f in s.FacetResults)
                {
                    Console.WriteLine("Facet: {0}", f.Key);
                    foreach (var fv in f.Value)
                    {
                        Console.WriteLine("Facet value: {0}", fv.Key);
                        Console.WriteLine("Min: {0}", fv.Value.Min);
                        Console.WriteLine("Max: {0}", fv.Value.Max);
                        Console.WriteLine("Sum of squares: {0}", fv.Value.SumOfSquares);
                    }
                }
            }

            //foreach (var sc in result.SpellChecking)
            //{
            //    Console.WriteLine("Query: {0}", sc.Query);
            //    foreach (var s in sc.Suggestions)
            //    {
            //        Console.WriteLine("Suggestion: {0}", s);
            //    }
            //}

            //foreach (var item in result.SimilarResults)
            //{
            //    Console.WriteLine("Similar documents to {0}",item.Key);
            //    foreach (var similar in item.Value)
            //    {
            //        Console.WriteLine(similar.Id);
            //    }
            //}

            //foreach (var facet in result.FacetQueries)
            //{
            //    Console.WriteLine("{0}: {1}", facet.Key, facet.Value);

            //}

            //DateFacetingResult dateFacetResult = result.FacetDates["addtime"];
            //foreach (KeyValuePair<DateTime, int> dr in dateFacetResult.DateResults)
            //{
            //    Console.WriteLine(dr.Key);
            //    Console.WriteLine(dr.Value);
            //}

            //foreach (var item in result.FacetFields["solrname"])
            //{
            //    Console.WriteLine("{0}:{1}", item.Key, item.Value);
            //}

            Console.WriteLine(result.NumFound);
        }
    }
}

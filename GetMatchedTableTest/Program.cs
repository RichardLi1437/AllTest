using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetMatchedTableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchedTableTestData[] mtTestDatas = {
                new MatchedTableTestData { 
                    Namespaces = "Search",
                    Dimensions = "Market, Vertical",
                    Measures = "QueryViewCountWithClicks, QueryViewCountWithAnswerPresent",
                    Filters = "Market=en-US,zh-CN&IsBotVNext = False & IsNormalQuery = True & AppInfoServerName = www.bing.com & AppInfoClientName = Browser",
                    ExpectedMatchedCount = 5,
                    ExpectedMatchTableNames = "LiveMetricsCube,May1stOverallCube.DailyMay1stOverallQuerySegAndRevenue,LiveMetricsVisitFormCube,SMLSLAPI,May1stOverallCube.DailyMay1stOverallSearchPageData"
                },
                new MatchedTableTestData {
                    Namespaces = "Competitive",
                    Dimensions = "Engine, Vertical",
                    Measures = "QueryViewCount, QueryViewCountWithClicks",
                    Filters = "IsBot=False&IsTopLevelBrowsingEvent=True&Country=United States&DataSource=ie11,ie10",
                    ExpectedMatchedCount = 2,
                    ExpectedMatchTableNames = "CompetitiveCube,CompetitiveUnifiedView"
                }
            };

            foreach (var mtData in mtTestDatas)
            {
                var mtQuery = mtData.ToMtQuery();
                //IEnumerable<MatchedTableInfo> matchedTables = 
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string queryJSON = serializer.Serialize(mtQuery);
                string method = "POST";
                WebClient client = new WebClient();
                client.QueryString.Add("Namespaces", mtData.Namespaces);
                client.Headers.Set(HttpRequestHeader.ContentType, "application/json");
                client.UseDefaultCredentials = true;
                //string reply = client.UploadString("http://rich2:8080/api/BFTQuery/GetMatchedTables", method, queryJSON);
                string reply = client.UploadString("https://rover/api/BFTQuery/GetMatchedTables", method, queryJSON);
                IEnumerable<MatchedTableInfo> matchedTables = serializer.Deserialize<List<MatchedTableInfo>>(reply);
                matchedTables.OrderBy(t => t.Priority);
                foreach (MatchedTableInfo table in matchedTables)
                {
                    Console.WriteLine("{0}\t[{1}]", table.Name, table.Type);
                    Console.WriteLine(table.DataSourcePath);
                    Console.Write("Pre-filters:");
                    foreach (ColumnFilter f in table.ColumnFilters)
                    {
                        if (f.Value.Contains(","))
                            Console.Write("\t{0} is in [{1}]", f.ColumnName, f.Value);
                        else
                            Console.Write("\t{0} = {1}", f.ColumnName, f.Value);
                    }
                    Console.WriteLine("\n");
                }
                if (mtData.ExpectedMatchedCount >= 0 && mtData.ExpectedMatchedCount != matchedTables.Count())
                    throw new Exception("error");
                foreach (var t in matchedTables)
                {
                    if (!mtData.ExpectedMatchTableNames.Split(',').Contains(t.Name))
                        throw new Exception("error!");
                }
            }
        }
    }

    public class MatchedTableQuery
    {
        public IEnumerable<QueryColumn> Dimensions;
        public IEnumerable<QueryColumn> Measures;
        public IEnumerable<ColumnFilter> Filters;
    }

    public class ColumnFilter
    {
        public ColumnFilterType Type { get; set; }

        public string ColumnName { get; set; }

        public string Value { get; set; }
    }

    public class MatchedTableInfo
    {
        public int Id { get; private set; }

        public MatchedTableType Type { get; set; }

        public int Priority { get; set; }

        public string Name { get; set; }

        public string DataSourcePath { get; set; }

        public IEnumerable<ColumnFilter> ColumnFilters { get; set; }
    }

    public enum MatchedTableType
    {
        Cosmos = 0,
        Database = 1,
        Cube = 2
    }

    public enum ColumnFilterType
    {
        BooleanFilters,
        StringFilters,
        DateTimeFilters,
        DoubleFilters,
        Int32Filters,
        Int64Filters
    }

    public class QueryColumn
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class MatchedTableTestData
    {
        public string Namespaces;
        public string Dimensions;
        public string Measures;
        public string Filters;
        public string ExpectedMatchTableNames;
        public int ExpectedMatchedCount;

        public MatchedTableQuery ToMtQuery()
        {
            MatchedTableQuery query = new MatchedTableQuery();
            query.Dimensions = from d in Dimensions.Split(',') select new QueryColumn { Id = -1, Name = d.Trim() };
            query.Measures = from m in Measures.Split(',') select new QueryColumn { Id = -1, Name = m.Trim() };
            query.Filters = from f in Filters.Split('&') select CreateFilter(f);
            return query;
        }

        static private ColumnFilter CreateFilter(string f)
        {
            string[] fnv = f.Split('=');
            string name = fnv[0].Trim(), value = fnv[1].Trim();
            ColumnFilterType t = value == bool.TrueString || value == bool.FalseString ? ColumnFilterType.BooleanFilters : ColumnFilterType.StringFilters;
            return new ColumnFilter { ColumnName = name, Type = t, Value = value };
        }
    }
}

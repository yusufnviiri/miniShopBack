using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class UserGroupQueryBuilder
    {
        private const float BoostName = 5.0f;
        private const float BoostType = 3.0f;
        private const float BoostCompany = 2.0f;
        private const float BoostAbout = 1.0f;
        private const float BoostPrefix = 0.3f;

        public static (Query Query, Sort? Sort) Build(
            UserGroupSearchRequest req,
            Analyzer analyzer)
        {
            var bq = new BooleanQuery();
            var hasAnyClause = false;

            if (!string.IsNullOrWhiteSpace(req.Query))
            {
                var textQuery = BuildTextQuery(req.Query, analyzer);
                if (textQuery is not null)
                {
                    bq.Add(textQuery, Occur.MUST);
                    hasAnyClause = true;
                }
            }

            if (req.GroupTypeId is int gtId)
            {
                bq.Add(NumericRangeQuery.NewInt32Range(
                    UserGroupIndexFields.GroupTypeId, gtId, gtId, true, true), Occur.MUST);
                hasAnyClause = true;
            }

            if (!string.IsNullOrWhiteSpace(req.City))
            {
                bq.Add(new TermQuery(new Term(
                    UserGroupIndexFields.City, req.City.Trim().ToLowerInvariant())), Occur.MUST);
                hasAnyClause = true;
            }
            if (!string.IsNullOrWhiteSpace(req.Region))
            {
                bq.Add(new TermQuery(new Term(
                    UserGroupIndexFields.Region, req.Region.Trim().ToLowerInvariant())), Occur.MUST);
                hasAnyClause = true;
            }
            if (!string.IsNullOrWhiteSpace(req.Country))
            {
                bq.Add(new TermQuery(new Term(
                    UserGroupIndexFields.Country, req.Country.Trim().ToLowerInvariant())), Occur.MUST);
                hasAnyClause = true;
            }

            if (req.MinMembers is int min)
            {
                bq.Add(NumericRangeQuery.NewInt32Range(
                    UserGroupIndexFields.MemberCount, min, int.MaxValue, true, true), Occur.MUST);
                hasAnyClause = true;
            }

            Query finalQuery = hasAnyClause ? bq : new MatchAllDocsQuery();

            Sort? sort = req.Sort switch
            {
                UserGroupSortMode.Relevance => null,
                UserGroupSortMode.NewestFirst =>
                    new Sort(new SortField(UserGroupIndexFields.CreatedAtTicks, SortFieldType.INT64, true)),
                UserGroupSortMode.MostMembers =>
                    new Sort(new SortField(UserGroupIndexFields.MemberCount, SortFieldType.INT32, true)),
                _ => null,
            };

            return (finalQuery, sort);
        }

        private static Query? BuildTextQuery(string userInput, Analyzer analyzer)
        {
            var stemmedTokens = AnalyzeToTokens(analyzer, UserGroupIndexFields.UserGroupName, userInput);
            if (stemmedTokens.Count == 0) return null;

            var rawTokens = userInput
                .ToLowerInvariant()
                .Split(new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(t => t.Length > 0)
                .ToList();

            var outer = new BooleanQuery();

            for (var i = 0; i < stemmedTokens.Count; i++)
            {
                var stemmed = stemmedTokens[i];
                var raw = i < rawTokens.Count ? rawTokens[i] : null;

                var perToken = new BooleanQuery();

                AddBoosted(perToken, UserGroupIndexFields.UserGroupName, stemmed, BoostName);
                AddBoosted(perToken, UserGroupIndexFields.GroupType, stemmed, BoostType);
                AddBoosted(perToken, UserGroupIndexFields.Company, stemmed, BoostCompany);
                AddBoosted(perToken, UserGroupIndexFields.AboutGroup, stemmed, BoostAbout);

                if (!string.IsNullOrEmpty(raw))
                {
                    var prefix = new PrefixQuery(new Term(
                        UserGroupIndexFields.UserGroupNamePrefix, raw))
                    {
                        Boost = BoostPrefix
                    };
                    perToken.Add(prefix, Occur.SHOULD);
                }

                perToken.MinimumNumberShouldMatch = 1;
                outer.Add(perToken, Occur.MUST);
            }

            return outer;
        }

        private static void AddBoosted(BooleanQuery bq, string field, string token, float boost)
        {
            var tq = new TermQuery(new Term(field, token)) { Boost = boost };
            bq.Add(tq, Occur.SHOULD);
        }

        private static List<string> AnalyzeToTokens(Analyzer analyzer, string field, string text)
        {
            var tokens = new List<string>();
            using var reader = new System.IO.StringReader(text);
            using var stream = analyzer.GetTokenStream(field, reader);
            var termAttr = stream.AddAttribute<ICharTermAttribute>();

            stream.Reset();
            while (stream.IncrementToken())
            {
                var token = termAttr.ToString();
                if (!string.IsNullOrEmpty(token)) tokens.Add(token);
            }
            stream.End();

            return tokens;
        }
    }
}

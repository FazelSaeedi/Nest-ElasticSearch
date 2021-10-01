using Nest;
using Nest_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest_Test
{

    internal class IndexDefiner
    {
        private readonly IElasticClient client;

        public IndexDefiner()
        {
            client = ElasticClientFactory.CreateElasticClient();
        }

        public void CreateIndex(string index)
        {
            var response = client.Indices.Create(index, s => s
                .Settings(CreateSettings)
                .Map<Person>(CreateMapping));
        }

        private IPromise<IIndexSettings> CreateSettings(IndexSettingsDescriptor settingsDescriptor)
        {
            return settingsDescriptor
                .Setting("max_ngram_diff", 7)
                .Analysis(CreateAnalysis);
        }

        private ITypeMapping CreateMapping(TypeMappingDescriptor<Person> mappingDescriptor)
        {
            return mappingDescriptor
                .Properties(pr => pr.AddAboutFieldMapping());
        }

        private IAnalysis CreateAnalysis(AnalysisDescriptor analysisDescriptor)
        {
            return analysisDescriptor
                .TokenFilters(CreateTokenFilters)
                .Analyzers(CreateAnalyzers);
        }

        private static IPromise<IAnalyzers> CreateAnalyzers(AnalyzersDescriptor analyzersDescriptor)
        {
            return analyzersDescriptor
                .Custom(Analyzers.NgramAnalyzer, custom => custom
                    .Tokenizer("standard")
                    .Filters("lowercase", TokenFilters.NgramFilter));
        }

        private static IPromise<ITokenFilters> CreateTokenFilters(TokenFiltersDescriptor tokenFiltersDescriptor)
        {
            return tokenFiltersDescriptor
                .NGram(TokenFilters.NgramFilter, ng => ng
                    .MinGram(3)
                    .MaxGram(10));
        }
    }

    public static class Analyzers
    {
        public const string NgramAnalyzer = "my-ngram-analyzer";
    }

    public static class TokenFilters
    {
        public const string NgramFilter = "my-ngram-filter";
    }

    public static class FieldsDefiner
    {
        public static PropertiesDescriptor<Person> AddAboutFieldMapping(this PropertiesDescriptor<Person> prpertiesDescriptor)
        {
            return prpertiesDescriptor
                .Text(t => t
                    .Name(n => n.About)
                    .Fields(f => f
                        .Text(ng => ng
                            .Name("ngram")
                            .Analyzer(Analyzers.NgramAnalyzer))));
        }
    }
}

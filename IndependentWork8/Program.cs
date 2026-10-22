using System;

namespace IndependentWork8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрація TagManager ===");
            var tagManager = new TagManager();
            tagManager.AddTag("doc1", "csharp");
            tagManager.AddTag("doc1", "programming");
            tagManager.AddTag("doc2", "csharp");
            tagManager.AddTag("doc2", "dotnet");
            
            Console.WriteLine($"Спільні теги doc1 та doc2: {string.Join(", ", tagManager.FindCommonTags("doc1", "doc2"))}");

            Console.WriteLine("\n=== Демонстрація SearchIndex ===");
            var searchIndex = new SearchIndex();
            searchIndex.AddDocument(1, "Hello world of dotnet");
            searchIndex.AddDocument(2, "dotnet is a great framework");
            searchIndex.AddDocument(3, "Hello programming");

            var result = searchIndex.SearchMultiple("Hello", "dotnet");
            Console.WriteLine($"Документи зі словами 'Hello' та 'dotnet': {string.Join(", ", result)}");
        }
    }
}
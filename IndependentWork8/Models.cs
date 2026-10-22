using System;
using System.Collections.Generic;

namespace IndependentWork8
{
    // ==========================================
    // Сценарій 1: Кеш метаданих (Dictionary)[cite: 24]
    // ==========================================
    public class FileMetadata
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
        public string Hash { get; set; }
    }

    public class FileMetadataCache
    {
        private Dictionary<string, FileMetadata> _cache = new Dictionary<string, FileMetadata>();

        public void Add(string filePath, FileMetadata metadata) => _cache[filePath] = metadata;
        
        public FileMetadata Get(string filePath) => _cache.TryGetValue(filePath, out var meta) ? meta : null;
        
        public void Remove(string filePath) => _cache.Remove(filePath);
        
        public bool Contains(string filePath) => _cache.ContainsKey(filePath);
    }

    // ==========================================
    // Сценарій 2: Унікальні теги (HashSet)[cite: 24, 25]
    // ==========================================
    public class TagManager
    {
        private Dictionary<string, HashSet<string>> _articleTags = new Dictionary<string, HashSet<string>>();

        public void AddTag(string articleId, string tag)
        {
            if (!_articleTags.ContainsKey(articleId))
                _articleTags[articleId] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            _articleTags[articleId].Add(tag);
        }

        public void RemoveTag(string articleId, string tag)
        {
            if (_articleTags.ContainsKey(articleId))
                _articleTags[articleId].Remove(tag);
        }

        public HashSet<string> GetAllUniqueTags()
        {
            var allTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tags in _articleTags.Values)
            {
                allTags.UnionWith(tags); // Об'єднання множин[cite: 25]
            }
            return allTags;
        }

        public HashSet<string> FindCommonTags(string articleId1, string articleId2)
        {
            if (!_articleTags.ContainsKey(articleId1) || !_articleTags.ContainsKey(articleId2))
                return new HashSet<string>();

            var common = new HashSet<string>(_articleTags[articleId1], StringComparer.OrdinalIgnoreCase);
            common.IntersectWith(_articleTags[articleId2]); // Перетин множин[cite: 25]
            return common;
        }
    }

    // ==========================================
    // Сценарий 3: Індекс для пошуку (Dictionary + HashSet)[cite: 25]
    // ==========================================
    public class SearchIndex
    {
        // Слово -> Множина ID документів, де воно зустрічається[cite: 25]
        private Dictionary<string, HashSet<int>> _invertedIndex = new Dictionary<string, HashSet<int>>(StringComparer.OrdinalIgnoreCase);

        public void AddDocument(int docId, string content)
        {
            var words = content.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (!_invertedIndex.ContainsKey(word))
                    _invertedIndex[word] = new HashSet<int>();
                
                _invertedIndex[word].Add(docId);
            }
        }

        public HashSet<int> Search(string word)
        {
            return _invertedIndex.TryGetValue(word, out var docs) ? new HashSet<int>(docs) : new HashSet<int>();
        }

        public HashSet<int> SearchMultiple(params string[] words)
        {
            if (words.Length == 0) return new HashSet<int>();

            var result = Search(words[0]);
            for (int i = 1; i < words.Length; i++)
            {
                result.IntersectWith(Search(words[i])); // Знайти документи, що містять ВСІ слова[cite: 25]
            }
            return result;
        }
    }
}
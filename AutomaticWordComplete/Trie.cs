using System.Collections.Generic;

namespace AutomaticWordComplete
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord = false;
    }

    public class Trie
    {
        private readonly TrieNode root = new TrieNode();

        public void Insert(string word)
        {
            TrieNode current = root;
            foreach (char ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                    current.Children[ch] = new TrieNode();
                current = current.Children[ch];
            }
            current.IsEndOfWord = true;
        }

        private TrieNode SearchPrefix(string prefix)
        {
            TrieNode current = root;
            foreach (char ch in prefix)
            {
                if (!current.Children.ContainsKey(ch))
                    return null;
                current = current.Children[ch];
            }
            return current;
        }

        public bool FindWord(string word)
        {
            TrieNode node = SearchPrefix(word);
            return node != null && node.IsEndOfWord;
        }

        private void FindWords(TrieNode node, string currentWord, List<string> results)
        {
            if (node.IsEndOfWord)
                results.Add(currentWord);

            foreach (var child in node.Children)
                FindWords(child.Value, currentWord + child.Key, results);
        }

        public List<string> AutoComplete(string prefix)
        {
            TrieNode prefixNode = SearchPrefix(prefix);
            List<string> results = new List<string>();
            if (prefixNode != null)
                FindWords(prefixNode, prefix, results);
            return results;
        }
    }
}

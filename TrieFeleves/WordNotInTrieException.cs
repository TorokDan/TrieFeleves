using System;

namespace TrieFeleves
{
    public class WordNotInTrieException : Exception
    {
        public WordNotInTrieException(string word)
            : base($"\"{word}\" nincs benne a trie-ben")
        {
            
        }
    }
}
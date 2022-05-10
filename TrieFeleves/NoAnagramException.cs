using System;

namespace TrieFeleves
{
    public class NoAnagramException : Exception
    {
        public NoAnagramException(string key)
            : base($"\"{key}\" has no anagram in the list")
        {
            
        }
    }
}
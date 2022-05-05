using System;

namespace TrieFeleves
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie('$');
            
            trie.Insert(new string[] {"alma", "ház", "kalap", "cipő" });
            // trie.Insert("alma$korte$haz$kalap$cipo");
            
            Console.WriteLine(trie.Search("ház"));
            
            trie.Bejaras();
        }
    }
}
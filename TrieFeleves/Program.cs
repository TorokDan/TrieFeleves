using System;

namespace TrieFeleves
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie('$');
            
            trie.Insert(new string[] {"alma", "ház", "kalap", "cipő", "sikk", "egyed", "kis" });
            // trie.Insert("alma$korte$haz$kalap$cipo");
            
            // Console.WriteLine(trie.Search("ház"));
            string[] asd = trie.SearchAnagram("kiskegyed");
            ;

            trie.Bejaras();
        }
    }
}
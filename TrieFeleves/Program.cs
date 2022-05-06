using System;

namespace TrieFeleves
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie('$');
            
            trie.Insert(new string[] {"alma", "ház", "kalap", "cipő", "sikk", "egyed", "kis" });
            trie.Insert("Tea alma, cipő, sátor, kever élő, kis, egyed, perec, sikk, kegy, izom, fej, fejfa, alom, álom");
            
            // Console.WriteLine(trie.Search("ház"));
            // string[] asd = trie.SearchAnagram("kiskegyed");

            // trie.Bejaras();
            // trie.Delete("alma");
            // trie.Bejaras();
            trie.WriteAnagramms("kéz");
        }
    }
}
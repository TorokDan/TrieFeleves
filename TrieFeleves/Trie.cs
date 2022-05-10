using System;

namespace TrieFeleves
{
    public class Trie
    {
        private const int Size = 34;

        protected class TrieNode
        {
            private readonly TrieNode[] _children;
            private bool _isEndOfWord;

            public TrieNode[] Children => _children;
            public bool IsEndOfWord => _isEndOfWord;
            public TrieNode()
            {
                _isEndOfWord = false;
                _children = new TrieNode[Size];
                for (int i = 0; i < _children.Length; i++)
                    _children[i] = null;
            }

            public void End() => _isEndOfWord = true;
            public void DeleteEnd() => _isEndOfWord = false;

            public bool AllChildNull()
            {
                int index = 0;
                while (index < _children.Length && _children[index] == null)
                    index++;
                return !(index < _children.Length);
            }
        }

        private readonly TrieNode _root;
        private readonly char _endChar;

        public Trie(char endChar)
        {
            _endChar = endChar;
            _root = new TrieNode();
        }

        

        /// <summary>
        /// Tömb tartalmainak beillesztése
        /// </summary>
        /// <param name="keys"></param>
        public void Insert(string[] keys)
        {
            foreach (var t in keys)
                InsertAKey(t);
        }
        
        /// <summary>
        /// Új szó beillesztése
        /// </summary>
        /// <param name="key"></param>
        public void Insert(string key)
        {
            key = key.ToLower();
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] != ' ')
                {
                    int index = -1;

                    try
                    {
                        index = GetIndex(key[i]);
                    }
                    catch (NotDefinedCharacter e)
                    {
                        index = -1;
                    }

                    if (index == -1)
                        key = key.Remove(i, 1);
                }
            }
            Insert(key.Split(' '));
        }
        
        /// <summary>
        /// Beilleszt egy új szót.
        /// </summary>
        /// <param name="key"></param>
        private void InsertAKey(string key)
        {
            if (Search(key)) return;
            TrieNode tmp = _root;
            key = key.ToLower();
            for (int level = 0; level < key.Length; level++)
            {
                int index = GetIndex(key[level]);

                tmp.Children[index] ??= new TrieNode();

                tmp = tmp.Children[index];
            }
                
            tmp.End();

        }

        private static int GetIndex(char charToIndex)
        {
            return charToIndex switch
            {
                'a' => 0,
                'á' => 1,
                'b' => 2,
                'c' => 3,
                'd' => 4,
                'e' => 5,
                'é' => 6,
                'f' => 7,
                'g' => 8,
                'h' => 9,
                'i' => 10,
                'í' => 11,
                'j' => 12,
                'k' => 13,
                'l' => 14,
                'm' => 15,
                'n' => 16,
                'o' => 17,
                'ó' => 18,
                'ö' => 19,
                'ő' => 20,
                'p' => 21,
                'r' => 22,
                's' => 23,
                't' => 24,
                'u' => 25,
                'ú' => 26,
                'ü' => 27,
                'ű' => 28,
                'v' => 29,
                'w' => 30,
                'x' => 31,
                'y' => 32,
                'z' => 33,
                _ => throw new NotDefinedCharacter(charToIndex)
            };
        }

        private static char GetChar(int index)
        {
            return index switch
            {
                0 => 'a',
                1 => 'á',
                2 => 'b',
                3 => 'c',
                4 => 'd',
                5 => 'e',
                6 => 'é',
                7 => 'f',
                8 => 'g',
                9 => 'h',
                10 => 'i',
                11 => 'í',
                12 => 'j',
                13 => 'k',
                14 => 'l',
                15 => 'm',
                16 => 'n',
                17 => 'o',
                18 => 'ó',
                19 => 'ö',
                20 => 'ő',
                21 => 'p',
                22 => 'r',
                23 => 's',
                24 => 't',
                25 => 'u',
                26 => 'ú',
                27 => 'ü',
                28 => 'ű',
                29 => 'v',
                30 => 'w',
                31 => 'x',
                32 => 'y',
                33 => 'z',
                _ => throw new NotDefinedCharacter(index)
            };
        }

        /// <summary>
        /// Visszaadja, hogy a keresett szó benne van-e a trie-ben
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Search(string key)
        {
            key = key.ToLower();
            TrieNode tmp = _root;

            for (int level = 0; level < key.Length; level++)
            {
                int index = GetIndex(key[level]);

                if (tmp.Children[index] == null)
                    return false;
                tmp = tmp.Children[index];
            }

            return tmp.IsEndOfWord;
        }
        
        public delegate void BejaroHandler(string key);

        public void Bejaras(BejaroHandler method = null) => BejarasRek(_root, string.Empty, method ??= Console.WriteLine);

        private static void BejarasRek(TrieNode actualNode, string word, BejaroHandler method)
        {
            for (int index = 0; index < actualNode.Children.Length; index++)
            {
                if (actualNode.Children[index] != null)
                {
                    if (actualNode.Children[index].IsEndOfWord)
                        method?.Invoke(word+GetChar(index));
                    BejarasRek(actualNode.Children[index], word+GetChar(index), method);
                }
            }
        }

        public void WriteAnagramms(string key, BejaroHandler _method = null)
        {
            foreach (var anagram in SearchAnagram(key))
            {
                if (_method == null)
                    _method = Console.WriteLine;
                _method?.Invoke(anagram);
            }
        }
        /// <summary>
        /// Megkeresi a megadott szó anagrammáit.
        /// </summary>
        /// <param name="key"></param>
        public string[] SearchAnagram(string key)
        {
            Lista<string> lista = new Lista<string>();
            SearchAnagramRek(_root, key.ToLower(), string.Empty, lista);
            return lista.Length != 0 ? lista.MakeItArray() : throw new NoAnagramException(key);
        }

        private static void SearchAnagramRek(TrieNode actualNode, string key, string word, Lista<string> lista)
        {
            for (int index = 0; index < actualNode.Children.Length; index++)
            {
                if (actualNode.Children[index] != null)
                    if (key.Contains(GetChar(index)))
                    {
                        if (actualNode.Children[index].IsEndOfWord)
                            lista.Add(word + GetChar(index));
                        SearchAnagramRek(actualNode.Children[index], key.Remove(key.IndexOf(GetChar(index)), 1),
                            word + GetChar(index), lista);
                    }
            }
        }

        /// <summary>
        /// Törli a megadott szót a trie-ből.
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key) => DeleteRek(_root, key.ToLower(), 0);

        /// <summary>
        /// Akkor tér vissza igazzal, ha törölt egy elemet.
        /// </summary>
        /// <param name="actualNode"></param>
        /// <param name="key"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static bool DeleteRek(TrieNode actualNode, string key, int level)
        {
            int index = GetIndex(key[level]);
            TrieNode node = actualNode.Children[index];
            bool deleted = false;
            if (actualNode.Children[index] == null) throw new WordNotInTrieException(key);
            if (level != key.Length - 1)
                deleted = DeleteRek(node, key, ++level);
            // végigment a rekurzió, és a szó benne is van a trieben
            if ((level != key.Length - 1 || !node.IsEndOfWord) && !deleted) return false;
            node.DeleteEnd();
            // nincs alatta gyerek
            if (!node.AllChildNull()) return false;
            actualNode.Children[index] = null;
            return true;
        }

        public void ChangeWord(string keyFrom, string keyTo)
        {
            Delete(keyFrom.ToLower());
            Insert(keyTo.ToLower());
        }
    }
}
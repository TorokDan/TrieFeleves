using System;

namespace TrieFeleves
{
    public class Trie
    {
        static readonly int _size = 34; // azért static mert a TriNode csak a statichoz fér hozzá...
        protected class TrieNode
        {
            // IDE azt ajánlja h nagybetűvel kezdődjön.
            private TrieNode[] _children;
            private bool _isEndOfWord;

            public TrieNode[] Children => _children;
            public bool IsEndOfWord => _isEndOfWord;
            public TrieNode()
            {
                _isEndOfWord = false;
                _children = new TrieNode[_size];
                for (int i = 0; i < _children.Length; i++)
                {
                    _children[i] = null;
                }
            }

            public void End() => _isEndOfWord = true;
        }

        private TrieNode _root;
        private char _endChar;

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
            foreach (string key in keys)
                this.InsertAKey(key);
        }
        
        /// <summary>
        /// Új szó beillesztése
        /// </summary>
        /// <param name="key"></param>
        public void Insert(string key)
        {
            this.Insert(key.Split(_endChar));
        }
        
        /// <summary>
        /// Beilleszt egy új szót.
        /// </summary>
        /// <param name="key"></param>
        private void InsertAKey(string key)
        {
            key = key.ToLower();
            TrieNode tmp = _root;

            for (int level = 0; level < key.Length; level++)
            {
                int index = GetIndex(key[level]);

                tmp.Children[index] ??= new TrieNode();

                tmp = tmp.Children[index];
            }
            
            tmp.End();
        }

        private int GetIndex(char charToIndex)
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
                _ => throw new Exception()
            };
        }

        private char GetChar(int index)
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
                _ => throw new Exception()
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

        private void BejarasRek(TrieNode actualNode, string word, BejaroHandler method)
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

        /// <summary>
        /// Keres egy szót az eddig felvett szavak közül, ami anagrammája a megadott szónak.
        /// </summary>
        /// <param name="key"></param>
        public string[] SearchAnagram(string key)
        {
            Lista<string> lista = new Lista<string>();
            SearchAnagramRek(_root, key, string.Empty, lista);
            return lista.MakeItArray();
        }

        private void SearchAnagramRek(TrieNode actualNode, string key, string word, Lista<string> lista)
        {
            for (int index = 0; index < actualNode.Children.Length; index++)
            {
                if (actualNode.Children[index] != null)
                {
                    if (key.Contains(GetChar(index)))
                    {
                        if (actualNode.Children[index].IsEndOfWord)
                            lista.Add(word+GetChar(index));
                        SearchAnagramRek(actualNode.Children[index], key.Remove(key.IndexOf(GetChar(index)),1), word+GetChar(index), lista);
                    }
                }
            }
        }

        private bool CanBeAnagram(char charToCheck, ref string key)
        {
            if (!key.Contains(charToCheck))
                return false;
            key = key.Remove(key.IndexOf(charToCheck), 1);
            return true;
        }
    }
}
using System;

namespace TrieFeleves
{
    public class Trie
    {
        static readonly int _size = 26; // azért static mert a TriNode csak a statichoz fér hozzá...
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
                int index = key[level] - 'a';

                tmp.Children[index] ??= new TrieNode();

                tmp = tmp.Children[index];
            }
            
            tmp.End();
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
                int index = key[level] - 'a';

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
            for (var index = 0; index < actualNode.Children.Length; index++)
            {
                if (actualNode.Children[index] != null)
                {
                    if (actualNode.Children[index].IsEndOfWord)
                        method?.Invoke(word+(char)(index+'a'));
                    BejarasRek(actualNode.Children[index], word+((char)(index+'a')), method);
                }
            }
        }
    }
}
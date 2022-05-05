namespace TrieFeleves
{
    public class Lista<T>
    {
        protected class Node
        {
            private T _value;
            private Node _next;

            public T Value => _value;

            public Node Next
            {
                get => _next;
                set => _next = value;
            }

            public Node(T value)
            {
                _value = value;
            }
        }

        private Node _head;
        private int _count;

        public void Add(T value)
        {
            if (_head == null)
                _head = new Node(value);
            else
            {
                Node act = _head;
                Node prev = _head;

                while (act != null)
                {
                    prev = act;
                    act = act.Next;
                }

                prev.Next = new Node(value);
            }

            _count++;
        }

        public T[] MakeItArray()
        {
            T[] answer = new T[_count];
            int counter = 0;

            Node node = _head;

            while (node != null)
            {
                answer[counter++] = node.Value;
                node = node.Next;
            }
            return answer;
        }
        
    }
}
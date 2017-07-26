using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        public Node()
        {
            Item = default(T);
            Next = null;
        }

        public Node(List<T> items)
        {
            Item = items[0];
            items.RemoveAt(0);
            Next = items.Count > 0 ? new Node<T>(items) : null;
        }

        public Node<T> Clone()
        {
            Node<T> head = new Node<T>()
            {
                Item = this.Item,
                Next = this.Next != null ? this.Next.Clone() : null
            };

            return head;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Item.ToString());

            if(this.Next != null)
            {
                sb.Append(",");
                sb.Append(this.Next.ToString());
            }

            return sb.ToString();
        }
    }
}

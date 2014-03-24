using System;
using System.Collections.Generic;

namespace ModernApp4Me.WP8.SnSWidgets.SnSWidgetLoopingSelector.SnSWidgetLoopingSelectorCore
{
    public sealed class SnSListLoopingDataSource<T> : SnSLoopingDataSourceBase
    {
        private LinkedList<T> _linkedList;
        private List<LinkedListNode<T>> _sortedList;
        private IComparer<T> _comparer;
        private NodeComparer _nodeComparer;

        public IEnumerable<T> Items
        {
            get
            {
                return _linkedList;
            }
            set
            {
                SetItemCollection(value);
            }
        }
 
        private void SetItemCollection(IEnumerable<T> collection)
        {
            _linkedList = new LinkedList<T>(collection);
 
            _sortedList = new List<LinkedListNode<T>>(_linkedList.Count);
            // initialize the linked list with items from the collections
            LinkedListNode<T> currentNode = _linkedList.First;
            while (currentNode != null)
            {
                _sortedList.Add(currentNode);
                currentNode = currentNode.Next;
            }
 
            var comparer = _comparer;
            if (comparer == null)
            {
                // if no comparer is set use the default one if available
                if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                {
                    comparer = Comparer<T>.Default;
                }
                else
                {
                    throw new InvalidOperationException("There is no default comparer for this type of item. You must set one.");
                }
            }
 
            _nodeComparer = new NodeComparer(comparer);
            _sortedList.Sort(_nodeComparer);
        }
 
        public IComparer<T> Comparer
        {
            get
            {
                return _comparer;
            }
            set
            {
                _comparer = value;
            }
        }
 
        public override object GetNext(object relativeTo)
        {
            // find the index of the node using binary search in the sorted list
            int index = _sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), _nodeComparer);
            if (index < 0)
            {
                return default(T);
            }
 
            // get the actual node from the linked list using the index
            LinkedListNode<T> node = _sortedList[index].Next ?? _linkedList.First;

            return node.Value;   
        }
 
        public override object GetPrevious(object relativeTo)
        {
            int index = _sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), _nodeComparer);
            if (index < 0)
            {
                return default(T);
            }

            LinkedListNode<T> node = _sortedList[index].Previous ?? _linkedList.Last;

            return node.Value;
        }
 
        private class NodeComparer : IComparer<LinkedListNode<T>>
        {
            private readonly IComparer<T> _iComparer;
 
            public NodeComparer(IComparer<T> comparer)
            {
                _iComparer = comparer;
            }
 
            public int Compare(LinkedListNode<T> x, LinkedListNode<T> y)
            {
                return _iComparer.Compare(x.Value, y.Value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Infrastructure
{
    public class LimitedStack<T>
    {
        private int _capacity;
        private List<T> _list;

        public LimitedStack(int capactity)
        {
            _capacity = capactity;
            _list = new List<T>(capactity);
        }

        public void Push(T item)
        {
            if (HasItems() && _list.Count == _capacity)
                _list.RemoveAt(0);

            _list.Add(item);
        }

        public T Pop()
        {
            if (!HasItems())
                throw new InvalidOperationException("Stack is empty");

            var itemToReturn = _list.Last();

            _list.Remove(itemToReturn);

            return itemToReturn;
        }

        public bool HasItems()
            => _list.Count > 0;
    }
}
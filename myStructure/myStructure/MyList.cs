using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace myStructure
{
    public class MyList<T>
    {
        T[] items;
        public int Count { get; set; }
        int currentIndex;
        const int capacity = 4;
        IEqualityComparer<T> comparer;
        public MyList()
        {
            items = new T[capacity];
            Count = 0;
            currentIndex = 0;
            comparer = EqualityComparer<T>.Default;
        }
        public MyList(IEqualityComparer<T> comparer)
        {
            items = new T[capacity];
            Count = 0;
            currentIndex = 0;
            this.comparer = comparer ?? EqualityComparer<T>.Default;
        }
        public MyList(int capacity)
        {
            items = new T[capacity];
            Count = 0;
            currentIndex = 0;
            comparer = comparer;
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > currentIndex)
                    throw new IndexOutOfRangeException();
                return items[index];
            }
            set
            {
                if (index < 0 || index >= currentIndex)
                    throw new IndexOutOfRangeException();
                items[index] = value;
            }
        }
        public List<T> this[string indexs]
        {
            get
            {
                if (string.IsNullOrEmpty(indexs))
                    throw new IndexOutOfRangeException();
                string[] indexsArray = indexs.Split(',');
                List<T> list = new List<T>();
                foreach (string index in indexsArray)
                {
                    if (Convert.ToInt32(index) < 0 || Convert.ToInt32(index) > currentIndex)
                        continue;
                    if (items[Convert.ToInt32(index)] == null)
                        continue;
                    list.Add(items[Convert.ToInt32(index)]);
                }
                return list;
            }
        }
        public void Add(T item)
        {
            if (currentIndex >= items.Length)
                Resize(items.Length * 2);
            items[currentIndex] = item;
            currentIndex++;
            Count++;
        }
        public void AddRenge(T[] itemsArray)
        {
            if (items.Length < Count + itemsArray.Length)
                Resize(Count + itemsArray.Length);
            for (int i = 0; i < itemsArray.Length; i++)
            {
                items[currentIndex] = itemsArray[i];
                currentIndex++;
                Count++;
            }
        }
        void Resize(int newSize)
        {
            T[] newItems = new T[newSize];
            Array.Copy(items, newItems, Count);
            items = newItems;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > currentIndex)
                throw new IndexOutOfRangeException();
            shiftLeft(index);
            currentIndex--;
            Count--;
        }
        public void RemoveRenge(int index, int count)
        {
            if (index < 0 || index > index || count < 0 || index + count > items.Length)
                throw new IndexOutOfRangeException();
            shiftLeft(index, count);
            if (items.Length / 2 > Count)
                Resize(items.Length / 2);
        }
        public void Insert(int index, T item)
        {
            if(index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            if (Count == items.Length)
                Resize(items.Length * 2);
             ShiftReght(index,1);
            items[index] = item;
            Count++;
            currentIndex++;
        }
        public void InsertRange(int index ,  T[] userItems)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            if (items.Length < Count + userItems.Length)
                Resize(items.Length * 2);
        }
        void ShiftReght(int index , int itemsOfIndex)
        {
            
            for (int i = items.Length - 1; i > index; i--)
            {
                items[i] = items[i - itemsOfIndex];
            }
            for(int i = index; i < index +itemsOfIndex; i++)
            {
                items[i] = default(T);
                Count++;
                currentIndex++;
            }
        }
        void shiftLeft(int index, int count)
        {

            for (int i = count + index; i < items.Length; i++)
            {

                items[index] = items[i];


                index++;
            }
            for (int i = items.Length - 1; i >= items.Length - count; i--)
            {
                items[i] = default(T);
                currentIndex--;
                Count--;
            }

        }
        void shiftLeft(int index)
        {
            for (int i = index; i < items.Length - 1; i++)
            {
                items[i] = items[i + 1];
            }
            if (items[items.Length - 1] != null)
                items[items.Length - 1] = default(T);
        }

        //public int IndexOf(T item)
        //{
        //    return items.IndexOf(item); 
        //}
        public int IndexOf(T item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (comparer.Equals(items[i], item))
                    return i;

            }
            return -1;
        }
        public void Remove(T item)
        {
            var index = IndexOf(item);
            shiftLeft(index);
            currentIndex--;
            Count--;
        }
    }
}

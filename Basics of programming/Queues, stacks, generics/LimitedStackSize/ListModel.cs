using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        private LimitedSizeStack<ICommand<TItem>> Commands { get; }

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Commands = new LimitedSizeStack<ICommand<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            var action = new Adding<TItem>(item, Items.Count - 1);
            action.Execute(Items);
            Commands.Push(action);
        }

        public void RemoveItem(int index)
        {
            var action = new Removing<TItem>(Items[index], index);
            Commands.Push(action);
            action.Execute(Items);
        }

        public bool CanUndo()
        {
            return Commands.Count != 0;
        }

        public void Undo()
        {
            Commands.Pop().Undo(Items);
        }
    }

    public interface ICommand<TItem>
    {
        void Execute(List<TItem> items);
        void Undo(List<TItem> items);
    }

    public class Adding<TItem> : ICommand<TItem>
    {
        public TItem Item { get; }
        public int Index { get; }

        public Adding(TItem item, int index)
        {
            Item = item;
            Index = index;
        }

        public void Execute(List<TItem> items)
        {
            items.Add(Item);
        }

        public void Undo(List<TItem> items)
        {
            items.RemoveAt(Index + 1);
        }
    }

    public class Removing<TItem> : ICommand<TItem>
    {
        public TItem Item { get; }
        public int Index { get; }

        public Removing(TItem item, int index)
        {
            Item = item;
            Index = index;
        }

        public void Execute(List<TItem> items)
        {
            items.RemoveAt(Index);
        }

        public void Undo(List<TItem> items)
        {
            items.Insert(Index, Item);
        }
    }
}
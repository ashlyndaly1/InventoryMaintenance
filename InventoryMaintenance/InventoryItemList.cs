namespace InventoryMaintenance
{
    public class InventoryItemList
    {
        private List<InventoryItem> items;

        public delegate void ChangeHandler(InventoryItemList itemList);

        public event ChangeHandler? Changed;

        public InventoryItemList()
        {
            items = new List<InventoryItem>();
        }

        public int Count => items.Count;

        public InventoryItem this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index), index,
                        $"Index {index} is not valid.");
                }

                return items[index];
            }
            set
            {
                if (index < 0 || index >= items.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index), index,
                        $"Index {index} is not valid.");
                }

                items[index] = value;
                Changed?.Invoke(this);
            }
        }

        public void Add(InventoryItem item)
        {
            items.Add(item);
            Changed?.Invoke(this);
        }

        public void Add(int itemNo, string description, decimal price)
        {
            InventoryItem item = new(itemNo, description, price);
            Add(item);
        }

        public void Remove(InventoryItem item)
        {
            if (items.Remove(item))
            {
                Changed?.Invoke(this);
            }
        }

        public static InventoryItemList operator +(
            InventoryItemList itemList, InventoryItem item)
        {
            itemList.Add(item);
            return itemList;
        }

        public static InventoryItemList operator -(
            InventoryItemList itemList, InventoryItem item)
        {
            itemList.Remove(item);
            return itemList;
        }

        public void Fill()
        {
            items = InventoryDB.GetItems();
            Changed?.Invoke(this);
        }

        public void Save() => InventoryDB.SaveItems(items);
    }
}
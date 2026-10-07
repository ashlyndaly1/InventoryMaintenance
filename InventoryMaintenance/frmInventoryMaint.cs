namespace InventoryMaintenance
{
    public partial class frmInventoryMaint : Form
    {
        public frmInventoryMaint()
        {
            InitializeComponent();
        }

        private InventoryItemList items = new();

        private void frmInventoryMaint_Load(object sender, EventArgs e)
        {
            items.Fill();
            FillItemListBox();
            items.Changed += Items_Changed;
        }

        private void Items_Changed(InventoryItemList itemList)
        {
            itemList.Save();
            FillItemListBox();
        }

        private void FillItemListBox()
        {
            lstItems.Items.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                InventoryItem item = items[i];
                lstItems.Items.Add(item.GetDisplayText());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewItem newItemForm = new();
            InventoryItem? item = newItemForm.GetNewItem();

            if (item != null)
            {
                items += item;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = lstItems.SelectedIndex;

            if (i == -1)
            {
                MessageBox.Show(
                    "Please select an item to delete.",
                    "No item selected");
            }
            else
            {
                InventoryItem item = items[i];

                string message =
                    $"Are you sure you want to delete {item.Description}?";

                DialogResult result = MessageBox.Show(
                    message,
                    "Confirm Delete",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    items -= item;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
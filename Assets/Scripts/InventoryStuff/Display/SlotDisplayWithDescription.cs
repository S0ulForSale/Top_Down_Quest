using TMPro;
using UnityEngine;

namespace InventoryStuff.Display
{
    public class SlotDisplayWithDescription : SlotDisplay
    {
        [SerializeField] private TMP_Text description;

        protected override void EmptySlot()
        {
            base.EmptySlot();
            description.enabled = false;
        }

        protected override void UpdateItem(ItemTemplate item)
        {
            base.UpdateItem(item);
            if (item == null) return;
            description.enabled = true;
            description.text = item.itemDescription;
        }
    }
}
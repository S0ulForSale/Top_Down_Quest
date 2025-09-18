using UnityEngine;
using UnityEngine.Events;

namespace InventoryStuff.Model
{
    public class Slot
    {
        public readonly UnityEvent OnInteract = new();
        public readonly UnityEvent<bool> OnEquip = new();
        public readonly UnityEvent<ItemTemplate> OnChange = new();
        public ItemTemplate item { get; private set; }
        public bool equipped { get; private set; }
        public bool empty => item == null;

        public void Update(ItemTemplate item)
        {
            if (this.item == item) return;
            this.item = item;
            OnChange.Invoke(item);
        }

        public void Equip(bool eq)
        {
            if (eq == equipped) return;
            if (eq && (empty || !item.equipable))
            {
                Debug.LogWarning("Item is unequippable");
                return;
            }

            equipped = eq;
            OnEquip.Invoke(eq);
        }


        public void Interact()
        {
            OnInteract.Invoke();
        }
    }
}
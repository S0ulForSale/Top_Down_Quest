using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace InventoryStuff.Model
{
    public class Inventory : MonoSingleton<Inventory>
    {
        private List<Slot> slots = new();
        public IEnumerable<Slot> slotEnum => slots;
        public readonly UnityEvent<ItemTemplate> OnEquip = new();
        public readonly UnityEvent<ItemTemplate> OnAdd = new();
        public readonly UnityEvent<ItemTemplate> OnRemove = new();
        public readonly UnityEvent<Slot> OnSelect = new();
        public Slot selected { get; private set; }
        [SerializeField] private int slotNum = 8;

        public override void Init()
        {
            for (int i = 0; i < slotNum; i++)
            {
                slots.Add(new Slot());
            }

            foreach (var slot in slots)
            {
                var capture = slot;
                slot.OnInteract.AddListener(() =>
                {
                    if (selected == capture)
                    {
                        UnequipExcept(capture);
                        return;
                    }

                    OnSelect.Invoke(capture);
                    selected = capture;
                });
            }
        }

        private void UnequipExcept(Slot slot)
        {
            foreach (var slot1 in slots)
            {
                slot1.Equip(slot == slot1);
            }

            OnEquip.Invoke(slot.item);
        }

        public void AddItem(ItemTemplate item)
        {
            if (!item.takeable)
            {
                OnAdd.Invoke(item);
                return;
            }

            int firstEmpty = slots.FindIndex((s) => s.empty);
            if (firstEmpty == -1 || firstEmpty >= slots.Count)
                throw new Exception("No empty slots Left");
            slots[firstEmpty].Update(item);
            OnAdd.Invoke(item);
        }

        public void RemoveItem(ItemTemplate item)
        {
            Slot firstEmpty = slots.FirstOrDefault((slot) => slot.item == item);
            if (firstEmpty == null) throw new Exception("Inventory has no such item");
            firstEmpty.Update(null);
            OnRemove.Invoke(item);
        }

        public bool HasItem(ItemTemplate item)
        {
            return slots.Any((slot) => slot.item == item);
        }

//     public GameObject Invent;
//
// //відкриття інвентаря
//     public void Open()
//     {
//         PauseManager.instance.SetPause(true);
//         Invent.SetActive(true);
//     }
// // закриття інвентаря
//     public void Close()
//     {
//         PauseManager.instance.SetPause(false);
//         Invent.SetActive(false);
//     }
    }
}
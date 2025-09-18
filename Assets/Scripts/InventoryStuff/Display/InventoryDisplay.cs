using System;
using System.Collections.Generic;
using System.Linq;
using InventoryStuff.Model;
using Managers;
using UnityEngine;

namespace InventoryStuff.Display
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private List<SlotDisplay> slotDisplays;
        [SerializeField] private SlotDisplay selected;
        private Inventory _inventory;

        public bool open { get; private set; }

        private void Start()
        {
            UpdateInventory(Inventory.instance);
        }


        private void UpdateInventory(Inventory inventory)
        {
            int i = 0;
            foreach (var slot in inventory.slotEnum)
            {
                slotDisplays[i++].UpdateSlot(slot);
            }

            inventory.OnSelect.AddListener((slot) => selected.UpdateSlot(slot));
            selected.UpdateSlot(inventory.slotEnum.First());
        }

        private void SetOpen(bool openWindow)
        {
            open = openWindow;
            PauseManager.instance.SetPause(openWindow);
            gameObject.SetActive(openWindow);
        }

        public void Open()
        {
            SetOpen(true);
        }

        public void Close()
        {
            SetOpen(false);
        }
    }
}
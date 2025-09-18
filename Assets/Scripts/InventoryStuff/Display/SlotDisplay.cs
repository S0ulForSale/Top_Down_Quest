using System;
using InventoryStuff.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InventoryStuff.Display
{
    public class SlotDisplay : MonoBehaviour
    {
        [SerializeField] private Button equipButton;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private Image icon;
        [SerializeField] private Image equipped;
        private Slot slot;

        public void Start()
        {
            if(equipButton != null)
                equipButton.onClick.AddListener(() => slot?.Interact());
        }

        public void UpdateSlot(Slot newSlot)
        {
            if (slot != null)
            {
                slot.OnChange.RemoveListener(UpdateItem);
                slot.OnEquip.RemoveListener(UpdateEquipped);
            }

            slot = newSlot;
            slot.OnChange.AddListener(UpdateItem);
            slot.OnEquip.AddListener(UpdateEquipped);
            UpdateItem(slot.item);
            UpdateEquipped(slot.equipped);
        }

        protected virtual  void EmptySlot()
        {
            icon.enabled = false;
            itemName.enabled = false;
            equipped.enabled = false;
        }

        protected virtual void UpdateItem(ItemTemplate item)
        {
            if (item == null)
            {
                EmptySlot();
                return;
            }

            icon.sprite = item.itemSprite;
            itemName.text = item.itemName;
            icon.enabled = true;
            itemName.enabled = true;
            equipped.enabled = item.equipable;
        }

        private void UpdateEquipped(bool eq)
        {
            equipped.color = eq ? new Color(200, 200, 200, 255) : new Color(100, 210, 100, 255);
            //Debug.Log();
        }
    }
}
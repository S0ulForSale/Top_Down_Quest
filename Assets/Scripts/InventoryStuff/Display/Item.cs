using System;
using InventoryStuff.Model;
using UnityEngine;

namespace InventoryStuff.Display
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteR;
        [SerializeField] private ItemTemplate item;

        private void Start()
        {
            spriteR.sprite = item.itemSprite;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Inventory.instance.AddItem(item);
                Destroy(this.gameObject);
                Debug.Log("Take Item");
            }
        }
    }
}
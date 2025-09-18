using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventoryStuff
{
    [Serializable]
    public class ItemTemplate
    {
        public string id;
        public string itemName;
        [FormerlySerializedAs("itemIcon")] [FormerlySerializedAs("itemImage")] public Sprite itemSprite;
        public string itemDescription;
        public bool equipable => equipableID != "";
        public bool takeable = true;
        public string equipableID;
    }
}
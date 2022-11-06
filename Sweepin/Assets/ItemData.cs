using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite sprite;
    public int damage;
    public int value;
    public int armor;
    public int speed;
    public enum ItemType
    {
        Weapon,
        Armor,
        Headgear,
        Offhand,
        Ring,
        Amulet,
        Ranged,
        Boots
    }
    public ItemType itemType;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public Dictionary<string, int> playerStats = new Dictionary<string, int>
    {
        { "damage", 0 },
        { "speed", 0 },
        { "armor", 0 },

    };
    private TextMeshProUGUI damageValueText;
    private TextMeshProUGUI speedValueText;
    private TextMeshProUGUI armorValueText;
    public GameObject equipmentMenuGameObject;

    //public Dictionary<string, TextMeshProUGUI> texts = new Dictionary<string, TextMeshProUGUI>
    //{
    //    { "Damage", damageValueText },
    //    { "Speed", speedValueText },
    //    { "Armor", armorValueText },

    //};


    void Start()
    {

        damageValueText = GameObject.Find("DamageValue").GetComponent<TextMeshProUGUI>();
        speedValueText = GameObject.Find("SpeedValue").GetComponent<TextMeshProUGUI>();
        armorValueText = GameObject.Find("ArmorValue").GetComponent<TextMeshProUGUI>();
        equipmentMenuGameObject = GameObject.Find("EquipmentMenu").gameObject;

        //print(playerStats["damage"]);
        IncreaseStats("speed", 10);
        //print(playerStats["damage"]);
        UpdateAllStatsText();

        CalculateAllEquipmentSlotsAndAddData();
    }

    public void IncreaseStats(string statsToIncrease, int amountToIncrease)
    {
        playerStats[statsToIncrease] += amountToIncrease;
    }
    public void DecreaseStats(string statsToDecrease, int amountToDecrease)
    {
        playerStats[statsToDecrease] += amountToDecrease;
    }

    public void UpdateAllStatsText()
    {
        damageValueText.text = playerStats["damage"].ToString();
        speedValueText.text = playerStats["speed"].ToString();
        armorValueText.text = playerStats["armor"].ToString();
    }
    public void SetAllPlayerStatsToZero()
    {
        playerStats["damage"] = 0;
        playerStats["speed"] = 0;
        playerStats["armor"] = 0;
    }

    public void CalculateAllEquipmentSlotsAndAddData()
    {
        SetAllPlayerStatsToZero();
        foreach (Transform equipmentSlot in equipmentMenuGameObject.transform)
        {
            if(equipmentSlot.childCount > 0)
            {
                //print($"Itemdata from equipment slot is: {equipmentSlot.GetComponentInChildren<ItemData>().damage}");
                var itemDataToAdd = equipmentSlot.GetComponentInChildren<ItemData>();
                IncreaseStats("damage", itemDataToAdd.damage);
                IncreaseStats("speed", itemDataToAdd.speed);
                IncreaseStats("armor", itemDataToAdd.armor);
            }
            
        }
        UpdateAllStatsText();
    }
}

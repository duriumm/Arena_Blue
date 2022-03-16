using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    private Slider manaSlider;
    private GameObject canvasPrefab;
    public float manaToDecreasePerTick = 2f;
    public float manaToIncreasePerTick = 2f;
    private float currentMana = 100f;
    private float maxMana = 100f;
    public bool isManaIncreasing = false;

    private GameObject manaStarsParticleEffectObject;


    void Start()
    {
        canvasPrefab = GameObject.FindWithTag("Canvas");
        manaSlider = canvasPrefab.transform.GetChild(0).gameObject.GetComponent<Slider>();

        manaStarsParticleEffectObject = canvasPrefab.transform.GetChild(0).gameObject.transform.Find("Fill").gameObject.transform.Find("ManaDrain Effect").gameObject;

        manaSlider.maxValue = maxMana;

    }

    private void Update()
    {
        if(isManaIncreasing == true)
        {
            IncreaseMana();
        }
        else if(isManaIncreasing == false)
        {
            DecreaseMana();
        }
        manaSlider.value = currentMana;
    }

    public void DecreaseMana()
    {
        if(currentMana <= 0)
        {
            return;
        }
        isManaIncreasing = false;
        manaStarsParticleEffectObject.SetActive(true);
        currentMana -= manaToDecreasePerTick * Time.deltaTime;
  
    }

    public void IncreaseMana()
    {
        if(currentMana >= maxMana)
        {
            return;
        }
        isManaIncreasing = true;
        manaStarsParticleEffectObject.SetActive(false);       
        currentMana += manaToDecreasePerTick * Time.deltaTime;
       
    }
}

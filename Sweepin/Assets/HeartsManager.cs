using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject originalHeartPrefab;
    private GameObject heartsContainer;
    private int currentAmountOfhearts;
    public bool testRemoveHeart = false;
    public bool testCREATEheart = false;

    void Start()
    {
        heartsContainer = this.gameObject;
        currentAmountOfhearts = currentAmountOfhearts = heartsContainer.transform.childCount;
        //print("START current amount of hearts is: " + currentAmountOfhearts);
    }
    private void Update()
    {
        //if(testRemoveHeart == true)
        //{
        //    DecreaseHearts(1);
        //}
        //if (testCREATEheart == true)
        //{
        //    IncreaseHearts(1);
        //}

    }

    public int DecreaseHearts(int amountOfHeartsToDecrease)
    {
        GameObject heartGameObjectToRemove = transform.GetChild(currentAmountOfhearts - 1).gameObject;
        heartGameObjectToRemove.transform.parent = null;
        Destroy(heartGameObjectToRemove);
        currentAmountOfhearts = heartsContainer.transform.childCount;
        print("After DECREASE HEARTS amount of hearts is: " + currentAmountOfhearts);
        testRemoveHeart = false;
        return currentAmountOfhearts;

    }
    public int IncreaseHearts(int amountOfHeartsToIncrease)
    {
        for (int i = 0; i < amountOfHeartsToIncrease; i++)
        {
            Instantiate(originalHeartPrefab, heartsContainer.transform);
        }
        
        currentAmountOfhearts = heartsContainer.transform.childCount;
        testCREATEheart = false;
        return currentAmountOfhearts;
    }
}

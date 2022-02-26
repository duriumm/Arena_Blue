using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    public void ShowDataOnHover()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }
    public void RemoveDataOnExit()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamDontDestroy : MonoBehaviour
{
    private static bool mainCameraExists;
    void Start()
    {
        if (!mainCameraExists)
        {
            mainCameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}

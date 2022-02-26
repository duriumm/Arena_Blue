using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{


    [SerializeField]
    private static bool objectExists = false;
    void Awake()
    {
        // If we dont find any gameObject with the same name
        // This will apply to MyCharacter, Main Camera & CM vcam1
        if (!objectExists)
        {
            objectExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // This start function will only run ONCE in the first scene and then never again which will make our 
    // gameobject stay alive over every scene :)
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

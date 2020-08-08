using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Music : MonoBehaviour
{

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
}

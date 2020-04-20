using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoot : MonoBehaviour
{
    public GameObject ModuleRootObject;
    private void Start()
    {
        if (ModuleRoot.Ins == null)
        {
            Instantiate(ModuleRootObject);
            SceneManager.LoadSceneAsync(1);
        }

    }
}

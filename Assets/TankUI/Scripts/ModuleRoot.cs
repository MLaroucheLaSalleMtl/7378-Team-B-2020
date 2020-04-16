using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRoot : MonoBehaviour
{
    public static ModuleRoot Ins;
    public MainUICtrl UIModule;
  

    private void Awake()
    {
        Ins = this;
    }

    public ModuleData ModuleData;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

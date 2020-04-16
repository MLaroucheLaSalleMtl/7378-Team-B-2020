using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuadType
{
    ENEMY,
    PLAYER
}
public class AddMiniMapQuad : MonoBehaviour
{
  
    public QuadType Type;
    void Start()
    {
        switch (Type)
        {
            case QuadType.ENEMY:
              GameObject go1=  Instantiate(ModuleRoot.Ins.ModuleData.EnemyQuad);
              go1.transform.SetParent(transform);
              go1.transform.localPosition=new Vector3(0,15,0);
              go1.transform.localScale=Vector3.one;;
                break;
            case QuadType.PLAYER:
                GameObject go2=  Instantiate(ModuleRoot.Ins.ModuleData.PlayerQuad);
                go2.transform.SetParent(transform);
                go2.transform.localPosition=new Vector3(0,340,0);
                go2.transform.localScale=Vector3.one;;
                break;
          
        }
    }

 
}

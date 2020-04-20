using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameCtrl : MonoBehaviour
{
  [SerializeField] private Text playerName;

  private void Start()
  {
     playerName.text = ModuleRoot.Ins.ModuleData.PlayName;
  }
}

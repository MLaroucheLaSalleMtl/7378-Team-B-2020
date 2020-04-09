using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinProgressObjCtrl : MonoBehaviour
{
    public float rotSpeed = 2f;
    public float curProgress = 0f;
    //each secs slider add val
    public float progressAddDeltaVal = 0.1f;
    public Action onProgressEndCallBack;
    public bool isStartCalcWinProgress = false;
    public string playerTag = "Player";
    public GameObject sliderCanvas = null;
    public Slider slider;
    void Update()
    {
        transform.Rotate(Vector3.up*Time.deltaTime*rotSpeed);
        if (isStartCalcWinProgress)
        {
            if (sliderCanvas != null && !sliderCanvas.gameObject.activeInHierarchy)
            {
                sliderCanvas.gameObject.SetActive(true);
            }
            curProgress += progressAddDeltaVal * Time.deltaTime;
            if (slider != null) slider.value = curProgress;
            if (slider.value >= 0.99f)
            {
                if (onProgressEndCallBack != null)
                {
                    onProgressEndCallBack();
                }

            }
        }
    }

    public void RegisterEndEvt(Action endEvt)
    {
        if (endEvt != null)
        {
            onProgressEndCallBack = endEvt;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(playerTag, StringComparison.CurrentCultureIgnoreCase))
        {
            isStartCalcWinProgress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(playerTag, StringComparison.CurrentCultureIgnoreCase))
        {
            isStartCalcWinProgress = false;
            sliderCanvas.SetActive(false);
        }
    }

  
}

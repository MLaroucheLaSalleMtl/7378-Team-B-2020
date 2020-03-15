using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    public Text shell_indicator;
    public Text ReloadTime;
    public Text APammo;
    public Text HEammo;
    public static int AP;
    public static int HE;
    public static int currentShell;
    public static float reload;
    public static bool canCount = true;
    public static bool doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        currentShell = 1;
        reload = 5.0f;
        AP = 20;
        HE = 20;
        APammo.text = AP.ToString();
        HEammo.text = HE.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(reload >= 0.0f && canCount)
        {
            reload -= Time.deltaTime;
            ReloadTime.text = reload.ToString("F")+"sec";
        }
        else if(reload <= 0.0f && !doOnce)
        {
            //PlayerWeaponController.canFire = true;
            canCount = false;
            doOnce = true;
            ReloadTime.text = "5.0sec";
            reload = 0.0f;
        }
        APammo.text = AP.ToString();
        HEammo.text = HE.ToString();
    }
    public void SwitchAP()
    {
        currentShell = 1;
        shell_indicator.text = "AP Loaded";
    }

    public void SwitchHE()
    {
        currentShell = 2;
        shell_indicator.text = "HE Loaded";
    }

    public void resetTimer()
    {
        //PlayerWeaponController.canFire = false;
        reload = 5.0f;
        canCount = true;
        doOnce = false;
    }
}

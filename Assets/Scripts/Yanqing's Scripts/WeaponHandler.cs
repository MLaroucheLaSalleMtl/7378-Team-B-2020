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
    private  int AP;
    private int HE;
    public  int currentShell;
    private float reload;
    private  bool canCount = true;
    private  bool doOnce = false;
    public bool canFire = false;

    public int AP1 { get => AP; set => AP = value; }
    public int HE1 { get => HE; set => HE = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentShell = 1;
        shell_indicator.text = "AP Loaded";
        
        reload = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponController>().MaxReload;

        AP1 = 40;
        HE1 = 35;
        APammo.text = AP1.ToString();
        HEammo.text = HE1.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (reload >= 0.0f && canCount)
        {
            reload -= Time.deltaTime;
            ReloadTime.text = reload.ToString("F") + "sec";
        }
        else if (reload <= 0.0f && !doOnce)
        {
            canFire = true;
            canCount = false;
            doOnce = true;
            ReloadTime.text = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponController>().MaxReload + "sec";
            reload = 0.0f;
        }
        APammo.text = AP1.ToString();
        HEammo.text = HE1.ToString();
    }
    public void SwitchAP()
    {
        if(AP1 != 0)
        {
            currentShell = 1;
            shell_indicator.text = "AP Loaded";
        }
        if(AP1 == 0)
        {
            shell_indicator.text = "Out of AP";
        }
    }

    public void SwitchHE()
    {
        if(HE1 != 0)
        {
            currentShell = 2;
            shell_indicator.text = "HE Loaded";
        }
        if(HE1 == 0)
        {
            shell_indicator.text = "Out of HE";
        }
    }

    public void resetTimer()
    {
        canFire = false;
        reload = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponController>().MaxReload;
        canCount = true;
        doOnce = false;
    }
}
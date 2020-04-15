using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitEvent : MonoBehaviour
{
    public Text HEpen;
    public Text APpen;
    public Text unpen;
    public Camera cam;
    public GameObject Canvas;
    Color colorA ;
    Color colorH;

    private bool AP = false;
    private bool HE = false;
    private bool UN = false;
    // Start is called before the first frame update
    void Start()
    {    
        cam = Camera.main;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "HE")
        {
            Vector2 screenPos = cam.WorldToScreenPoint(this.transform.position);
            screenPos.x += 150;
            //GameObject.Instantiate(HEpen);
            colorH = HEpen.color;
            colorH.a = 1;
            HEpen.rectTransform.position = screenPos;
            HE = true;
        }
        if (collision.transform.tag == "AP")
        {
            Vector2 screenPos = cam.WorldToScreenPoint(this.transform.position);
            screenPos.x += 150;
            //Instantiate(APpen);
            colorA = APpen.color;
            colorA.a = 1;
            APpen.rectTransform.position = screenPos;
            AP = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (AP)
        {
            colorA.a = Mathf.Lerp(colorA.a, 0, Time.deltaTime);
            print(colorA.a);
            APpen.color = colorA;
            if (colorA.a == 0f)
            {
                colorA.a = 0;
                AP = false;
            }
        }

        if (HE)
        {
            colorH.a = Mathf.Lerp(colorH.a, 0, Time.deltaTime);
            HEpen.color = colorH;
            if (colorH.a == 0f)
            {
                colorH.a = 0;
                HE = false;
            }
        }
        if(UN)
        {

        }

    }
}

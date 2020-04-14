using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayDamage : MonoBehaviour
{
    public GameObject pen;
    public GameObject Unpen;
    private static GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }


    public void Pen(int damage, Transform location)
    {
        GameObject dmg = Instantiate(pen);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        dmg.transform.SetParent(canvas.transform, false);
        dmg.transform.position = screenPosition;
        dmg.GetComponentInChildren<Text>().text = "-" + damage.ToString();
    }

    public void Ricochet(Transform location)
    {
        GameObject dmg = Instantiate(Unpen);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        dmg.transform.SetParent(canvas.transform, false);
        dmg.transform.position = screenPosition;
        dmg.GetComponentInChildren<Text>().text = "Ricochet";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

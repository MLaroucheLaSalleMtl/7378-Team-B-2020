using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instruction : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject Light;
    public GameObject Medium;
    public GameObject Heavy;
    public static int step = -1;
    public Text instruction;
    public GameObject[] waypoints;
    private bool cando1 = false;
    private bool cando2 = false;
    private bool cando3 = false;
    private bool cando4 = false;
    private bool cando5 = false;
    private bool cando6 = false;
    private bool cando7 = false;
    public static bool Completed;
    // Start is called before the first frame update
    private void Awake()
    {
        Spawn(Menu_Switch.tank_id);

    }
    void Start()
    {
        StartCoroutine(Nextstep());
    }

    private void Spawn(int id)
    {
        switch (id)
        {
            case 1:
                GameObject.Instantiate(Light, spawnPos.position, Quaternion.Euler(0, 90, 0));
                break;
            case 2:
                GameObject.Instantiate(Medium, spawnPos.position, Quaternion.Euler(0, 90, 0));
                break;
            case 3:
                GameObject.Instantiate(Heavy, spawnPos.position, Quaternion.Euler(0, 90, 0));
                break;
        }
    }

    IEnumerator Nextstep()
    {
        yield return new WaitForSeconds(5);
        step += 1;
    }
    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 0:
                cando1 = true;
                instruction.text = "Let's start by driving your tank. Reach the Designated place!, you can Press W/push left stick upward to drive forward, or press S/push left stick downward to drive backward";
                break;
            case 1:
                cando2 = true;
                instruction.text = "Great! Now try to start turning your tank. Press A/push let stick to left to turn left, press D/push the stick to right to turn right.";
                break;
            case 2:
                cando3 = true;
                instruction.text = "Nicely done, now drive to the designated place, we'll try some shooting!";
                break;
            case 3:
                cando4 = true;
                instruction.text = "These red walls are breakable, drive straight through! Some other obstacles in these game are breakable too";
                break;
            case 4:
                cando5 = true;
                instruction.text = "Move your mouse/right stick to change your perspective! You'll notice your turret is rotating together with you. Each tank has a different rotation rate depends on their type!";
                break;
            case 5:
                cando6 = true;
                instruction.text = "Now let's try to aim at our target, you can see two different types of shells on the bottom right.  Press 1/X to switch to AP rounds, press 2/B to switch to HE rounds. Click left mouse button/press right trigger to fire!";
                break;
            case 6:
                instruction.text = "Heavily armored target has more equivalent armor when the incident angle is too big, try replace yourself. You can also try to press Shift/left shoulder to bring up telescope view.";
                break;
            case 7:

                instruction.text = "You've finished tutorials! You can press Esc/Start at any moment to return to Main menu, or start level 1 using the same tank";
                break;
        }
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i == step)
            {
                waypoints[i].SetActive(true);
            }
            else waypoints[i].SetActive(false);
        }
        if (step == 1 && cando1)
        {
            float a = 0;
            a = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || a != 0)
            {
                step += 1;
            }
        }
        if (step == 4 && cando4)
        {
            float a = 0;
            a = Input.GetAxis("Mouse X");
            if (a != 0) step += 1;
        }
        if (step == 5 && cando5)
        {
            float a;
            a = Input.GetAxis("Trigger");
            if (Input.GetKey(KeyCode.Mouse0) || a == 1)
            {
                step += 1;
            }
        }
        if (step == 6 && cando6)
        {
            float a;
            a = Input.GetAxis("Trigger");
            if (Input.GetKey(KeyCode.LeftShift) || a == -1)
            {
                print("pressed");
                step += 1;
            }
        }
        if (step == 7)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelEndWin(1);
            Completed = true;
        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneTank : MonoBehaviour
{
    private TankTrackAnimation[] tracks;
    public Transform Turret;
    public Transform target;
    private float TankSpeed = 5f;
    private bool Back = false;
    private bool stopNfire = false;
    public GameObject backStop;
    public AudioSource engine;
    private PlayerWeaponController wp;

    // Start is called before the first frame update
    void Start()
    {
        tracks = GetComponentsInChildren<TankTrackAnimation>();
        wp = new PlayerWeaponController();
    }

    private void Forward()
    {
        this.transform.position += this.transform.forward * TankSpeed * Time.deltaTime;
        foreach(TankTrackAnimation track in tracks)
        {
            track.MoveTrack(new Vector2(1, 0));
        }
    }

    private void Backward()
    {
        this.transform.position += -this.transform.forward * 3f * Time.deltaTime;
        foreach (TankTrackAnimation track in tracks)
        {
            track.MoveTrack(new Vector2(-0.6f, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "FrontStop")
        {
            Back = true;
            backStop.SetActive(true);
        }
        else if(collision.gameObject.name == "BackStop")
        {
            print("stop");
            StartCoroutine(Fire());
            stopNfire = true;
        }

    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(2.0f);
        PlayerWeaponController.fire = true;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("GameMenu");
    }
    // Update is called once per frame
    void Update()
    {
        if(!stopNfire)
        {
            if (!Back)
            {
                Forward();
            }
            if (Back)
            {
                Backward();
                Vector3 localTargetPos = transform.InverseTransformPoint(target.position);
                Quaternion rotationGoal = Quaternion.LookRotation(localTargetPos);
                Quaternion newRotation = Quaternion.RotateTowards(Turret.localRotation, rotationGoal, 30f * Time.deltaTime);
                Turret.localRotation = newRotation;
            }
        }
    }
}

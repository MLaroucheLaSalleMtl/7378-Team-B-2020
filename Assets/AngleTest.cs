using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 vel;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        //StartCoroutine(SaveSpeed());
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        var collisionAngle = Mathf.Abs(90 - (Vector3.Angle(vel,normal)));

        print(collisionAngle);
        Destroy(this.gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        vel = rigidbody.velocity;
    }

    IEnumerator SaveSpeed()
    {
        yield return new WaitForFixedUpdate();
        vel = rigidbody.velocity;
    }
}

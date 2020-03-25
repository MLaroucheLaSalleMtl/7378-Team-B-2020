using System;
using UnityEngine;

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
<<<<<<< HEAD
    private TankTrackAnimation[] tracks;

=======
>>>>>>> master


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
<<<<<<< HEAD
            tracks = GetComponentsInChildren<TankTrackAnimation>();
    }
=======
        }
>>>>>>> master


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
<<<<<<< HEAD

        if (v > 0)
        {
            print("forward");
            foreach (TankTrackAnimation track in tracks)
            {
                track.MoveTrack(new Vector2(1f, 0f));
            }
        }
        else if (v < 0)
        {
            print("backward");
            foreach (TankTrackAnimation track in tracks)
            {
                track.MoveTrack(new Vector2(-1f, 0f));
            }
        }
    }
    }
=======
        }
    }
>>>>>>> master

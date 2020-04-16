using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car; // the car controller we want to use
    private TankTrackAnimation[] tracks;

    private void Awake()
    {
        // get the car controller
        m_Car = GetComponent<CarController>();
        tracks = GetComponentsInChildren<TankTrackAnimation>();
    }


        private void Update()
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

        if (v > 0)
        {
            if (h < 0)
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    if (track.right)
                    {
                        track.MoveTrack(new Vector2(1f, 0f));
                    }
                    if (track.left)
                    {
                        track.MoveTrack(new Vector2(0.3f, 0f));
                    }
                }
            }
            else if (h > 0)
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    if (track.right)
                    {
                        track.MoveTrack(new Vector2(0.3f, 0f));
                    }
                    if (track.left)
                    {
                        track.MoveTrack(new Vector2(1f, 0f));
                    }
                }
            }
            else
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    track.MoveTrack(new Vector2(1f, 0f));
                }
            }

        }
        else if (v < 0)
        {
            if (h < 0)
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    if(track.right)
                    {
                        track.MoveTrack(new Vector2(-1f, 0f));
                    }
                    if(track.left)
                    {
                        track.MoveTrack(new Vector2(-0.3f, 0f));
                    }
                }
            }
            else if (h > 0)
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    if (track.right)
                    {
                        track.MoveTrack(new Vector2(-0.3f, 0f));
                    }
                    if (track.left)
                    {
                        track.MoveTrack(new Vector2(-1f, 0f));
                    }
                }
            }
            else
            {
                foreach (TankTrackAnimation track in tracks)
                {
                    track.MoveTrack(new Vector2(-1f, 0f));
                }
            }
        }
        else if (h < 0)
        {
            foreach (TankTrackAnimation track in tracks)
            {
                if (track.right)
                {
                    track.MoveTrack(new Vector2(1f, 0f));
                }
                if (track.left)
                {
                    track.MoveTrack(new Vector2(-1f, 0f));
                }
            }
        }
        else if (h > 0)
        {
            foreach (TankTrackAnimation track in tracks)
            {
                if (track.right)
                {
                    track.MoveTrack(new Vector2(-1f, 0f));
                }
                if (track.left)
                {
                    track.MoveTrack(new Vector2(1f, 0f));
                }
            }
        }
        //print("Current: " + m_Car.CurrentSpeed + "  4s before: " + m_Car.m_PrevSpeed);
    }

}

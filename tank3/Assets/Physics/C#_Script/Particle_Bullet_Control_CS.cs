using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ChobiAssets.PTM
{

	public class Particle_Bullet_Control_CS : MonoBehaviour
	{
        /*
		 * This script should be attached to the gameobject that has a particle system used for generating the particle bullets.
		 */


        // User options >>
        public float Attack_Point;

        // << User options

        ParticleSystem thisParticleSystem;
        List<ParticleCollisionEvent> collisionEvents;


        void Start()
		{
			Initialize();
		}


		void Initialize()
		{
            thisParticleSystem = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
        }


        void OnParticleCollision(GameObject hitObject)
        {
            int eventsCount = thisParticleSystem.GetCollisionEvents(hitObject, collisionEvents);

            for (int i = 0; i < eventsCount; i++)
            {
                // Get the "Damage_Control_##_##_CS" script in the hit object.
                var damageScript = collisionEvents[i].colliderComponent.GetComponent<Damage_Control_00_Base_CS>();
                if (damageScript == null)
                { // The hit object does not have "Damage_Control_##_##_CS" script.
                    continue;
                }
                // The hit object has "Damage_Control_##_##_CS" script. >> It should be a breakable object.

                // Calculate the hit damage.
                float damageValue = Attack_Point;

                // Send the damage value to "Damage_Control_##_##_CS" script.
                damageScript.Get_Damage(damageValue, 0);
            }
           
        }


	}

}
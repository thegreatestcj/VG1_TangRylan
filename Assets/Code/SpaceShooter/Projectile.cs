using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody2D _rb;

        Transform target;
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void ChooseNearestTarget()
        {
            float closestDistance = 9999f;

            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
            // Debug.Log("Asteroids found");

            for(int i = 0;  i < asteroids.Length; i++)
            {
                Asteroid asteroid = asteroids[i];

                if (asteroid.transform.position.x > transform.position.x)
                {
                    Vector2 directionToTarget = asteroid.transform.position - transform.position;

                    if (directionToTarget.sqrMagnitude < closestDistance)
                    {
                        closestDistance = directionToTarget.sqrMagnitude;

                        target = asteroid.transform;
                    }
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            float acceleration = 0.5f;
            float maxSpeed = 1f;

            ChooseNearestTarget();
            if(target != null)
            {
                Vector2 directionToTarget = target.position - transform.position;
                float distanceToTarget = directionToTarget.magnitude;

                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                _rb.MoveRotation(angle);

                // Dynamic acceleration and maxSpeed
                acceleration = acceleration + distanceToTarget * 0.05f;
                maxSpeed = maxSpeed + distanceToTarget * 0.3f;

            }


            _rb.AddForce(transform.right * acceleration);
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);



        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<Asteroid>())
            {
                Destroy(other.gameObject);
                Destroy(gameObject);

                GameObject explosion = Instantiate(
                    GameController.instance.explosionPrefab,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(explosion, 0.25f);
            }
        }
    }

}

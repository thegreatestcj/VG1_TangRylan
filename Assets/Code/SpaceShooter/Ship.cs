using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        public GameObject projectilePrefab;

        // I increased the firingDelay.
        public float firingDelay = 5f;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("FiringTimer");
        }

        IEnumerator FiringTimer()
        {
            yield return new WaitForSeconds(firingDelay);

            FireProjectile();

            StartCoroutine("FiringTimer");
        }

        void FireProjectile()
        {
            Debug.Log("Projectile Fired");
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
            transform.position = new Vector2(0, yPosition);
        }
    }

}

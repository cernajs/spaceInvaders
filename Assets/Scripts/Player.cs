using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Shoot shootPrefab;

    public float speed = 8.0f;
    private bool _laserShoot;

    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_laserShoot)
            {
                _laserShoot = true;

                var laserShootStart = this.transform.position + Vector3.up;

                Shoot laserBeam = Instantiate(this.shootPrefab, laserShootStart, Quaternion.identity);
                laserBeam.collision += hit;
                
            }
        }
    }

    private void hit()
    {
        _laserShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") || other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //private void ShootLaser()
}

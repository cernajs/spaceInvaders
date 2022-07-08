
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed;

    public Vector3 direction;

    public System.Action collision;

    private void Update()
    {
        this.transform.position += this.direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.collision != null)
        {
            this.collision.Invoke();
        }
        Debug.Log(other);
        Destroy(this.gameObject);
    }
}

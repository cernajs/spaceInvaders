using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpaceship : MonoBehaviour
{
    private Vector3 _direction = Vector2.right;
    public float speed = 18.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            ScoreManager.instance.AddExtraPoint();
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        this.transform.position += _direction * this.speed * Time.deltaTime;

        Vector3 leftBoundarie = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightBoundarie = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (this.gameObject.activeInHierarchy && _direction == Vector3.right && this.transform.position.x >= (rightBoundarie.x) * 3)
        {
            _direction.x *= -1.0f;
        }
        else if (this.gameObject.activeInHierarchy && _direction == Vector3.left && this.transform.position.x <= -(rightBoundarie.x) * 3)
        {
            _direction.x *= -1.0f;
        }
    }
}

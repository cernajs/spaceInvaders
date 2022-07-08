using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public System.Action kill;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //hit by laser
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.gameObject.SetActive(false);
            this.kill.Invoke();
        }
    }
}

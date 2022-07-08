using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;

    public Shoot missilePref;

    public int columns = 11;
    public int rows = 5;

    public int numOfInvadersKIlled { get; private set; }

    public int totalInvadersCount => this.rows * this.columns;
    
    public float percentageKilled => (float)this.numOfInvadersKIlled / (float)this.totalInvadersCount;

    private float spacing = 2.8f; // mezery mezi invadery
    //public AnimationCurve speed = 9.0f;
    public AnimationCurve speed = new AnimationCurve();

    private Vector3 _direction = Vector2.right;
    //public AnimationCurve speed;

    //GENERATION

    private void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = spacing * (this.columns - 1);
            float height = spacing * (this.rows - 1);

            Vector2 centerInvaders = new Vector2(-width / 2, -height / 2);
            Vector3 rowCount = new Vector3(centerInvaders.x, centerInvaders.y + row * spacing, 0.0f);

            for (int column = 0; column < this.columns; column++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.kill += onDead;
                Vector3 pos = rowCount;
                pos.x += column * spacing;
                invader.transform.localPosition = pos;
            }
        }
    }

    private void onDead()
    {
        this.numOfInvadersKIlled++;

        ScoreManager.instance.AddPoint();

        if (this.totalInvadersCount == this.numOfInvadersKIlled)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(fireMissile), 1, 1);
    }

    private void fireMissile()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value <= (1.0f / (this.totalInvadersCount - this.numOfInvadersKIlled)))
            {
                Instantiate(this.missilePref, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    //MOVEMENT

    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentageKilled) * Time.deltaTime;

        Vector3 leftBoundarie = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightBoundarie = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (invader.gameObject.activeInHierarchy && _direction == Vector3.right && invader.position.x >= (rightBoundarie.x - 1))
            {
                _direction.x *= -1.0f;
                Vector3 currentPosition = this.transform.position;
                currentPosition.y -= 1.0f;
                this.transform.position = currentPosition;
            }
            else if (invader.gameObject.activeInHierarchy && _direction == Vector3.left && invader.position.x <= (leftBoundarie.x + 1))
            {
                _direction.x *= -1.0f;
                Vector3 currentPosition = this.transform.position;
                currentPosition.y -= 1.0f;
                this.transform.position = currentPosition;
            }
        }
    }

}

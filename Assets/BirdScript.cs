using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private float highestPoint = 16;
    private float lowestPoint = -16;
    private float deadZone = -20;

    public float rotationSpeed = 1f; // Velocidad de rotación
    private const float TargetRotation = -77f; // Rotación objetivo en caída libre

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            ResetRotation();
        }

        if (IsInFreeFall())
        {
            RotateInFreeFall();
        }

        if (transform.position.y > highestPoint || transform.position.y < lowestPoint)
        {
            logic.gameOver();
            birdIsAlive=false;

            isOutOfBounds();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;

        isOutOfBounds();
    }

    void isOutOfBounds()
    {
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    private bool IsInFreeFall()
    {
        // El GameObject está en caída libre si su velocidad vertical (y) es negativa
        return myRigidbody.velocity.y < 0;
    }

    private void RotateInFreeFall()
    {
        transform.rotation = Quaternion.Euler(0, 0, -77);
    }

    private void ResetRotation()
    {
        // Restablecer rotación a 0
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


}
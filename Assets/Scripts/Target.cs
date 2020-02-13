using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explotionParticle;

    private float speedMin = 12;
    private float speedMax = 16;
    private float torque = 10;
    private float xRange = 4;
    private float yPos = -2;

    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        RandomForceGenerator();
        RandomTorqueGenerator();
        RandomPositionGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomForceGenerator()
    {
        targetRb.AddForce(Vector3.up * Random.Range(speedMin, speedMax), ForceMode.Impulse);
    }

    void RandomTorqueGenerator()
    {
        targetRb.AddTorque(Random.Range(-torque,torque), Random.Range(-torque,torque), Random.Range(-torque,torque), ForceMode.Impulse);
    }
    
    void RandomPositionGenerator()
    {
        transform.position = new Vector3(Random.Range(-xRange, xRange), yPos);
    }

    //Destroy on mouse click
    private void OnMouseDown()
    {
        if (gameManager.gameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);

            Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);
        }
    }

    //Destroy fallen objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            if (!gameObject.CompareTag("Bad"))
            {
                if (gameManager.gameActive)
                {
                    gameManager.UpdateLives(1);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10.0f;
    private float leftbound = -10.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player controller script from the player gameobject 
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // moving the objects if game is not over
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // If obstacles fall behind the map they are destroyed
        if(transform.position.x < leftbound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

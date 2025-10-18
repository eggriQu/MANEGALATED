using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackOpponentsPosition : MonoBehaviour
{
    public GameObject opponent;
    public SpriteRenderer sprite;
    public float oppXPos;
    public bool flipped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oppXPos = opponent.transform.position.x;
        if (transform.position.x - oppXPos > 0)
        {
            flipped = true;
            sprite.flipX = true;
        }
        else
        {
            flipped = false;
            sprite.flipX = false;
        }
    }
}

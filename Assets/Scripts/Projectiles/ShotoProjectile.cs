using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoProjectile : MonoBehaviour
{
    public GameObject gameObj;
    private Animator anim;
    private Transform projectileTransform;

    void Awake()
    {
        anim = gameObj.GetComponent<Animator>();
        projectileTransform = gameObj.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projectileTransform.Translate(new Vector2(16.5f, 0) * Time.deltaTime);

        if (projectileTransform.position.x <= -30 || projectileTransform.position.x >= 30)
        {
            Destroy(gameObj);
        }
    }
}

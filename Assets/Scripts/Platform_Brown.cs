using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Brown : MonoBehaviour {

    private bool Fall_Down = false;
    [SerializeField] private float speed = 6f;

    EdgeCollider2D edgeCollider2D;
    PlatformEffector2D platformEffector2D;


    private void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        platformEffector2D = GetComponent<PlatformEffector2D>();
        
    }

    void FixedUpdate () 
    {
        if (Fall_Down)
            transform.position -= Vector3.up * speed * Time.deltaTime;
	}

    public void Deactive()
    {
        edgeCollider2D.enabled = false;
        platformEffector2D.enabled = false;

        Fall_Down = true;
    }

    public void OnEnable()
    {
        edgeCollider2D.enabled = true;
        platformEffector2D.enabled = true;

        Fall_Down = false;
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_White : MonoBehaviour {

    EdgeCollider2D edgeCollider2D;
    PlatformEffector2D platformEffector2D;

    private void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    public void Deactive()
    {
        edgeCollider2D.enabled = false;
        platformEffector2D.enabled = false;
    }

    private void OnEnable()
    {
        edgeCollider2D.enabled = true;
        platformEffector2D.enabled = true;
    }
}

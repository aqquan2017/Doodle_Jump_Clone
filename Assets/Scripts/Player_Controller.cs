using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_Controller : MonoBehaviour {

    Rigidbody2D Rigid;
    public float Movement_Speed = 10f;
    private float Movement = 0;
    private int Direction = 1;
    private Vector3 Player_LocalScale;

    public Sprite[] Spr_Player = new Sprite[2];

    private Sprite playerSprite;
    private BoxCollider2D boxCollider;

    //Touch or use acceleration
    public bool isTouch = true;

	// Use this for initialization
	void Start () 
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Rigid = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>().sprite;
        Player_LocalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Set Movement value
#if UNITY_EDITOR
        Movement = Input.GetAxis("Horizontal") * Movement_Speed; 

#elif UNITY_ANDROID
        Debug.LogError("ANDROID");
        if (isTouch)
        {
            Movement = 0;
            if (Input.touchCount > 0)
            {
                print("Touch");
                var touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    Movement = -Movement_Speed;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    Movement = Movement_Speed;
                }
            }
        }
        else
        {
            //acceleation
            Movement = Input.acceleration.x* Movement_Speed;
        }

#endif

        // Player look right or left
        if (Movement > 0)
            transform.localScale = new Vector3(Player_LocalScale.x, Player_LocalScale.y, Player_LocalScale.z);
        else if (Movement < 0)
            transform.localScale = new Vector3(-Player_LocalScale.x, Player_LocalScale.y, Player_LocalScale.z);
	}

    void FixedUpdate()
    {
        // Calculate player velocity
        Vector2 Velocity = Rigid.velocity;
        Velocity.x = Movement;
        Rigid.velocity = Velocity;

        // Player change sprite
        if (Velocity.y < 0)
        {
            playerSprite = Spr_Player[0];

            // Active player collider
            boxCollider.enabled = true;

            // Fall propeller after fly up
            Propeller_Fall();
        }
        else
        {
            playerSprite = Spr_Player[1];

            // Deactive player collider
            boxCollider.enabled = false;
        }

        // Player wrap
        Vector3 Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        float Offset = 0.5f;

        if (transform.position.x > -Top_Left.x + Offset)
            transform.position = new Vector3(Top_Left.x - Offset, transform.position.y, transform.position.z);
        else if(transform.position.x < Top_Left.x - Offset)
            transform.position = new Vector3(-Top_Left.x + Offset, transform.position.y, transform.position.z);
    }

    void Propeller_Fall()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Active", false);
            transform.GetChild(0).GetComponent<Propeller>().Set_Fall(gameObject);
            transform.GetChild(0).parent = null;
        }
    }

    public void PlayerRotate360(float totalTime, int degTotal) 
    {
        transform.DORotate(Vector3.forward * degTotal, totalTime, RotateMode.FastBeyond360);
    }

    
}

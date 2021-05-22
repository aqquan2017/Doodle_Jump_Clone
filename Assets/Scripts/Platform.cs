using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TheDeveloper.AdvancedObjectPool;

public class Platform : MonoBehaviour {

    public float Jump_Force = 10f;
    [SerializeField] private float Destroy_Distance;
    [SerializeField] private bool Create_NewPlatform = false;

    private GameObject Game_Controller;
    private Platform_Generator platform_Generator;
    private Animator animator;
    private AudioSource audioSource;
    private ObjectPool objectPool;

    // Use this for initialization
    void Start()
    {
        Game_Controller = GameObject.Find("Game_Controller");
        platform_Generator = Game_Controller.GetComponent<Platform_Generator>();
        audioSource = GetComponent<AudioSource>();

        // Set distance to destroy the platforms out of screen
        if (Game_Controller != null)
            Destroy_Distance = Game_Controller.GetComponent<Game_Controller>().Get_DestroyDistance();

        animator = GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    private void OnEnable()
    {
        Create_NewPlatform = false;
        
        if(animator != null)
        {
            animator.SetBool("Active", false);
            
        }
    }

    void FixedUpdate()
    {
        // Platform out of screen
        if (transform.position.y - Camera.main.transform.position.y < Destroy_Distance)
        {
            // Create new platform
            if (Game_Controller != null && name != "Platform_Brown(Clone)" && name != "Spring(Clone)" && name != "Trampoline(Clone)" && !Create_NewPlatform)
            {
                platform_Generator.Generate_Platform(1);
                Create_NewPlatform = true;
            }
            
            // Deactive Collider and effector
            //GetComponent<EdgeCollider2D>().enabled = false;
            //GetComponent<PlatformEffector2D>().enabled = false;
            //GetComponent<SpriteRenderer>().enabled = false;

            // Deactive collider and effector if gameobject has child
            if (transform.childCount > 0)
            {
                //if(transform.GetChild(0).GetComponent<Platform>()) // if child is platform
                //{
                //    transform.GetChild(0).GetComponent<EdgeCollider2D>().enabled = false;
                //    transform.GetChild(0).GetComponent<PlatformEffector2D>().enabled = false;
                //    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                //}

                // Destroy this platform if sound has finished
                if (!audioSource.isPlaying)
                {
                    objectPool = GetComponentInParent<ObjectPool>();

                    if (objectPool != null)
                    {
                        //animator.SetBool("Active", false);
                        objectPool.Despawn(this.gameObject);
                    }
                    else Debug.LogError("ERR");
                    //else
                    //    Destroy(gameObject);
                }
            }
            else
            {
                // Destroy this platform if sound has finished
                if (!audioSource.isPlaying)
                {
                    ObjectPool objectPool = GetComponentInParent<ObjectPool>();

                    if (objectPool != null)
                        objectPool.Despawn(this.gameObject);
                }
            }
        }
    }

	void OnCollisionEnter2D(Collision2D Other)
    {
        // Add force when player fall from top
        if (Other.relativeVelocity.y <= 0f)
        {
            Rigidbody2D Rigid = Other.rigidbody;


            if (Rigid != null)
            {
                Vector2 Force = Rigid.velocity;
                Force.y = Jump_Force;
                Rigid.velocity = Force;

                // Play jump sound
                audioSource.Play();

                // if gameobject has animation; Like spring, trampoline and etc...
                if (animator != null)
                    animator.SetBool("Active", true);

                // Check platform type
                Platform_Type();
            }

            //Check Trampoline => rotate player 360 deg
            if (this.name == "Trampoline(Clone)")
            {
                Player_Controller player = Other.gameObject.GetComponent<Player_Controller>();

                if (player != null)
                {
                    player.PlayerRotate360(1.5f, 360);
                }
            }

            //bounce the "Platform_Brown(Clone)" 
            if (name == "Platform_Green(Clone)")
            {
                transform.DOMoveY(transform.position.y - 0.3f, 0.2f).
                    OnComplete( ()=> transform.DOMoveY(transform.position.y + 0.3f, 0.2f) );
            }
        }
    }

    void Platform_Type()
    {
        if (TryGetComponent(out Platform_White c))
            c.Deactive();
        else if (TryGetComponent(out Platform_Brown d))
            d.Deactive();
    }
}

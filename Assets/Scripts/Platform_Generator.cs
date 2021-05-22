using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDeveloper.AdvancedObjectPool;

public class Platform_Generator : MonoBehaviour {

    public ObjectPool Platform_Green;
    public ObjectPool Platform_Blue;
    public ObjectPool Platform_White;
    public ObjectPool Platform_Brown;

    public ObjectPool Spring;
    public ObjectPool Trampoline;
    public ObjectPool Propeller;

    private GameObject Platform;
    private GameObject Random_Object;

    public float Current_Y = 0;
    float Offset;
    Vector3 Top_Left;

	// Use this for initialization
	void Start () 
    {
        // Initialize boundary
        Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Offset = 1.2f;

        // Initialize platforms
        Generate_Platform(10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate_Platform(int Num)
    {
        for (int i = 0; i < Num; i++)
        {
            // Calculate platform x, y
            float Dist_X = Random.Range(Top_Left.x + Offset, -Top_Left.x - Offset);
            float Dist_Y = Random.Range(2f, 5f);

            // Create brown platform random with 1/8 probability
            int Rand_BrownPlatform = Random.Range(1, 8);

            if (Rand_BrownPlatform == 1)
            {
                float Brown_DistX = Random.Range(Top_Left.x + Offset, -Top_Left.x - Offset);
                float Brown_DistY = Random.Range(Current_Y + 1, Current_Y + Dist_Y - 1);
                Vector3 BrownPlatform_Pos = new Vector3(Brown_DistX, Brown_DistY, 0);

                Platform_Brown.Spawn(BrownPlatform_Pos, Quaternion.identity , Platform_Brown.transform);
            }

            // Create other platform
            Current_Y += Dist_Y;
            Vector3 Platform_Pos = new Vector3(Dist_X, Current_Y, 0);
            int Rand_Platform = Random.Range(1, 10);
            
            if (Rand_Platform == 1) // Create blue platform
                Platform = Platform_Blue.Spawn(Platform_Pos, Quaternion.identity, Platform_Blue.transform);
            else if (Rand_Platform == 2) // Create white platform
                Platform = Platform_White.Spawn(Platform_Pos, Quaternion.identity, Platform_White.transform);
            else // Create green platform
                Platform = Platform_Green.Spawn(Platform_Pos, Quaternion.identity, Platform_Green.transform);

            //Platform.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);

            if (Rand_Platform != 1)
            {
                // Create random objects; like spring, trampoline and etc...
                int Rand_Object = Random.Range(1, 40);

                if (Rand_Object == 4) // Create spring
                {
                    Vector3 Spring_Pos = new Vector3(Platform_Pos.x + 0.5f, Platform_Pos.y + 0.27f, 0);
                    Random_Object = Spring.Spawn(Spring_Pos, Quaternion.identity, Spring.transform);
                    
                    // Set parent to object
                    //Random_Object.transform.parent = Platform.transform;
                }
                else if (Rand_Object == 7) // Create trampoline
                {
                    Vector3 Trampoline_Pos = new Vector3(Platform_Pos.x + 0.13f, Platform_Pos.y + 0.25f, 0);
                    Random_Object = Trampoline.Spawn(Trampoline_Pos, Quaternion.identity, Trampoline.transform);

                    // Set parent to object
                    //Random_Object.transform.parent = Platform.transform;
                }
                else if (Rand_Object == 15) // Create propeller
                {
                    Vector3 Propeller_Pos = new Vector3(Platform_Pos.x + 0.13f, Platform_Pos.y + 0.15f, 0);
                    Random_Object = Propeller.Spawn( Propeller_Pos, Quaternion.identity, Propeller.transform);

                    // Set parent to object
                    //Random_Object.transform.parent = Platform.transform;
                }
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public Transform Target;
    
    [SerializeField] private Game_Controller Game_Controller;
    private bool Game_Over = false;

    [SerializeField] private float Time_ToDown = 0;

    

    // Update is called once per frame
    void Update()
    {
        // Check game over
        Game_Over = Game_Controller.Get_GameOver();
    }

    void FixedUpdate()
    {
        // Move camera to down if game over
        if (Game_Over)
        {
            if(Time.time < Time_ToDown + 4f)
                transform.position -= Vector3.up;
            else
            {
                // Delete player and all objects
                //GameObject Player = GameObject.FindGameObjectWithTag("Player");
                GameObject[] Objects = GameObject.FindGameObjectsWithTag("Object");

                //Destroy(Player);
                if(Target)
                    Destroy(Target.gameObject);
                foreach (GameObject Obj in Objects)
                    Destroy(Obj);
            }
        }
    }

	void LateUpdate () 
    {
        if(!Game_Over)
        {
            // if target.y > camera.y + 2
            if (Target.position.y > transform.position.y + 2)
            {
                Vector3 New_Pos = new Vector3(transform.position.x, Target.position.y - 2, transform.position.z);
                transform.position = New_Pos;
            }

            Time_ToDown = Time.time;
        }
	}
}

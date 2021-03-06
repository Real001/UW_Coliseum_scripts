﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_like_jager : MonoBehaviour 
{
	[SerializeField]
	float rotation_speed = 200f;

	private UnityEngine.AI.NavMeshAgent agent;
	private RaycastHit hit;
	private Vector3 Look_target = new Vector3();
	private Vector3 LastLook_target = new Vector3();
	private Vector3 dir;
	private float view_angle;
    private int Health = 3;

    void Start() 
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update() 
	{
		if (Input.GetMouseButtonDown(1)) 
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) 
			{
				agent.destination = hit.point;
				Look_target = hit.point;
			}
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) 
		{
			Look_target = hit.point;
		}
		LookHere ();
        if (Health == 0)
            Destroy(this.gameObject);
    }

	void LookHere()
	{
		if (Look_target != LastLook_target)
		{
			CalculateAngle(Look_target);
			if(view_angle > 3)
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotation_speed * UnityEngine.Time.deltaTime);
		}
	}

	private void CalculateAngle(Vector3 temp)
	{
		dir = new Vector3(temp.x, transform.position.y, temp.z) - transform.position;
		view_angle = Vector3.Angle(dir, transform.forward);
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "throwed_spire")
            Health--;
        if (other.gameObject.name == "Weapon")
            Destroy(this.gameObject);
    }
}

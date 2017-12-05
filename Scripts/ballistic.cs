﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* This Script defines the ballistic
 * In this game. all attaction (skills included) will act via ballistic system
 * the ballistic will destory itself when get touched with enemy/boundary/after 2 seconds.
 * if target is an enemy, a stun animaion and a forceback will be applied.
 */
public class ballistic : MonoBehaviour {
    public GameObject From;
    public float speed;
    public float damage;
    public float side = -1;

	// Use this for initialization
	void Start () { 
        Destroy(this.gameObject, 2.0f);
	}
	
    void OnTriggerEnter(Collider colli)
    {
        // attacktion dont work on same team
        string enemytag;
        if (this.gameObject.tag == "Team0")
            enemytag = "Team1";
        else
            enemytag = "Team0";
        GameObject t = colli.gameObject;
        if (t.tag == enemytag)
        {
            Destroy(this.gameObject);
            // apply stun and forceback to target
            if(t.tag != "Team0" && t.name != "Boss")
            {
                t.GetComponent<attribute>().ForceBackDerection = transform.forward;
                t.GetComponent<attribute>().ForceBackCounter = t.GetComponent<attribute>().ForceBackCounterMax;
                t.GetComponent<Animator>().SetTrigger("stun");
            }
            if (t.GetComponent<attribute>().update_HP(Mathf.Min( t.GetComponent<attribute>().DEF - damage,0)) <= 0)
            {
                From.GetComponent<attribute>().update_EXP(t.GetComponent<attribute>().DropEXP);
                From.GetComponent<attribute>().gold += t.GetComponent<attribute>().DropGold;
            }
        }
    }
    void OnTriggerExit(Collider colli)
    {
        GameObject t = colli.gameObject;
        if (t.tag == "BallisticBoundary")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
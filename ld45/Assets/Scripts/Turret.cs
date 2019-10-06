﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Cell
{
    // Start is called before the first frame update
    public float range;
    public int damage;
    public int damageSpeed;
    private int passed;
    public float timeGap = 0.5f;
    bool switch1 = true;


    //Finding a target by finding the nearest object with the tag ENEMY
    private GameObject GetTarget()
    {
       
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        
        GameObject lastObject = gameObject;
        float distance = Mathf.Infinity;
        foreach (GameObject x in objects)
        {
            Vector3 diff = x.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
            float dist = diff.sqrMagnitude;
            if (dist < distance)
            {
                lastObject = x;
                distance = dist;
            }
        }
        return lastObject;
    }

    public override void onImpulse()
    {
        Debug.Log("wrk");
        StartCoroutine(initiateShooting());
    }



    private void Shoot()
    {
        GameObject Target = GetTarget();
        Debug.Log("Searching");

        Vector2 dist =  Target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        // Ważne
        //if (Target!= gameObject ) Rotate(dist);


        if (dist.sqrMagnitude < range && Target != gameObject)
        {
            Debug.Log("One frame, one kill");
            Destroy(Target);
            DrawArrow.ForDebug(gameObject.GetComponent<Transform>().position, dist);
        }
    }
    private IEnumerator initiateShooting()
    {
        // warunek - odległóść
        if(GetTarget()!=gameObject)
        {
            
        }
        
        StartCoroutine(animate());
        switch1 = false;
        yield return new WaitForSeconds(timeGap);
        Shoot();
    }
    private IEnumerator animate()
    {
        Transform ch;
        ch = GetComponentsInChildren<Transform>()[1];

        Vector3 origin = ch.right;
        Vector3 target = (GetTarget().GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position);
        target.z = origin.z = 0;
        origin = Vector3.Normalize(origin);
        target = Vector3.Normalize(target);
        float time = 0;
        
        while (time<timeGap)
        {
            foreach (Transform trans in GetComponentsInChildren<Transform>())
            {
                if (trans.name != "TurretBase")
                {
                    trans.right = -target*(time/timeGap)+origin*(1-time/timeGap);

                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    private void Awake()
    {
        setPulseAction(action);
    }


    private void Rotate(Vector2 Vect2)
    {
        
        //Determining the rotation and rotating
        //float RotAngle = Vector2.Angle(Vector2.up,Vect2);
        foreach (Transform trans in GetComponentsInChildren<Transform>())
        {
            if (trans.name != "TurretBase")
            {
                trans.right =-GetTarget().GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
                //trans.RotateAround(Vector3.forward, RotAngle);
                //trans.rotation = Quaternion.Euler(0, 0, RotAngle-90);


            }
        }
       



    }


}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //These are the neighbouring tiles
    public Cell right = null , lup = null , ldown = null, left = null,rup = null,rdown = null;
    
    public void Instantiate(Vector2Int p)
    {
        pos = p;
        transform.position = new Vector2(-(pos.y%2)*0.5f+pos.x,pos.y*Mathf.Sqrt(3)*0.5f);
    }

    //This is on only ONCE per energy cycle. Used for singe-time actions
    public bool active = true;

    //This determines the energy of the tile
    public bool isActivated = false;

    public int timesActivated = 0;

    public SpriteRenderer renderer;

    public Vector2Int pos;

    [SerializeField] private int hp = 100;


    public void InstantiateCell(Vector2Int p)
    {
        pos = p;
        transform.localPosition = (Vector2)pos;
    }
    public int getHp()
    {
        return hp;
    }
    public void setHp(int newHp)
    {
        hp = newHp;
    }

    public void dealDamage(int damage)
    {
        if (damage > 0)
        {
            hp -= damage;
        }

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }


    /// Functions
    /*public IEnumerator animate(int duration)
    {
        Color color = new Color(1, 1, 1);
        float t = 0;
        while(t<duration)
        {
            color.r = 1 * (t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }*/

    public void getImpulse(Cell parent)
    {
        // Debug.Log("from getImpulse");
        if(parent.timesActivated>timesActivated)
        {
            //Debug.Log("got impulse");
            isActivated = true;
            WhenActivatedDoOnce();
            timesActivated = parent.timesActivated;
            StartCoroutine(propagateImpuls());
            
        }
    }
    public virtual void WhenActivatedDoOnce()
    {
    }


    // coroutine
    public IEnumerator propagateImpuls()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log($"inside propagate impulse, activated{timesActivated}");
        yield return new WaitForSeconds(0.5f);
        if(right!=null)
        {
            right.getImpulse(this);
        }
        if (left != null)
        {
            left.getImpulse(this);
        }
        if (rup != null)
        {
            rup.getImpulse(this);
        }
        if (rdown != null)
        {
            rdown.getImpulse(this);
        }
        if (lup != null)
        {
            lup.getImpulse(this);
        }
        if (ldown != null)
        {
            ldown.getImpulse(this);
        }
        isActivated = false;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        //GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log("change color");
    }

    private void Update()
    {

    }

    // Update is called once per frame

    public void FixedUpdate()
    {
        //Setting sprite accordingly to cell's activation state
        //if (isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteDeactivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteActivated;
        //if (!isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteActivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteDeactivated;
    }


    //This will ensure that this GameObject is at coordinates expressed in Int values
    public void SnapToIntPosition()
    {
        Vector2 SnappedPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        transform.position = SnappedPosition;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolPathBehaviour : StateMachineBehaviour
{
    public float speed = 1;
    public bool isLoop;
    private bool inReverse;
    public List<Transform> PathNodes;
    public Transform nextPos;
    private GameObject thisObject;
    private int i;
    [Header("This is determining if the sprite for your enemy is facing the right or left")]
    public bool isEnemySpriteFacingRight = true;

    private EnemyAnimController cont;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    //
    public Vector3 lastPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisObject = animator.gameObject;
        Transform paths = thisObject.transform.parent.Find("Paths"); // load in an empty named Paths that contains transforms
        foreach (Transform path in paths)
        {
            PathNodes.Add(path); // add every path to our list
        }
        nextPos = PathNodes[0]; // the first target is the first transform
        cont = animator.gameObject.GetComponent<EnemyAnimController>();
        lastPosition = cont.returnPosition;

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navigate();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        lastPosition = animator.gameObject.transform.position;
        cont.returnPosition = lastPosition;
        Debug.Log("Last Position (Path) Saved " + lastPosition);
    }

    private void setNextNode()
    {
        nextPos = PathNodes[i]; // set the next position
        if (i < PathNodes.Count - 1) // if we are within bounds and going forwards
        {
            if (!inReverse)
            {
                i++; // increment the iterator for the next time this function is called
            }
            else 
            {
                i--; // decrement the iterator for the next time this function is called
                if (i == 0)
                {
                    inReverse = false;
                }
            }
        }
        else
        {
            if (isLoop)
            {
                i = 0;
            }
            else 
            {
                inReverse = true;
                i --;
            }
        }
    }

    private void navigate()
    {
        if (cont.GetState() == 1)
        {
            if (thisObject.transform.position == nextPos.position) // if we have reached this position
            {
                setNextNode(); // set the next position
            }

            float step = speed * Time.deltaTime;
            thisObject.transform.position = Vector3.MoveTowards(thisObject.transform.position, nextPos.position, step); // move towards the next position
            float temp = isEnemySpriteFacingRight ? -1 : 1;
            if (thisObject.transform.position.x > nextPos.position.x)
            {
                thisObject.transform.localScale = new Vector3(Mathf.Abs(thisObject.transform.localScale.x) * temp, thisObject.transform.localScale.y);
            }
            else
            {
                thisObject.transform.localScale = new Vector3(Mathf.Abs(thisObject.transform.localScale.x) * temp * -1, thisObject.transform.localScale.y);
            }
        }
        else //Return only since chase will auto change behavior
        {
            float step = speed * Time.deltaTime;
            thisObject.transform.position = Vector3.MoveTowards(thisObject.transform.position, lastPosition, step); // move towards the return position
            float temp = isEnemySpriteFacingRight ? -1 : 1;
            if (thisObject.transform.position.x > nextPos.position.x)
            {
                thisObject.transform.localScale = new Vector3(Mathf.Abs(thisObject.transform.localScale.x) * temp, thisObject.transform.localScale.y);
            }
            else
            {
                thisObject.transform.localScale = new Vector3(Mathf.Abs(thisObject.transform.localScale.x) * temp * -1, thisObject.transform.localScale.y);
            }

            if (thisObject.transform.position == lastPosition)
            {
                cont.SetStateNormal();
            }
        }
        

    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Puzzle : MonoBehaviour
{

    private Animator anim;

    // essentially acts as a fixed-size stack
    private Stack<Action> actions = new Stack<Action>();



    private void Awake()
    {
        
        anim = GetComponent<Animator>();

    }



    // triggers the current action in the queue
    public void DoAction(int maximumActions)
    {

        if(actions.Count > 0)
        {
            if(actions.Pop().GetType() == typeof(DoNothing)) anim.SetTrigger("Fall");

            actions.Push(new DoNothing());
        }

        actions.Push(new Fall(anim));

    }



    public void UndoAction()
    {

        int index = actions.Count - 1;

        if(index >= 0)
        {
            actions.Pop().UndoAction();
        }

    }

}
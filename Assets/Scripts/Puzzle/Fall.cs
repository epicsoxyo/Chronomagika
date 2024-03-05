using UnityEngine;

public class Fall : Action
{

    public Fall(Animator anim)
    {

        _anim = anim;

    }



    public override void UndoAction()
    {

        _anim.SetTrigger("Raise");

    }

}
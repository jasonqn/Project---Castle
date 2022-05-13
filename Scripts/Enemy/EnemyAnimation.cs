using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator anim;

    public void EnemyAttack()
    {
        anim.SetTrigger("attack");
    }

    public void EnemyDie()
    {
        anim.SetBool("dead",true);
    }

}

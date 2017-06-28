using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrikeStateBehavior : FighterStateBehavior
{
    [SerializeField] GameObject FireAttack;
    override public void OnStateEnter(Animator animator,
                                      AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        float fighterX = fighter.oponent.transform.position.x;
        float fighterZ = fighter.oponent.transform.position.z;
        GameObject instance = Object.Instantiate(
            FireAttack,
            new Vector3(fighterX, 1, fighterZ),
            Quaternion.Euler(0, 0, 0)
            ) as GameObject;

        DigitalRuby.PyroParticles.FireCollisionForwardScript FireStrike = instance.GetComponent<DigitalRuby.PyroParticles.FireCollisionForwardScript>();
        FireStrike.caster = fighter;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenStateBehavior : FighterStateBehavior
{
    [SerializeField] GameObject Hadoken;
    [SerializeField] float size;
    override public void OnStateEnter(Animator animator,
                                      AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateEnter(animator, stateInfo, layerIndex);
        float fighterX = fighter.transform.position.x;
       // Debug.Log(fighter.name);
        GameObject instance = Object.Instantiate(
            Hadoken,
                new Vector3(fighterX, fighter.transform.position.y+1, fighter.transform.position.z),
                Quaternion.Euler(0, fighter.transform.localRotation.y, 0)
                ) as GameObject;
        
        instance.transform.localScale = new Vector3(fighter.transform.localScale.x * size, fighter.transform.localScale.y*size, fighter.transform.localScale.z*size);
        instance.transform.localRotation = new Quaternion(fighter.transform.localRotation.x, fighter.transform.localRotation.y, fighter.transform.localRotation.z, fighter.transform.localRotation.w);
        DigitalRuby.PyroParticles.FireCollisionForwardScript hadokenScript = instance.GetComponentInChildren<DigitalRuby.PyroParticles.FireCollisionForwardScript>();
        hadokenScript.caster = fighter;
        
    }

}
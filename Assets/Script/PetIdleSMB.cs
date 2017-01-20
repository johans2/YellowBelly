using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

public class PetIdleSMB : StateMachineBehaviour {


    float checkInterval = 2f;
    float currentTime;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currentTime += Time.deltaTime;
        
        if(currentTime > checkInterval) {
            currentTime = 0;

            if(Random.Range(0,100) > 50) {
                animator.SetTrigger("Mouth");
            }



        }


    }
}

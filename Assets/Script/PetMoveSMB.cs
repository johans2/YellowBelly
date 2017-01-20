using UnityEngine;
using System.Collections;
using RGCommon;


public class PetMoveSMB : StateMachineBehaviour {

    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(!agent.hasPath) {
            animator.SetBool("Move", false);
            animator.SetBool("Idle", true);
        }


    }


}

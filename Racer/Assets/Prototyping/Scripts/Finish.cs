using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public FinishUI finishController;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            finishController.StartCoroutine(finishController.FinishSequence());
        }
    }
}

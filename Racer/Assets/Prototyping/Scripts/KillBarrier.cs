using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{

    public Transform player;
    public Transform respawnPosition;


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            player.position = respawnPosition.position;
        }
    }

}

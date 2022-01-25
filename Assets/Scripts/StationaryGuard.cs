using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryGuard : MonoBehaviour
{
    

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    public Transform player;

    public float viewAngle;
    
    Color originalColor;

    void Start() {
        originalColor = spotlight.color;
    }

    void Update() {
        if(this.canSeePlayer()) {
            spotlight.color = Color.red;
            GuardController.hasBeenCaught = true;
        }
        else {
            spotlight.color = originalColor;
        }
    }


    bool canSeePlayer() {
        if(Vector3.Distance(transform.position, player.position) < viewDistance) {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardandPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBetweenGuardandPlayer < viewAngle/2f) {
                if(! Physics.Linecast(transform.position, player.position, viewMask)) {
                    return true;
                }


            }
        }
        return false;
    }
}

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider))]
public class Can_Pickup : MonoBehaviour {
    public int isBeingHeld = 0;
    void OnDestroy() {
        PickUp_Vive.destroySelected = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if(GetComponent<Collider>().GetType() == typeof(MeshCollider)) {
            Gizmos.DrawWireMesh(GetComponent<MeshCollider>().sharedMesh, this.transform.position, this.transform.rotation, this.transform.lossyScale);
        }
        else if(GetComponent<Collider>().GetType() == typeof(BoxCollider)) {
            Gizmos.DrawWireCube(GetComponent<BoxCollider>().center + transform.position, GetComponent<BoxCollider>().size);

        }
        else if(GetComponent<Collider>().GetType() == typeof(SphereCollider)) {
            Gizmos.DrawWireSphere(GetComponent<SphereCollider>().center, GetComponent<SphereCollider>().radius);
        }
    }
}
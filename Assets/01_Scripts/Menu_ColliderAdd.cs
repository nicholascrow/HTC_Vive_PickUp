using UnityEngine;
using UnityEditor;

public class Menu_ColliderAdd : MonoBehaviour {

    [MenuItem("Nick's Tools/Add Colliders")]
    static void AddColliders() {
        if(Selection.activeGameObject.GetComponent<Collider>() == null) {
           // print(Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.name);
        }

        for(int i = 0; i < Selection.activeGameObject.GetComponentsInChildren<MeshFilter>().Length; i++) {
            print(Selection.activeGameObject.GetComponentsInChildren<MeshFilter>()[i].sharedMesh.name);
            Selection.activeGameObject.GetComponentsInChildren<MeshFilter>()[i].gameObject.AddComponent<MeshCollider>();
            /*if(Selection.activeGameObject.GetComponent<Collider>() == null) {
                Selection.activeGameObject.GetComponentsInChildren<MeshFilter>()[i].gameObject.AddComponent<MeshCollider>();
            }*/
        }
    }

    [MenuItem("Nick's Tools/Fit Collider")]
    static void FitCollider() {
      //  Vector3 change = Selection.activeGameObject.GetComponent<BoxCollider>().size;
        if(Selection.activeGameObject.GetComponent<Collider>() != null && Selection.activeGameObject.GetComponent<Collider>().GetType() == typeof(BoxCollider)) {
          //  while(!Selection.activeGameObject.GetComponent<BoxCollider>().bounds.Contains(new Vector3(change.x - 1, change.y, change.z))) {
            //    Selection.activeGameObject.GetComponent<BoxCollider>().size = new Vector3(change.x - 1, change.y, change.z);
           // }
        }
    }


}

using UnityEngine;
using System.Collections;

public class GrabDelegate : MonoBehaviour {
    public delegate void ControllerEventHandler(GameObject controller);

    public static event ControllerEventHandler onGrabbable;
    public static event ControllerEventHandler onGrabbed;
    public static event ControllerEventHandler onDrop;

    public static void onGrabbableItem(GameObject controller) {
        if(onGrabbable != null) {
            onGrabbable(controller);
        }
    }
    public static void onGrabbedItem(GameObject controller) {
        if(onGrabbed != null) {
            onGrabbed(controller);
        }
    }
    public static void onDropItem(GameObject controller) {
        if(onDrop != null) {
            onDrop(controller);
        }
    }
}

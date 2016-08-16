using UnityEngine;
using System.Collections;

public class PickUp_Vive : MonoBehaviour {

    //the controllers
    SteamVR_TrackedObject trackedObj;

    //the joint to keep this object attached to the controller
    FixedJoint joint;

    //object that can be selected
    GameObject selectableObj;

    //object that is currently selected
    GameObject selectedObj;

    public Rigidbody attachPT;

    //should we destroy this object?
    public static bool destroySelected = false;

    public float scalething = 10;
    //when the scene starts
    void Awake() {
        Physics.gravity = new Vector3(0,(float) (-1 *(9.81 * (scalething / 2))),0);
        //we can't select anything until the controllers are tracked
        selectableObj = null;
        selectedObj = null;

        //controller tracking
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        
        //get the controllers
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        //break the joint if the object we are holding is destroyed
        if(destroySelected && joint != null) {
            //destroy the joint
            Object.DestroyImmediate(joint);

            //set it to null
            joint = null;
        }

        //if we can select an object then grab it.
        if(joint == null && selectableObj != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip)) {
            AddJoint(selectableObj);
        }

        //if we are holding an object lets stop holding it
        else if(joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip)) {
            RemoveJoint(selectedObj);
        }
    }

    /// <summary>
    /// When the controller enters an object, we need to mark it so we can grab it. 
    /// </summary>
    /// <param name="collision">The object we can grab.</param>
    /// 
    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.GetComponent<Can_Pickup>()
        && !collision.gameObject.GetComponent<Can_Pickup>().isBeingHeld) {
            setGrabbable(collision.gameObject);
        }
    }

    /// <summary>
    /// When the controller leaves the object.
    /// </summary>
    /// <param name="collisionInfo">The object left.</param>
    void OnTriggerExit(Collider collisionInfo) {
        selectableObj = null;
    }

    /// <summary>
    /// This method adds a joint between the 2 objects (this object and the one we are grabbing)
    /// </summary>
    void AddJoint(GameObject addJointTo) {
        selectedObj = addJointTo;
      // addJointTo.GetComponent<Can_Pickup>().isBeingHeld++;
        joint = addJointTo.AddComponent<FixedJoint>();
        joint.connectedBody = attachPT;
    }

    /// <summary>
    /// Removes the joint between the 2 objects.
    /// </summary>
    void RemoveJoint(GameObject removeJointFrom) {
        
        //is the object we want to grab being held still
       // removeJointFrom.GetComponent<Can_Pickup>().isBeingHeld--;

        //the controller
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        // the rigidbody on the joint
        Rigidbody r = removeJointFrom.GetComponent<Rigidbody>();//joint.gameObject.GetComponent<Rigidbody>();

        //destroy the join
        Object.DestroyImmediate(joint);

        //set it to null
        joint = null;

        //selected object is now null
        selectedObj = null;

        //the next part applies physics.
         var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

       // var origin = trackedObj.transform;
        if(origin != null) {
            r.velocity = origin.TransformVector(device.velocity);
            r.angularVelocity = origin.TransformVector(device.angularVelocity * .1f);
           
        }
        else {
            r.velocity = device.velocity;
            r.angularVelocity = device.angularVelocity * .1f;
        }

        r.maxAngularVelocity = r.angularVelocity.magnitude;
    }

    public void setGrabbable(GameObject collidedObject) {
        selectableObj = collidedObject;
    }
    

    void Start() {
        GrabDelegate.onGrabbable += this.setGrabbable;
        GrabDelegate.onGrabbed += this.AddJoint;
        GrabDelegate.onDrop += this.RemoveJoint;
    }
    

}

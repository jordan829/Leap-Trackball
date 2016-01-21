using UnityEngine;
using Leap;

public class LeapControled : MonoBehaviour
{
    Controller controller;

    Hand mainHand;

    Vector3 currPosition;
    Vector3 prevPosition;
    Vector3 handDelta;

    // called when the script instance is being loaded.
    void Awake()
    {
        controller = new Controller();
    }

    void Update()
    {
        mainHand = controller.Frame().Hands.Frontmost;

        currPosition = mainHand.PalmPosition.ToUnityScaled();

        transform.gameObject.GetComponent<Renderer>().material.color = Color.white;

        if (mainHand.PinchStrength == 1.0)
        {
            transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
            handDelta = currPosition - prevPosition;
            handDelta *= 1000;

            transform.rotation = Quaternion.Euler(-handDelta.y, -handDelta.x, 0) * transform.rotation;
        }

        prevPosition = currPosition;
    }
}
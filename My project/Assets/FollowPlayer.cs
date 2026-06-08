using UnityEngine;

public class FollowPlayerForward : MonoBehaviour
{
    public Transform player;

    private PickupLamp pickupLamp;

    void Start()
    {
        pickupLamp = GetComponent<PickupLamp>();
    }

    void Update()
    {
        if (pickupLamp == null)
            return;

        if (!pickupLamp.pickedUp)
            return;

        transform.forward = player.forward;
    }
}
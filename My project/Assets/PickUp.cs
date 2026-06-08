using UnityEngine;

public class PickupLamp : MonoBehaviour
{
    public Transform holdPoint;

    private bool canInteract = false;
    public bool pickedUp = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // 記錄初始位置
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (!pickedUp)
            {
                PickUp();
            }
            else
            {
                Drop();
            }
        }
    }

    void PickUp()
    {
        pickedUp = true;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Debug.Log("撿起燈");
    }

    void Drop()
    {
        pickedUp = false;

        transform.SetParent(null);

        transform.position =holdPoint.position + holdPoint.forward * 0.5f;

        transform.rotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Debug.Log("放下燈");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
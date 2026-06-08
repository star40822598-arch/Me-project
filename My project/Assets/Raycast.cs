using UnityEngine;

public class LampRaycaster : MonoBehaviour
{
    public float distance = 10f;

    void Update()
    {
        Ray ray =
            new Ray(
                transform.position,
                transform.forward
            );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            LightTarget target =
                hit.collider.GetComponent<LightTarget>();

            if (target != null)
            {
                target.Activate();
            }
        }
    }
}
using UnityEngine;

public class LightTarget : MonoBehaviour
{
    public bool activated = false;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Activate()
    {
        if (activated) return;

        activated = true;

        // 變白色
        rend.material.color = Color.white;
    }
}
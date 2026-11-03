using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private InteractionCanvasScript interactionCanvasScript;


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            interactionCanvasScript.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            interactionCanvasScript.Hide();
        }
    }
}

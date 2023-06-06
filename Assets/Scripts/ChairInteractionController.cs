using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInteractionController : MonoBehaviour , IInteractable
{
    Collider m_Collider;

    public void Interact()
    {
        m_Collider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

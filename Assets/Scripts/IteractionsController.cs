using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using StarterAssets;


interface IInteractable {
    public void Interact();
}

public class IteractionsController : NetworkBehaviour
{
    public Transform IteractorSource;
    public float iteractionRange;
    
    Animator animator;
    Collider m_Collider;

    PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(movementScript.isSitted == false)
            {
                Ray ray = new Ray(IteractorSource.position, IteractorSource.forward);
                if(Physics.Raycast(ray, out RaycastHit hitInfo, iteractionRange))
                {
                    if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactionObj))
                    {
                        //interactionObj.Interact();
                        animator.SetBool("Stand", false);
                        animator.SetBool("Sit", true);
                        m_Collider.enabled = false;
                        movementScript.isSitted = true;
                        gameObject.transform.position = new Vector3(
                            hitInfo.collider.gameObject.transform.position.x,
                            hitInfo.collider.gameObject.transform.position.y + 0.2f,
                            hitInfo.collider.gameObject.transform.position.z
                        );
                        transform.rotation = Quaternion.LookRotation(hitInfo.collider.gameObject.transform.forward);
                        //transform.forward = hitInfo.collider.gameObject.transform.forward;
                        //Debug.Log(transform);

                    }
                }
            } 
            else 
            {
                animator.SetBool("Sit", false);
                animator.SetBool("Stand", true);
                var currentPosition = gameObject.transform.position;
                var currentForward = gameObject.transform.forward;
                var newPosition = currentPosition - currentForward;
                gameObject.transform.position = newPosition;
                m_Collider.enabled = true;
                movementScript.isSitted = false;
            }
        }
    }
}

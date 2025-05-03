using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public GameObject uiPrompt;
    public float rayDistance = 3f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (uiPrompt != null)
            uiPrompt.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Door")) 
            {
                uiPrompt.SetActive(true);

           
                return;
            }
        }

        uiPrompt.SetActive(false);
    }
}

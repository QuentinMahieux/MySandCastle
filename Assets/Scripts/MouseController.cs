using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        // Clic droit = bouton 1
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                hit.collider.gameObject.GetComponent<DefaultBlock>().PlaceBlock();
            }

        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                hit.collider.gameObject.GetComponent<DefaultBlock>().RemoveBlock();
            }

        }
    }
}

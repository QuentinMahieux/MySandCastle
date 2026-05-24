using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;

    private Camera _cam;
    private VisualInterraction lastInteraction;

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
                hit.collider.gameObject.GetComponent<DefaultBlock>().ClickRight();
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                hit.collider.gameObject.GetComponent<DefaultBlock>().ClickLeft();
            }

        }

        Ray _ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, maxDistance))
        {
            VisualInterraction newInteraction = _hit.transform.gameObject.GetComponent<VisualInterraction>();
            if (newInteraction != lastInteraction && lastInteraction)
            {
                lastInteraction.DesactiveOutLine();
            }

            lastInteraction = newInteraction;
            lastInteraction.ActiveOutLine();
        }
    
    }
}

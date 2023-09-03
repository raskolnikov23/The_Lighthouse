using UnityEngine;

public class Raycaster : MonoBehaviour
{
    Camera cam;
	public LayerMask layerMask;
    public RayData rayData;


	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		RayCaster();
	}

    void RayCaster()
    {
        RaycastHit hit = new();

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            rayData.inFocus = true;
            rayData.lookingOnObject = hit.transform.gameObject;
            rayData.lookingOn = rayData.lookingOnObject.name;
            rayData.distanceBetween = hit.distance;
        }

        else
        {
            rayData.lookingOn = null;
            rayData.inFocus = false;
        }
    }
}

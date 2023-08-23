using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
	[SerializeField]
	public bool showName;
	public TextMeshProUGUI text;
	Camera cam;
	public string lookingOn;
	public GameObject lookingOnObject;
	public LayerMask layerMask;
	public float distanceBetween;
	public bool inFocus;



	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		RayCaster();

		if (text != null)
		{
			text.text = lookingOn;
			text.text += $"\n Distance: {distanceBetween}";

            if (showName == true && inFocus == true)
            {
                text.enabled = true;
            }
            else
            {
                text.enabled = false;
            }
        }



	void RayCaster()
	{
		RaycastHit hit = new();


		if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			inFocus = true;
 
			lookingOnObject = hit.transform.gameObject;
			lookingOn = lookingOnObject.name;
			distanceBetween = hit.distance;

        }
		else
		{
			lookingOn = null;
			inFocus = false;
		}
	}
	
	




	}
}

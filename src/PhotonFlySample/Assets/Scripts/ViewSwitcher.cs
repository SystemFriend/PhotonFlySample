using UnityEngine;
using System.Collections;

public class ViewSwitcher : MonoBehaviour {

    [HideInInspector]
    public GameObject myCaracter = null;
    public Vector3 myCameraPosition = Vector3.zero;
    public KeyCode switchKey = KeyCode.Space;
    private Vector3 InitialCameraPosition { get; set; }
    private Quaternion InitialCameraRotation { get; set; }
    private bool IsWorldView { get; set; }

    // Use this for initialization
    void Start () {
        this.InitialCameraPosition = Camera.main.transform.position;
        this.InitialCameraRotation = Camera.main.transform.rotation;
        this.IsWorldView = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(this.switchKey) && (this.myCaracter != null))
        {
            this.SetCameraPosition(!this.IsWorldView);
        }
	}

    public void SwitchCameraToWorld()
    {
        this.SetCameraPosition(true);
    }

    public void SwitchCameraToLocal()
    {
        this.SetCameraPosition(false);
    }

    public void SetCameraPosition(bool setToWorld)
    {
        if (this.myCaracter != null)
        {
            var mainCamera = Camera.main;
            if (!setToWorld)
            {
                mainCamera.transform.parent = this.myCaracter.transform;
                mainCamera.transform.localPosition = this.myCameraPosition;
                mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                mainCamera.transform.parent = null;
                mainCamera.transform.position = this.InitialCameraPosition;
                mainCamera.transform.rotation = this.InitialCameraRotation;
            }
            this.IsWorldView = setToWorld;
        }
    }
}

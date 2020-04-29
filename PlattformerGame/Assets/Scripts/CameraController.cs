using UnityEngine;

public class CameraController : MonoBehaviour

{
    [Header("Objects used by the camera")]
    [Tooltip("Use the pivot prefab or a pivot")]
    [SerializeField] private Transform pivot;
    [Tooltip("Target that the camera will follow")]
    [SerializeField] private Transform target;

    [Header("Camera Offset Values")]
    [Tooltip("Use to change the offset values")]
    [SerializeField] private Vector3 offset;
    [Tooltip("Check to use default offset values")]
    [SerializeField] private bool defaultOffsetValues;

    [Header("Camera view values for X,Y Axis")]
    [Tooltip("Adjusts the camera rotation of the X-Axixs")]
    [Range(1, 10)]
    [SerializeField] private float rotationSpeed;
    [Tooltip("Adjusts the values of the Y-Axis maximum view angle")]
    [SerializeField] private float maxViewAngle;
    [Tooltip("Adjusts the values of the Y-Axis minimum view angle")]
    [SerializeField] private float minViewAngle;
    [Tooltip("Check to revert the Y-Axis")]
    [SerializeField] private bool invertYaxis;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        CamOffset();
    }


    public void Update()
    {

        CamRotation();
        CamTarget();
    }

    public void CamOffset()
    {
        if (defaultOffsetValues)
        {
            offset = target.position - transform.position;
        }
    }

    public void CamRotation()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        target.Rotate(0, horizontal, 0);

       
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;

        if (invertYaxis)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }



        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }


        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

    }
    public void CamTarget()
    {
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }

        transform.LookAt(target);
    }

}
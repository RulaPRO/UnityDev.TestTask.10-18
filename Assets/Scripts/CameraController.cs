using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _offset;
    public float MinRotation = -80f; // ограничение вращения по Y
    public float MaxRotation = 5f;   // ограничение вращения по Y
    public float Zoom = 0.25f;       // чувствительность при увеличении, колесиком мышки
    public float ZoomMax = 10f;      // макс. увеличение
    public float ZoomMin = 3f;       // мин. увеличение
    private float _sensitivity;      // чувствительность мышки
    public float Sensitivity
    {
        set { _sensitivity = value; }
    }

    private float _x, _y;
    
    private Transform _target;
    public Transform Target
    {
        set { _target = value; }
    }

    void Start () 
    {
        _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(ZoomMax) / 2);
    }

    void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _offset.z += Zoom;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _offset.z -= Zoom;
        }
        _offset.z = Mathf.Clamp(_offset.z, -Mathf.Abs(ZoomMax), -Mathf.Abs(ZoomMin));

        _x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivity;
        _y += Input.GetAxis("Mouse Y") * _sensitivity;
        _y = Mathf.Clamp(_y, MinRotation, MaxRotation);
        transform.localEulerAngles = new Vector3(-_y, _x, 0);
        transform.position = transform.localRotation * _offset + _target.position + new Vector3(0f, 2f, 0f);
    }
}

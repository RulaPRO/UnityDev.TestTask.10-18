using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Velocity = 0.5f;

    private float _sensX;
    public float SensX
    {
        set { _sensX = value; }
    }

    private Vector2 _input;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Rotate();
        //Move();
    }

    void GetInput()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        _animator.SetFloat("BlendX", _input.x);
        _animator.SetFloat("BlendY", _input.y);
        if (Input.GetMouseButtonDown(0))
        {
            //_animator.SetBool("AttackStrong", false);
            _animator.SetTrigger(Random.Range(0, 2) == 0 ? "AttackL" : "AttackR");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("PMB, AttackStrong");
            //_animator.SetBool("AttackStrong", true);
            _animator.SetTrigger("AttackStrong");
        }
    }

    void Rotate()
    {
        float rotation = Input.GetAxis("Mouse X") * _sensX;
        Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.up);
        transform.localRotation *= quaternion;
    }

    void Move()
    {
        if (_input.x > 0)
        {
            //transform.position += transform.right * Velocity * Time.deltaTime;
        }
        else if (_input.x < 0)
        {
            //transform.position -= transform.right * Velocity * Time.deltaTime;
        }

        if (_input.y > 0)
        {
            //transform.position += transform.forward * Velocity * Time.deltaTime;
        }
        else if (_input.y < 0)
        {
            //transform.position -= transform.forward * Velocity * Time.deltaTime;
        }
    }
}

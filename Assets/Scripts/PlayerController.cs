using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _sensX;
    public float SensX
    {
        set { _sensX = value; }
    }

    private Animator _animator;
    private Vector2 _input;
    
    private bool _move;
    private float _acceleration;
    private float _accelerationTime = 0.5f;
    private bool _attack;
    public bool Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
        Rotate();
    }

    private void GetInput()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        _animator.SetFloat("BlendX", _input.x);
        _animator.SetFloat("BlendY", _input.y);
        
        if (_input != Vector2.zero)
        {
            _animator.SetBool("Move", true);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                _acceleration += Time.deltaTime;
                if (_acceleration >= _accelerationTime)
                {
                    _animator.SetTrigger("Run");
                    _acceleration = 0;
                }
            }
        }
        else
        {
            _animator.SetBool("Move", false);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Left") &&
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Right"))
            {
                _animator.SetTrigger(Random.Range(0, 2) == 0 ? "AttackL" : "AttackR"); 
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Left") ||
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack Right"))
            {
                _animator.SetTrigger("AttackStrong");
            }
        }
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis("Mouse X") * _sensX;
        Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.up);
        transform.localRotation *= quaternion;
    }

    public void StartAttack()
    {
        _attack = true;
    }
    
    public void StopAttack()
    {
        _attack = false;
    }
}

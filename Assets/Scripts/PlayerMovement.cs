using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, InputController.IPlayerActions
{
    Rigidbody2D rb;
    Animator anim;
    public float speed = 3f;
    float x, y;
    public Vector2 _lastDir;
    private InputController ic;
    void Awake()
    {
        ic = new InputController();
        ic.Player.SetCallbacks(this);
    }
    void OnEnable()
    {
        ic.Player.Enable();
    }
    void OnDisable()
    {
        ic.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(x, y) * speed * Time.fixedDeltaTime);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //if tag is player
            if (gameObject.tag == "Player")
            {
                x = context.ReadValue<Vector2>().x;
                y = context.ReadValue<Vector2>().y;
                if (x != 0 || y != 0)
                {
                    _lastDir = new Vector2(x, y);
                }
                rb.velocity = new Vector2(x * speed, y * speed).normalized;

                anim.SetFloat("Horizontal", _lastDir.x);
                anim.SetFloat("Vertical", _lastDir.y);
                anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
            }
        }
        else if(context.canceled)
        {
            x = 0;
            y = 0;
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
    }
}

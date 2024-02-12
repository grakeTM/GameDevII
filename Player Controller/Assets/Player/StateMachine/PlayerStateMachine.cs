using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class PlayerStateMachine : MonoBehaviour
{
    //state stuff 
    private PlayerBaseState current_state;
    public PlayerGroundState ground_state = new PlayerGroundState();
    public PlayerAirState air_state = new PlayerAirState();

    //debug 
    public TMP_Text debug_text;

    //movement input 
    [HideInInspector] private Vector2 move_input;
    [HideInInspector] private bool grounded;

    //movement vairables 
    [HideInInspector] public CharacterController character_controller;
    [HideInInspector] public Vector3 player_velocity;
    [HideInInspector] public Vector3 wish_dir = Vector3.zero;
    [HideInInspector] public bool jump_buton_pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //get component
        character_controller = GetComponent<CharacterController>();

        current_state = ground_state;

        current_state.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        current_state.UpdateState(this);
        DebugText(); 
    }

    void FixedUpdate()
    {
        findWishDir(); 
        current_state.FixedUpdateState(this);
        MovePlayer(); 
    }

    public void SwitchState(PlayerBaseState cur_state, PlayerBaseState new_State)
    {
        cur_state.ExitState(this);
        current_state = new_State;
        current_state.EnterState(this);
    }
    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }
    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) jump_buton_pressed = true;
        if (context.phase == InputActionPhase.Canceled) jump_buton_pressed = false;
    }
    public void DebugText()
    {
        debug_text.text = "Wish Dir: " + wish_dir.ToString();
        debug_text.text += "\nPlayer Velocity: " + player_velocity.ToString();
        debug_text.text += "\nPlayer Speed: " + new Vector3(player_velocity.x, 0, player_velocity.z).magnitude.ToString();
        debug_text.text += "\nstate: " + current_state.ToString();
    }
    public void findWishDir()
    {
        //find wish dir 
        wish_dir = transform.right * move_input.x + transform.forward * move_input.y;
        wish_dir = wish_dir.normalized;
    }

    public void MovePlayer()
    {
        character_controller.Move(player_velocity * Time.deltaTime);
    }

}

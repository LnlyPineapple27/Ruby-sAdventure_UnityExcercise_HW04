// Student name: Phan Tan Dat
// Student ID: 18127078
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RubyController : MonoBehaviour
{ 
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    public ParticleSystem healthEffect;
    public ParticleSystem hitEffect;
    AudioSource audioSource;
    public AudioClip throwSound, hitSound;
    // Start is called before the first frame update
    RubyInputActions inputActions;
    Vector2 currentInput;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        currentHealth = maxHealth;
        // ==input==
        inputActions = new RubyInputActions();
        inputActions.Ruby.Enable();
        inputActions.Ruby.Attack.performed += LaunchCog;
        inputActions.Ruby.Movement.performed += OnMovement;
        inputActions.Ruby.Movement.canceled += OnMovement;
        inputActions.Ruby.Interact.performed += TalkToNPC;
    }

    // Update is called once per frame
    void Update()
    { 
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        // if(Input.GetKeyDown(KeyCode.C))
        // {
        //     LaunchCog();
        // }
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        //     if (hit.collider != null)
        //     {
        //         NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
        //         if (character != null)
        //         {
        //             character.DisplayDialog();
        //         }
        //     }
        // }
    }
    
    void FixedUpdate()
    {
        if(currentInput.magnitude>0.01){
            Vector2 position = rigidbody2d.position;
            position = position + speed * currentInput * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        } 
        //================Movement=====================
        // horizontal = Input.GetAxis("Horizontal");
        // vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(currentInput.x, currentInput.y);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }


        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            audioSource.PlayOneShot(hitSound);
            Instantiate(hitEffect, GetComponent<Rigidbody2D>().position + Vector2.up * 1.5f, Quaternion.identity);
        }
        else if (amount > 0)
        {
            Instantiate(healthEffect, GetComponent<Rigidbody2D>().position + Vector2.up * 1.5f, Quaternion.identity);  
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    
    void LaunchCog(InputAction.CallbackContext context){
        if(context.performed){
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);
            audioSource.PlayOneShot(throwSound);
            animator.SetTrigger("Launch");
        }
    }
    void OnMovement(InputAction.CallbackContext context){
        if(context.performed){
            currentInput = context.ReadValue<Vector2>();
        }
        if(context.canceled){
            currentInput = Vector2.zero;
        }
    }
    void TalkToNPC(InputAction.CallbackContext context){
        if(context.performed){
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

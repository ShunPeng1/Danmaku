using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Anim_Controller : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    public void OnTakingDamage(bool isDefeated)
    {
        _animator.SetTrigger("Damaged");
        if (isDefeated)
        {
            _animator.SetBool("isDefeated", isDefeated);
        }
    }
}

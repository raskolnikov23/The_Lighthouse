using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator _animator;






    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Attack()
    {
    
        _animator.SetBool("attackBool", true);
        Invoke("FuckOff",.2f);
    }
    
    public void FuckOff()
    {
        _animator.SetBool("attackBool", false);
    }
}

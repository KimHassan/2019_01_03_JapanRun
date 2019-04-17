using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsEffect : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
            Destroy(this.gameObject);
    }
}

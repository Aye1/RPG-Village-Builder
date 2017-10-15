using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour {

    public bool isMouseOver = false;
    private Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAnimatorVariables();
    }

    public void OnMouseEnter()
    {
        isMouseOver = true;
    }

    public void OnMouseOver()
    {
        isMouseOver = true;
    }

    public void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void UpdateAnimatorVariables()
    {
        _animator.SetBool("isMouseOver", isMouseOver);
    }
}

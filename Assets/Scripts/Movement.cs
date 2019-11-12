#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]private float MovementSpeed = 100;
    [SerializeField] private float CoordinateScaleFactor = 0.7f;
    public Vector2 GetInput;

    // Update is called once per frame
    void Update()
    {
        GetInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        SetAnimatorData();
        var move = GetInput.magnitude > 1 ? GetInput.normalized : GetInput;
        GetComponent<Rigidbody2D>().velocity = new Vector2(move.x, move.y * CoordinateScaleFactor) * Time.deltaTime * MovementSpeed;
        //transform.Translate(GetInput.normalized * Time.deltaTime * 2);
    }

    void SetAnimatorData()
    {
        Animator?.SetFloat("Horizontal", GetInput.x);
        Animator?.SetFloat("Vertical", GetInput.y);
        Animator?.SetFloat("Magnitude", GetInput.magnitude);
    }
}

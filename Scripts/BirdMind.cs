using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMind : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rigidbody;
    // Start is called before the first frame update

    float vel = 3f;
    GunControl gun;
    [SerializeField]
    BoxCollider collider;

    BirdGenerator birdGenerator;

    bool activated = false;
    bool isDead = false;

    void Activate()
    {
        birdGenerator = transform.root.GetComponent<BirdGenerator>();
        gun = GameObject.FindWithTag("Gun").GetComponent<GunControl>();
        activated = true;
    }

    public void Init()
    {
        if (!activated) Activate();
        isDead = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        int dir = Random.Range(0,2)*2 - 1;
        transform.position = new Vector3(15f * dir, Random.Range(0.8f,4.5f), Random.Range(12f,30f) );
        transform.rotation = Quaternion.Euler(0f, -90f * dir, 0f);
//        collider.enabled = true;
        rigidbody.useGravity = false;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = vel * transform.forward;
    }

    public IEnumerator Die()
    {
//        collider.enabled = false;
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        yield return new WaitForSeconds(0.3f);
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = true;        
        animator.Play("die");
        yield return null;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!isDead) 
            GameManager.Lost();
        birdGenerator.ReturnToList(gameObject);
    }
}

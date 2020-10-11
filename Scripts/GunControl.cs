using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    RectTransform aimCircle;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    AudioSource audioSource;

    float cd = 0.4f;
    float cdTime = 0f;



    void Update()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Луч пускается из камеры с тегом MainCamera
        RaycastHit hit;

        Vector2 ps =  Input.mousePosition;//Camera.main.WorldToScreenPoint(Input.mousePosition);
        aimCircle.localPosition = ps - new Vector2 (Screen.width/2f, Screen.height/2f);

        if(Physics.Raycast(ray, out hit, mask))
//        if(Physics.Raycast(transform.position, hit.point, out hit))
           transform.LookAt(hit.point);

        if ( (cdTime < Time.time) && (Input.GetMouseButtonDown(0)))
        {
            cdTime = Time.time + cd;
            audioSource.Play();
            GameManager.shots++;
            LayerMask bird = LayerMask.GetMask("Bird");
            Physics.Raycast(transform.position, transform.forward, out hit, 50f);//(ray, out hit, bird);
            if (hit.transform != null)
            if (hit.transform.GetComponent<BirdMind>() != null)  
            {
                GameManager.Shot(true);
                Debug.Log ("You are monster!");
                GameManager.hits++;
                GameManager.scores++;
                StartCoroutine(hit.transform.GetComponent<BirdMind>().Die());
            }
            else
            {
                GameManager.Shot(false);
                Debug.Log ("Your accuracy is hopeless, bro!");
            }
            gameManager.RefreshStat();
        }
 
    }




 }

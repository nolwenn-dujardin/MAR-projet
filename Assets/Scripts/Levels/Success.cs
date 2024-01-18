using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.TextCore.Text;

public class Success : LevelState
{
    private GameObject character;
    private new Camera camera;

    // For Cam movement around character
    private float camSpeed = 2.5f;
    private List<Vector3> camMovePoints = new List<Vector3>();
    private int camCurrentPoints = 0;
    private Vector3 lookTarget;
    private bool pointTimerEnded = true;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            character = other.gameObject;

            SucceedBehaviour();
        }
    }

    private void SucceedBehaviour() 
    {
        if (CurrentState == State.Started && pointTimerEnded)
        {
            // Change camera
            character.GetComponentInChildren<Camera>().enabled = false;
            camera.enabled = true;

            // Set cam look target
            lookTarget = character.transform.position;
            lookTarget.y = character.transform.localScale.y;

            // Play end animation
            Animator animator = character.GetComponent<Animator>();

            //animator.Play("Success");

            // Create points for camera movement
            Vector3 center = transform.position;
            float camHeigth = character.transform.localScale.y * 2;

            Vector3 point1 = new Vector3(center.x + 2, camHeigth, center.z + 2);
            Vector3 point2 = new Vector3(center.x + 2, camHeigth, center.z - 2);
            Vector3 point3 = new Vector3(center.x - 2, camHeigth, center.z - 2);
            Vector3 point4 = new Vector3(center.x - 2, camHeigth, center.z + 2);

            camMovePoints.Add(point1);
            camMovePoints.Add(point2);
            camMovePoints.Add(point3);
            camMovePoints.Add(point4);

            // Set level state
            CurrentState = State.Succeed;
        }
    }

    private void Update()
    {
        if (CurrentState == State.Succeed && pointTimerEnded)
        {
            // Déplacement de la caméra vers le point suivant
            Vector3 pointCible = camMovePoints[camCurrentPoints];
            float distance = Vector3.Distance(camera.transform.position, pointCible);

            if (distance > 0.1f)
            {
                // Déplacement de la caméra vers le point cible
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, pointCible, camSpeed * Time.deltaTime);
            }
            else
            {
                // Passer au point de déplacement suivant une fois que le point actuel est atteint
                camCurrentPoints = (camCurrentPoints + 1) % camMovePoints.Count;

                // Attendre au point
                StartCoroutine(MoveCamera());
            }

            // Orienter la caméra vers le personnage
            camera.transform.LookAt(lookTarget);
        }
    }

    private IEnumerator MoveCamera()
    {
        pointTimerEnded = false;
        // Attente d'une seconde avant de passer au point suivant
        yield return new WaitForSeconds(1.0f);
        pointTimerEnded = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : LevelState
{
    public List<AudioClip> succeedMusics = new List<AudioClip>();

    private GameObject character;
    private new Camera camera;
    private Animator animator;
    private RagdollBehaviour ragdollBehaviour;

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
            animator = character.GetComponent<Animator>();
            ragdollBehaviour = character.GetComponent<RagdollBehaviour>();

            SucceedBehaviour();
        }
    }

    private void SucceedBehaviour() 
    {
        if (CurrentState == State.Started)
        {
            // Change camera
            character.GetComponentInChildren<Camera>().enabled = false;
            camera.enabled = true;

            // Set cam look target
            lookTarget = character.transform.position;
            lookTarget.y = character.transform.position.y + character.transform.localScale.y;

            // Play end animation
            animator.Play("Chicken Dance");

            // Set succeed music
            /*AudioSource audioSource = GetComponentInChildren<AudioSource>();
            audioSource.clip = succeedMusics[new System.Random().Next(0, succeedMusics.Count)];
            audioSource.Play();*/

            AudioManager.Instance.PlayMusic("Success"+Random.Range(1,2));
            TmpManagerInput.Instance.endingScreen();

            // Create points for camera movement
            Vector3 center = transform.position;
            float camHeigth = character.transform.position.y + character.transform.localScale.y * 2;

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
            // D�placement de la cam�ra vers le point suivant
            Vector3 pointCible = camMovePoints[camCurrentPoints];
            float distance = Vector3.Distance(camera.transform.position, pointCible);

            if (distance > 0.1f)
            {
                // D�placement de la cam�ra vers le point cible
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, pointCible, camSpeed * Time.deltaTime);
            }
            else
            {
                // Passer au point de d�placement suivant une fois que le point actuel est atteint
                camCurrentPoints = (camCurrentPoints + 1) % camMovePoints.Count;

                // Attendre au point
                StartCoroutine(MoveCamera());
            }

            // Orienter la cam�ra vers le personnage
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

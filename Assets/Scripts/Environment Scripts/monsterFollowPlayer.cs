using UnityEngine;
using UnityEngine.Analytics;

public class SpiderController : MonoBehaviour
{
    public Transform player;  // Referinta pentru player
    public float moveSpeed = 3f;  // Viteza de miscare a paianjenului
    public float gravity = 9.8f;  // Gravitatia paianjenului
    private CharacterController characterController;
    public GameObject gameOverUI;
    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player != null)
        {
            // Directia in care trebuie sa mearga paianjenul pentru a ajunge la jucator
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f;  // Ignora miscarea pe verticala

            // Roteste paianjenul in directia jucatorului
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), 0.1f);

            // Verifica daca paianjenul se afla pe pamant
            if (characterController.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;  // Reseteaza miscarea pe verticala daca paianjenul se afla pe pamant
            }

          //  Debug.Log($"Is Grounded: {characterController.isGrounded}");

            // Aplica gravitate
            ApplyGravity();

            // Misca paianjenul in directia jucatorului
            Vector3 moveDirection = directionToPlayer.normalized;
            moveDirection.y = player.position.y;  // Include miscarea pe verticala
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            // Aplica gravitatie manual
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Verifica daca exista o coliziune intre paianjen si jucator
        if (other.CompareTag("Player"))
        {
            // Daca paianjenul atinge jucatorul, atunci este Game Over
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void IncreaseSpiderSpeed(float speedMultiplier)
    {
        // Viteza paianjenului se mareste cand jucatorul colecteaza monede
        moveSpeed *= speedMultiplier;
        Debug.Log($"Spider speed increased to: {moveSpeed}");
    }
}

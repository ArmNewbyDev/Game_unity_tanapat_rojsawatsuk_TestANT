using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public float moveSpeed = 7f;
    public List<KeyCode> fireKeys = new List<KeyCode> { KeyCode.P };

    private GameObject bulletPrefab;

    void Start()
    {
        // โหลด Bullet Prefab จาก Resources/Prefabs/Bullets
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        if (bulletPrefab == null)
        {
            Debug.LogWarning("ไม่พบ Bullet Prefab ที่ Resources/Prefabs/Bullet");
        }
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move() // Function For Move In Camera Zone only
    {
        float moveX = Input.GetAxis("Horizontal"); // Detect Player Input Horizontal Axis Button like Left, Right, A, D
        float moveY = Input.GetAxis("Vertical"); // Detect Player Input Vertical Axis Button like Up, Down, W, S
        Vector3 delta = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;

        transform.position += delta;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    void Shoot() // Create Prefab Bullet at position Player
    {
        foreach (KeyCode key in fireKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (bulletPrefab != null)
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // If Player colaps Enemy Player will take damage at MainLogic and Enemy Get Destory or Dead
        {
            FindObjectOfType<MainLogic>()?.GetDamage();
            Destroy(other.gameObject,0.2f); // Enemey Dead After Tackle to Player in 0.2 sec
        }
    }
}
 
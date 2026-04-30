using UnityEngine;

public class BunnyLauncher : MonoBehaviour

{

    public GameObject carrotPrefab;

    public Transform shootPoint;

    public float m = 1.0f;          // มวล

    public float a_multiplier = 10f; // ตัวคูณความแรง

    private Vector2 startPos;

    void Update()

    {

        // 1. กดเมาส์เก็บจุดเริ่มต้น

        if (Input.GetMouseButtonDown(0))

        {

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        // 2. ปล่อยเมาส์เพื่อยิง

        if (Input.GetMouseButtonUp(0))

        {

            // --- เช็คก่อนว่ากระสุนใน GameManager เหลือไหม ---

            if (GameManager.Instance != null && GameManager.Instance.ammo > 0)

            {

                Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 dragVector = startPos - endPos; 

                float acceleration = dragVector.magnitude * a_multiplier;

                Vector2 direction = dragVector.normalized;

                // สูตร F = ma

                Vector2 force = direction * (m * acceleration);

                Shoot(force);

                // --- สั่งลดจำนวนแครอทใน GameManager ---

                GameManager.Instance.UseAmmo();

            }

            else

            {

                Debug.Log("Out of Carrots!");

            }

        }

    }

    void Shoot(Vector2 forceVector)

    {

        if (carrotPrefab != null && shootPoint != null)

        {

            GameObject newCarrot = Instantiate(carrotPrefab, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = newCarrot.GetComponent<Rigidbody2D>();

            if (rb != null)

            {

                rb.AddForce(forceVector, ForceMode2D.Impulse);

            }

        }

    }

}
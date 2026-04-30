using UnityEngine;

public class BunnyLauncher : MonoBehaviour

{

    public GameObject carrotPrefab;

    public Transform shootPoint;

    public float m = 1.0f;          // มวล

    public float a_multiplier = 10f; // ตัวคูณความแรง (ปรับเพิ่ม-ลดใน Inspector ได้)

    private Vector2 startPos;

    void Update()

    {

        // 1. กดเมาส์ปุ๊บ เก็บจุดเริ่มต้นทันที

        if (Input.GetMouseButtonDown(0))

        {

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        // 2. ปล่อยเมาส์ปุ๊บ คำนวณจุดสุดท้ายแล้ว "ยิง"

        if (Input.GetMouseButtonUp(0))

        {

            Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // คำนวณเวกเตอร์การลาก (ลากถอยหลังไปทางไหน พุ่งไปทางตรงข้าม)

            Vector2 dragVector = startPos - endPos; 

            // --- ส่วนที่ทำให้ ลากไกล=แรง / ลากใกล้=เบา ---

            float acceleration = dragVector.magnitude * a_multiplier;

            Vector2 direction = dragVector.normalized;

            // สูตร F = ma

            Vector2 force = direction * (m * acceleration);

            Shoot(force);

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

                // ใช้ Impulse เพื่อให้พุ่งออกไปทันที

                rb.AddForce(forceVector, ForceMode2D.Impulse);

            }

        }

    }

}


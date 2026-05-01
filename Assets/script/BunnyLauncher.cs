using UnityEngine;

public class BunnyLauncher : MonoBehaviour
{
    public GameObject carrotPrefab;
    public Transform shootPoint;

    public float m = 1.0f;          // มวล
    public float a_multiplier = 10f; // ตัวคูณความเร่ง

    Vector2 startPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 drag = startPos - endPos;

            // ✅ คำนวณ "ความเร่ง" จากระยะลาก
            float a = drag.magnitude * a_multiplier;

            // ✅ หา "ทิศทาง"
            Vector2 direction = drag.normalized;

            // ✅ ใช้สูตรฟิสิกส์ F = m * a
            Vector2 force = direction * m * a;

            Shoot(force);
        }
    }

    void Shoot(Vector2 force)
    {
        GameObject carrot = Instantiate(carrotPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = carrot.GetComponent<Rigidbody2D>();

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
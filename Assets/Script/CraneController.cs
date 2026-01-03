using UnityEngine;

public class CraneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody carriageRb; // XZ移動する台車
    [SerializeField] private Rigidbody winchRb;    // 上下に動くウインチ

    [Header("Speeds (m/s)")]
    [SerializeField] private float moveSpeed = 1.5f; // XZ移動
    [SerializeField] private float liftSpeed = 1.2f; // Y移動

    [Header("World Bounds")]
    [SerializeField] private Vector2 xRange = new Vector2(-1.5f, 1.5f); // X制限
    [SerializeField] private Vector2 zRange = new Vector2(-1.0f, 1.0f); // Z制限
    [SerializeField] private Vector2 yRange = new Vector2(0.0f, 5.0f);  // Y制限（地面～天井）

    float xIn, zIn, yIn;

    void Update()
    {
        // ←→ / ↑↓ で水平、Q/A で上下
        xIn = (Input.GetKey(KeyCode.LeftArrow)  ? -1f : 0f)
            + (Input.GetKey(KeyCode.RightArrow) ?  1f : 0f);
        zIn = (Input.GetKey(KeyCode.DownArrow)  ? -1f : 0f)
            + (Input.GetKey(KeyCode.UpArrow)    ?  1f : 0f);
        yIn = (Input.GetKey(KeyCode.Q) ?  1f : 0f) +
              (Input.GetKey(KeyCode.A) ? -1f : 0f);
    }
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // --- Carriage を XZ に ---
        Vector3 c = carriageRb.position;
        c += new Vector3(xIn * moveSpeed, 0f, zIn * moveSpeed) * dt;
        c.x = Mathf.Clamp(c.x, xRange.x, xRange.y);
        c.z = Mathf.Clamp(c.z, zRange.x, zRange.y);
        carriageRb.MovePosition(c);

        // --- Winch を Y に + XZ は Carriage に追従させる ←ここがポイント ---
        Vector3 w = winchRb.position;

        // 追記：XZを台車に合わせる
        w.x = c.x;
        w.z = c.z;

        // 既存：Yは入力で上下＆クランプ
        w.y += yIn * liftSpeed * dt;
        w.y  = Mathf.Clamp(w.y, yRange.x, yRange.y);

        winchRb.MovePosition(w);
    }

}

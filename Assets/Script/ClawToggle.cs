// ClawToggle.cs
using UnityEngine;

public class ClawToggle : MonoBehaviour
{
    [System.Serializable]
    public class Finger
    {
        public HingeJoint joint;
        public bool invertDirection;       // 片方だけ回転向きが逆ならON
        [Range(-90, 90)] public float openAngle = 30f;   // Limits.Min に相当
        [Range(-90, 90)] public float closeAngle = -10f; // Limits.Max に相当
    }

    [Header("Fingers")]
    public Finger[] fingers;

    [Header("Motor Params")]
    public float moveVelocity = 160f;   // deg/s（動かすとき）
    public float holdVelocity = 40f;    // deg/s（閉じて保持するときの弱押し）
    public float motorForce = 100f;     // トルク（保持力に直結）

    [Header("Behavior")]
    public bool holdWithMotor = true;   // 閉じた後も弱く押し続ける

    private bool isClosing = false;

    void Start()
    {
        // 角度制限を反映し、Inspector側のUseMotorは切っておく
        foreach (var f in fingers)
        {
            var limits = f.joint.limits;
            limits.min = f.openAngle;
            limits.max = f.closeAngle;
            f.joint.limits = limits;

            f.joint.useLimits = true;
            f.joint.useMotor  = false;
            f.joint.useSpring = false; // まずはモーター制御だけ
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClosing = !isClosing;   // トグル
            ApplyState(isClosing);
        }

        // 閉じている間に弱く押し続けて保持したい場合
        if (isClosing && holdWithMotor)
        {
            DriveMotor(closing: true, hold: true);
        }
    }

    void ApplyState(bool closing)
    {
        // 開/閉のときにだけモーターを回す
        DriveMotor(closing, hold: false);
    }

    void DriveMotor(bool closing, bool hold)
    {
        foreach (var f in fingers)
        {
            float dir = closing ? -1f : 1f;   // Axis=(1,0,0) を想定：負が閉、正が開
            if (f.invertDirection) dir *= -1f;

            var m = f.joint.motor;
            m.force = motorForce;
            m.targetVelocity = (hold ? holdVelocity : moveVelocity) * dir;
            f.joint.motor = m;

            f.joint.useMotor = true;
            f.joint.useSpring = false;

            // 角度がリミット付近ならモーターを止めて無駄な力をかけない
            float a = f.joint.angle;
            float stopEps = 1.5f; // [deg]
            if (!closing && Mathf.Abs(a - f.openAngle) < stopEps)
                f.joint.useMotor = false;
            if (closing && Mathf.Abs(a - f.closeAngle) < stopEps && !hold)
                f.joint.useMotor = false;
        }
    }
}

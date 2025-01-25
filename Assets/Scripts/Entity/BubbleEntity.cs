using UnityEngine;

public class BubbleEntity : MonoBehaviour
{
    public float MoveForce = 5f;
    public float TurnForce = 5f;

    Rigidbody2D _rb;

    #region COM vars
    [field: SerializeField] public Transform BubbleHolder { get; private set; }
    [SerializeField] Transform _offsetTransform;
    Vector2 _targetLocalOffset;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LerpCOMOffset();
    }

    #region COM
    void LerpCOMOffset()
    {
        _offsetTransform.transform.localPosition = _offsetTransform.transform.localPosition.Lerp(_targetLocalOffset, 12f * Time.deltaTime);
    }
    public void AdjustCOM()
    {
        Vector2 averageLocalPos = Vector2.zero;
        int count = 0;
        foreach(Transform t in BubbleHolder)
        {
            averageLocalPos += (Vector2)t.transform.localPosition;
            count++;
        }
        if (count == 0) return;
        averageLocalPos /= count;

        _targetLocalOffset = -(Vector3)averageLocalPos;
    }
    #endregion

    #region Controls
    public void Move(Vector2 dir)
    {
        _rb.AddForce(dir.normalized * MoveForce);
    }

    public void Turn(float dir)
    {
        dir = Mathf.Sign(dir);
        _rb.AddTorque(dir * TurnForce);
    }
    #endregion
}

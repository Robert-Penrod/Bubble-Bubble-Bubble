using UnityEngine;

public class BubbleEntity : MonoBehaviour
{
    [field: SerializeField] public Transform BubbleHolder { get; private set; }
    [SerializeField] Transform _offsetTransform;
    Vector2 _targetLocalOffset;

    private void Update()
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
}

using UnityEngine;

public class BubbleEntity : MonoBehaviour
{
    [field: SerializeField] public Transform BubbleHolder { get; private set; }
    [SerializeField] Transform _offsetTransform;

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

        _offsetTransform.transform.localPosition = -(Vector3)averageLocalPos;
    }
}

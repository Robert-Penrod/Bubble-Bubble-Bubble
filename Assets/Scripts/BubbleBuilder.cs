using UnityEngine;

public class BubbleBuilder : MonoBehaviour
{
    [SerializeField] BubbleEntity _targetBubbleEntity;
    [SerializeField] GameObject _bubblePrefab;
    Vector2 _placePoint;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_placePoint, 0.5f);
    }

    private void Update()
    {
        HandleInputTemp();
        HandleBubbleCast();
    }

    void HandleInputTemp()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlaceBubble();
        }
    }

    void PlaceBubble()
    {
        Instantiate(_bubblePrefab, _placePoint, Quaternion.identity, _targetBubbleEntity.BubbleHolder);
        _targetBubbleEntity.AdjustCOM();
    }

    void HandleBubbleCast()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPos = _targetBubbleEntity.transform.position;
        Vector2 inputDir = mousePoint - targetPos;

        RaycastHit2D hit = BubblePerimiterCast(targetPos, inputDir, 20f);
        if (hit.collider == null) return;
        _placePoint = hit.point + hit.normal * 0.5f * 0.9f;
    }

    RaycastHit2D BubblePerimiterCast(Vector2 origin, Vector2 inputDir, float radius)
    {
        Vector2 dir = -inputDir.normalized;
        Vector2 castOrigin = origin - dir * radius;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, 0.5f, dir, Vector2.Distance(origin, castOrigin));
        Debug.DrawLine(castOrigin, castOrigin + dir * hit.distance);
        hit.normal = -dir;
        return hit;
    }
}

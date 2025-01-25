using UnityEngine;

public class BubbleBuilder : MonoBehaviour
{
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
        Instantiate(_bubblePrefab, _placePoint, Quaternion.identity);
    }

    void HandleBubbleCast()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = BubblePerimiterCast(Vector2.zero, mousePoint, mousePoint.magnitude);
        if (hit.collider == null) return;
        _placePoint = hit.point + hit.normal * 0.5f * 0.9f;
    }

    RaycastHit2D BubblePerimiterCast(Vector2 origin, Vector2 point, float radius)
    {
        Vector2 dir = (origin - point).normalized;
        Vector2 castOrigin = origin - dir * radius;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, 0.5f, dir, Vector2.Distance(origin, castOrigin));
        hit.normal = -dir;
        return hit;
    }
}

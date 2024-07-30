using System.Collections;
using System.Collections.Generic;
using KyawaLib;
using Unity.VisualScripting;
using UnityEngine;

public class HomeObject : MonoBehaviour
{
    [SerializeField, ReadOnly]
    List<ShowcaseMochi> m_showcaseMochis = new();

    public List<ShowcaseMochi> showcaseMochis => m_showcaseMochis;

    public ShowcaseMochi GetShowcaseMochi(int id)
    {
        var mochi = m_showcaseMochis.Find(_ => _.ID == id);
        KyDebug.AssertIsNotNull(mochi);
        return mochi;
    }

    void FixedUpdate()
    {
        var plateBubble = HomeManager.instance?.UI?.plateBubble;
        if (plateBubble == null)
            return;

        var camera = Camera.main;
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider)
        {
            var parent = hit.collider.transform.parent;
            if (parent.TryGetComponent<ShowcaseMochi>(out var showcaseMochi))
            {
                var mochiData = DataManafer.instance.mochiData.GetDataFromID(showcaseMochi.ID);
                var name = showcaseMochi.isUnlocked ? mochiData.name : "？？？";
                var range = mochiData.range;
                var hint = (range.x == range.y) ? $"{range.x}回" : $"{range.x}〜{range.y}回";
                var viewPortPos = camera.WorldToViewportPoint(hit.collider.transform.position);
                var canvasPos = new Vector3((int)(viewPortPos.x * PixelArtGame.SCREEN_WIDTH), (int)(viewPortPos.y * PixelArtGame.SCREEN_HEIGHT));
                plateBubble.Show(name, hint, canvasPos);
                return;
            }
        }

        plateBubble.Hide();
    }
}

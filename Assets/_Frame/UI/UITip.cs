using UnityEngine;

public class UITip : UIBase<UITip>
{
    public UITipItem DefaultTipPrefab;

    public UITipItem ShowTip()
    {
        var prefab = DefaultTipPrefab;
        return ShowTip(prefab, Vector3.zero);
    }

    public UITipItem ShowTip(Vector3 position)
    {
        var prefab = DefaultTipPrefab;
        return ShowTip(prefab, position);
    }

    public UITipItem ShowTip(UITipItem tipPrefab, Vector3 position)
    {
        var tip = GamePool.Spawn(tipPrefab, transform, position);
        tip.transform.position = position;
        tip.Show();
        return tip;
    }
}

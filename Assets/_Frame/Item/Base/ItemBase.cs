using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Aya.Physical;

public enum ItemDeSpawnMode
{
    None = 0,
    Effect = 1,
    Exit = 2,
}

public enum ItemEffectMode
{
    Once = 0,
    UnLimit = 1,
    Count = 2,
    Stay = 3,
}

public abstract class ItemBase : GameEntity
{
    [FoldoutGroup("Param"), Tooltip("����Layer")] public LayerMask LayerMask;
    [FoldoutGroup("Param"), EnumToggleButtons, Tooltip("����ʱ��")] public ItemDeSpawnMode DeSpawnMode = ItemDeSpawnMode.Effect;
    [FoldoutGroup("Param"), Tooltip("�Ƿ������壬ֻ���սű�")] public bool RetainRender;
    [FoldoutGroup("Param"), EnumToggleButtons, Tooltip("����ģʽ")] public ItemEffectMode EffectMode = ItemEffectMode.Once;
    [FoldoutGroup("Param"), ShowIf("EffectMode", ItemEffectMode.Count), Tooltip("Countģʽ��������")] public int EffcetCount = 1;
    [FoldoutGroup("Param"), ShowIf("EffectMode", ItemEffectMode.Stay), Tooltip("Stayģʽ�������")] public float StayInterval = 1f;

    private bool Alive;
    private int SelfEffcetCount;

    protected override void Awake()
    {
        base.Awake();
        var listener = gameObject.AddComponent<ColliderListener>();
        listener.onTriggerEnter.Add(LayerMask, OnEffectEnter);
        listener.onTriggerStay.Add(LayerMask, OnEffectStay);
        listener.onTriggerExit.Add(LayerMask, OnEffectExit);
    }

    public virtual void Init()
    {
        Alive = true;
        SelfEffcetCount = 0;
    }

    private void OnEffectEnter(Collider other)
    {
        if (!Alive) return;
        OnEnter(other);
        if (EffectMode == ItemEffectMode.Once)
        {
            Alive = false;
        }
        else if(EffectMode == ItemEffectMode.Count)
        {
            SelfEffcetCount++;
            if (SelfEffcetCount >= EffcetCount)
            {
                Alive = false;
            }
        }

        //�ж��Ƿ����
        if (DeSpawnMode == ItemDeSpawnMode.Effect)
        {
            GamePool.DeSpawn(this);
        }
    }

    private void OnEffectStay(Collider other)
    {
        if (!Alive) return;
        OnStay(other);
    }

    private void OnEffectExit(Collider other)
    {
        if (!Alive) return;
        OnExit(other);

        //�ж��Ƿ����
        if (DeSpawnMode == ItemDeSpawnMode.Exit)
        {
            GamePool.DeSpawn(this);
        }
    }

    public virtual void OnEnter(Collider other)
    {
        if (!Alive) return;
    }

    private float _LastStayTrigger;
    public virtual void OnStay(Collider other)
    {
        if (!Alive) return;
        if (Time.realtimeSinceStartup - _LastStayTrigger < StayInterval)
        {
            return;
        }

        _LastStayTrigger = Time.realtimeSinceStartup;
    }

    public virtual void OnExit(Collider other)
    {
        if (!Alive) return;
    }
}

public abstract class ItemBase<T> : ItemBase where T : Component
{
    public override void OnEnter(Collider other)
    {
        base.OnEnter(other);
        OnTargetEffect(other.transform.GetComponentInParent<T>());
    }

    public override void OnStay(Collider other)
    {
        base.OnStay(other);
        OnTargetStay(other.transform.GetComponentInParent<T>());
    }

    public override void OnExit(Collider other)
    {
        base.OnExit(other);
        OnTargetExit(other.transform.GetComponentInParent<T>());
    }

    public abstract void OnTargetEffect(T target);

    public virtual void OnTargetStay(T target)
    {

    }

    public virtual void OnTargetExit(T target)
    {

    }
}

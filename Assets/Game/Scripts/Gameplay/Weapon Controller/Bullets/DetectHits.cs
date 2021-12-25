using UnityEngine;

[RequireComponent(typeof(Despawn))]
public class DetectHits : TriggerEvents
{
    [SerializeField] private GenericReference<int> _hitReward;
    [SerializeField] private GameEvent _addScore;
    [SerializeField] private GameObject _hitImpact;
    
    private Despawn _despawn;
    private GameObject _hitParticle;

    protected override void Start()
    {
        base.Start();
        _despawn = GetComponent<Despawn>();
    }

    public override void TriggerEnter(GameObject triggeredObject)
    {
        _addScore.Raise(_hitReward.Value);
        _despawn.DespawnMe();
        _hitParticle = PoolManager.Instance.Spawn(_hitImpact, transform.position, Quaternion.identity);
        
        _hitParticle.transform.position = transform.position;
        _hitParticle.transform.rotation = Quaternion.identity;
        
        PoolManager.Instance.Despawn(triggeredObject, true);
    }

    public override void TriggerExit(GameObject triggeredObject)
    {
    }
}

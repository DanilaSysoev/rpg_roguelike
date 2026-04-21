using RpgRoguelike.Core.EnemyStrategies;

namespace RpgRoguelike.Core;

public class Enemy : Entity
{
    public IMovementStrategy? MovementStrategy { get; internal set; }

    public override void Update()
    {
        base.Update();
        if(!IsStunned)
            Action();
    }

    private void Action()
    {
        if(WithinReach(Game.Instance.Player))
            Attack(Game.Instance.Player);
        else
            Move();
    }

    public override void Attack(Entity target)
    {
        if(Weapon is null)
            return;
        
        Weapon.Attack(target);
        Weapon.ApplyEffect(target);
    }

    private bool WithinReach(Entity player)
    {
        return Position.DownNeighbor() == player.Position ||
               Position.LeftNeighbor() == player.Position ||
               Position.UpNeighbor() == player.Position ||
               Position.RightNeighbor() == player.Position;
    }

    protected virtual void Move()
    {
        MovementStrategy?.Move(this);
    }
}

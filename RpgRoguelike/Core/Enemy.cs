namespace RpgRoguelike.Core;

public class Enemy : Entity
{
    public override void Update()
    {
        base.Update();
        if(!IsStunned)
            Action();
    }

    private void Action()
    {
        Move();
        Attack(Game.Instance.Player);
    }

    public override void Attack(Entity target)
    {
        if(Weapon is null)
            return;
        
        if(WithinReach(target))
        {
            Weapon.Attack(target);
            Weapon.ApplyEffect(target);
        }
    }

    private bool WithinReach(Entity player)
    {
        return Position.DownNeighbor() == player.Position ||
               Position.LeftNeighbor() == player.Position ||
               Position.UpNeighbor() == player.Position ||
               Position.RightNeighbor() == player.Position;
    }

    protected virtual void Move()
    {}
}

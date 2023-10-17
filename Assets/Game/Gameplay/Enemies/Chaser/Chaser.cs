
// Враг, атакующий в ближнем бою
namespace Enemies
{
    public class Chaser : EnemyBase, IDamagable, IGrabable
    {
        public void GetDamage()
        {
            throw new System.NotImplementedException();
        }

        public void OnGrab()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnemyDeath()
        {
            throw new System.NotImplementedException();
        }
    }
}

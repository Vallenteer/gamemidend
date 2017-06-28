using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;
        public Fighter caster;
        public float damage;
        public void OnCollisionEnter(Collision col)
        {
            
            Fighter fighter = col.gameObject.GetComponent<Fighter>();
            Debug.Log("HIT " +fighter.name);
            if (fighter != null && fighter != caster)
            {
                fighter.hurt(damage);
                Destroy(gameObject);
            }
        }
    }
}

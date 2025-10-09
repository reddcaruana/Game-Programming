using UnityEngine;

namespace _2_ProgFun
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private string enemyName;
        
        [SerializeField]
        [Range(1, 10)]
        private int attackValue;

        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float torque = 180f;
        
        [SerializeField]
        private bool isBoss;

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Rotate(Vector3.up * torque * Time.deltaTime);
        }
    }
}
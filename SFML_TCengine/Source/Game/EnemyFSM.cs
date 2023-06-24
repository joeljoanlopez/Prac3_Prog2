using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGame;

namespace SFML_TCengine.Source.Game
{
    public class EnemyFSM
    {
        private enum State
        {
            Idle,
            Attack
        }
        private State currentState;
        private Vector2f enemyPosition;
        private Vector2f playerPosition;
        private float detectionRange;
        private bool playerInRange;

        public EnemyFSM(Vector2f startPosition, Vector2f startPlayerPosition, float range)
        {
            currentState = State.Idle;
            enemyPosition = startPosition;
            playerPosition = startPlayerPosition;
            detectionRange = range;
            playerInRange = false;
        }
        public void Update(Vector2f currentPlayerPosition)
        {
            playerPosition = currentPlayerPosition;
            switch(currentState)
            {
              case State.Idle:
                    if(playerInRange)
                    {
                        currentState = State.Attack;

                    }
                    break;
                case State.Attack:
                    if (!playerInRange)
                    {
                        currentState = State.Idle;
                    }
                    break;

            }
        }
        public bool PlayerInRange()
        {
            float distance;
            float distanceX = (enemyPosition.X - playerPosition.X);
            float distanceY = (enemyPosition.Y - playerPosition.Y);
            distance = ((distanceX* distanceX)+(distanceY * distanceY));
            distance = (float)Math.Sqrt(distance);
            playerInRange = distance <= detectionRange;
            return playerInRange;
        }
        public void AttackPlayer() 
        {
            float distance;
            float distanceX = (enemyPosition.X - playerPosition.X);
            float distanceY = (enemyPosition.Y - playerPosition.Y);
            Vector2f attackDirection = new Vector2f(distanceX, distanceY);

        }
    }
}

using System;
using SFML;
using TCEngine;

namespace TCGame
{

    public class AnimationFSMComponent : BaseComponent
    {
        enum AnimationState
        {
            Idle,
            Moving,
        };

        AnimationState _CurrentState;

        string _TexturePath;
        uint _rows;
        uint _cols;

        float _scale;

        public AnimationFSMComponent(float scale)
        {
            _CurrentState = AnimationState.Idle;
            _scale = scale;
        }

        void ChangeState(AnimationState nextState)
        {
            // OnLeaveState(_CurrentState);
            OnEnterState(nextState);
            _CurrentState = nextState;
        }

        private void OnEnterState(AnimationState nextState)
        {
            switch (nextState)
            {
                case AnimationState.Idle:
                    _TexturePath = "Data/Textures/ProtaIdle.png";
                    _cols = 3u;
                    _rows = 1u;
                    Animate();
                    break;
                case AnimationState.Moving:
                    _TexturePath = "Data/Textures/ProtaRun.png";
                    _cols = 4u;
                    _rows = 1u;
                    Animate();
                    break;
            }
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
            switch (_CurrentState)
            {
                case AnimationState.Idle:
                    if (Owner.GetComponent<PlayerMovementController>().Moving())
                    {
                        ChangeState(AnimationState.Moving);
                    }
                    break;
                case AnimationState.Moving:

                    if (!Owner.GetComponent<PlayerMovementController>().Moving())
                    {
                        ChangeState(AnimationState.Idle);
                    }
                    break;
            }
        }

        private void Animate()
        {
            AnimatedSpriteComponent animation = Owner.GetComponent<AnimatedSpriteComponent>();
            animation.ChangeAnim(_TexturePath, _cols, _rows);
            animation.sprite.Scale *= _scale;
            animation.Center();
        }
    }
}
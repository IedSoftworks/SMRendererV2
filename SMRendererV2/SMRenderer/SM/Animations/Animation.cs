using SM.Core;
using SM.Data.Types;

namespace SM.Animations
{
    public class Animation : Timer
    {
        private Keyframe _origin;
        private AnimationVector _moveVector;
        private AnimationStruct _aStruct;
        public int CurrentKeyframeIndex { get; private set; } = 0;
        public Keyframe CurrentKeyframe { get; private set; }
        public Vector Obj { get; }
        public float KeyframeTimer { get; private set; }
        public float KeyframeTargetTime { get; private set; }

        public Animation(Vector obj, AnimationStruct aStruct, bool repeat) : base(aStruct.Time, repeat)
        {
            _aStruct = aStruct;
            Obj = obj;
        }

        public override void Start(bool repeater = false)
        {
            base.Start(repeater);

            CurrentKeyframeIndex = 0;
            Obj.Set(CurrentKeyframe.Value);
            NextKeyframe();
        }

        public override void PerformTick(float delta)
        {
            if (KeyframeTimer >= KeyframeTargetTime) NextKeyframe();

            KeyframeTimer += delta;
            Obj.Set(_origin.Value + _moveVector * _aStruct.CalculationFunction(this, delta));

            base.PerformTick(delta);
        }


        private void NextKeyframe()
        {
            _origin = _aStruct.Keyframes[CurrentKeyframeIndex];

            if (CurrentKeyframeIndex == _aStruct.KeyframeAmount - 1) Stop();

            CurrentKeyframeIndex++;
            KeyframeTimer = 0;
            CurrentKeyframe = _aStruct.Keyframes[CurrentKeyframeIndex];
            KeyframeTargetTime = CurrentKeyframe.Time - _origin.Time;

            if (!_aStruct.RelativeValues)
                _moveVector = CurrentKeyframe.Value - _origin.Value;
            else 
                _moveVector = CurrentKeyframe.Value;
        }
    }
}
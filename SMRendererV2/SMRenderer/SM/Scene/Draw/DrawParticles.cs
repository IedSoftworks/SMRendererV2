using System;
using System.Reflection.Emit;
using OpenTK;
using SM.Render.ShaderPrograms;
using SM.Scene.Cameras;
using SM.Scene.Draw.Base;
using SM.Scene.Draw.Particles;

namespace SM.Scene.Draw
{
    public class DrawParticles : ModelPositioning
    {
        public ParticleObject ParticleObject;

        public TimeSpan Time = TimeSpan.FromSeconds(5);
        public float TotalTime { get; private set; }
        public float TimeLeft { get; private set; }
        public virtual Func<ParticleObject, Particle, DrawParticles, Matrix4> MoveAction { get; } = ParticleMovements.Linear;

        public bool Infinite = false;

        public float FadeInEndTime = 2f;
        public float FadeOutStartTime = 3f;
        public float Fade = 1;

        public DrawParticles(ParticleObject particleObject)
        {
            ParticleObject = particleObject;
        }

        public override void Prepare(double delta)
        {
            base.Prepare(delta);

            if (!ParticleObject.Ready) ParticleObject.Generate();

            TotalTime += (float)delta;
            TimeLeft = (float) Time.TotalSeconds - TotalTime;

            if (Infinite) return;

            if (TotalTime > Time.TotalSeconds)
            {
                Parent.Remove(this);
                return;
            }
            Fade = TotalTime < FadeInEndTime ? TotalTime / FadeInEndTime : 1;
            if (Fade == 1)
            {
                Fade = TimeLeft < FadeOutStartTime ? TimeLeft / FadeInEndTime : 1;
            }

        }

        public override void Draw(Camera camera)
        {
            RenderProgramCollection.Particle.Draw(camera, this);
        }
    }
}
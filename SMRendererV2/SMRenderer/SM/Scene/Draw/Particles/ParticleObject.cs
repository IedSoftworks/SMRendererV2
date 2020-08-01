using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SM.Core.Exceptions;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Draw.Particles
{
    public abstract class ParticleObject
    {
        private ulong _lastFrame = 0;

        public Particle[] Particles { get; private set; }

        public bool Ready { get; private set; } = false;
        public int Amount = 32;

        public bool VariableSpeeds = true;
        public float MinSpeed = 1f;
        public float MaxSpeed = 5;

        public bool VariableSizes = true;
        public float MaxSize = 1;

        public bool RotateTowardsOrigin = false;

        public void Generate()
        {
            Particles = new Particle[Amount];

            for (int i = 0; i < Amount; i++)
            {
                Direction direction = MotionAlgorithm(i);
                direction.Normalize();

                Quaternion quaternion = new Quaternion(0,0,0);
                if (RotateTowardsOrigin)
                    quaternion = Matrix4.LookAt(Vector3.Zero, direction, Vector3.UnitY).ExtractRotation().Inverted();

                direction.Mul(VariableSpeeds ? (float)SMGlobals.Randomizer.NextDouble() * MaxSpeed + MinSpeed : MaxSpeed);

                Particles[i] = new Particle() 
                {
                    Direction = direction, 
                    Size = new Size(VariableSizes ? (float)SMGlobals.Randomizer.NextDouble() * MaxSize + .1f : .1f),
                    Rotation = quaternion
                };
            }

            Ready = true;
        }

        public abstract Direction MotionAlgorithm(int index);
    }
}
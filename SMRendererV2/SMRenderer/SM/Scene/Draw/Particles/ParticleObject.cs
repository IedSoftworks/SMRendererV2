using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SM.Core.Exceptions;
using SM.Data.Types;
using SM.Data.Types.VectorTypes;
using SM.Utility;

namespace SM.Scene.Draw.Particles
{
    public abstract class ParticleObject
    {
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
                Vector direction = MotionAlgorithm(i);
                direction.Normalize();

                Quaternion quaternion = new Quaternion(0,0,0);
                if (RotateTowardsOrigin)
                    quaternion = Matrix4.LookAt(Vector3.Zero, direction, Vector3.UnitY).ExtractRotation().Inverted();

                direction.Mul(VariableSpeeds ? Randomize.GetFloat(MinSpeed, MaxSpeed) : MaxSpeed);

                Particles[i] = new Particle() 
                {
                    Direction = direction, 
                    Size = new Vector(VariableSizes ? Randomize.GetFloat(.1f, MaxSize) : .1f),
                    Rotation = quaternion
                };
            }

            Ready = true;
        }

        public abstract Vector MotionAlgorithm(int index);
    }
}
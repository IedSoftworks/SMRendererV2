using System;
using System.Collections.Generic;
using OpenTK;
using SMRenderer.Core;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Renderer;
using SMRenderer.Types.Extensions;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Draw
{
    public class DrawParticle : DrawBase
    {
        public const int MaximumVertices = 737280;
        public static bool IgnoreVerticeMax = false;
        public static Random Random = new Random();

        internal float[] ShaderFloats = new float[] {};
        internal float CurrentTime;

        public Position StartPosition = new Position(0);
        public Size ObjectSize = new Size(1);
        public Rotation Rotation = new Rotation(0);

        public Matrix4 ModelMatrix;

        public TimeSpan Length = TimeSpan.FromSeconds(5);
        public float TimeLeft = 1;
        public float FadeStart = .6f;
        public float CurrentFade { get; private set; }
        public bool PrettyRemove = false;

        public Vector2 Cone = Vector2.One;
        public float Speed = 1;
        public float MinSpeed = .1f;
        public bool VariableSpeeds = true;

        public int Amount = 8;

        public override void Prepare(double delta)
        {
            ModelMatrix = (Matrix4) Rotation * ObjectSize * StartPosition;
            
            CurrentTime += (float)delta;
            TimeLeft = (float)Length.TotalSeconds - CurrentTime;

            CurrentFade = FadeStart >= TimeLeft ? TimeLeft : 1;
            if (TimeLeft <= 0) Parent.Remove(this);
        }

        public override void Draw(double delta)
        {
            ParticleRenderer.program.Draw(ModelMatrix, this);
        }

        public override bool AddTrigger(SMItemCollection collection)
        {
            Reload();
            return true;
        }

        public override bool RemoveTrigger(SMItemCollection collection)
        {
            return !PrettyRemove || TimeLeft <= 0;
        }

        public void Reload()
        {
            if (Amount > SMRenderer.MAX_PARTICLES)
                throw new LogException("Your Particle-Amount excedes the maximum.\nThe program is stopped, because the shader will not handle it.");
            if (Mesh.VertexCount * Amount > MaximumVertices)
            {
                if (IgnoreVerticeMax)
                    Log.Write(LogWriteType.Warning, $"Your Vertice Amount exceds the set maximum.\n\nVertice Amount: {Mesh.VertexCount * Amount}");
                else
                    throw new LogException($"Your Vertice Amount exceds the set maximum.\n\nVertice Amount: {Mesh.VertexCount * Amount}\nMaximum Vertices: {MaximumVertices}");
            }

            CurrentTime = 0;

            List<float> args = new List<float>();

            for (int i = 0; i < Amount; i++)
            {
                float x = (float)(Random.NextDouble() * (Cone.X * 2)) - Cone.X;
                float y = VariableSpeeds ? (float)(Random.NextDouble() * Speed) + MinSpeed : Speed;
                float z = (float)(Random.NextDouble() * (Cone.Y * 2)) - Cone.Y;

                args.AddRange(x,y,z);
            }

            ShaderFloats = args.ToArray();
        }
    }
}
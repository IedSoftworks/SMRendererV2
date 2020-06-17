using System;
using System.Collections.Generic;
using OpenTK;
using SMRenderer.Base.Interfaces;
using SMRenderer.Base.Models;
using SMRenderer.Base.Renderer;
using SMRenderer.Base.Types.Extensions;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Models;

namespace SMRenderer.Base.Draw
{
    public class DrawParticle : IShowObject
    {
        public const int MaximumVertices = 737280;
        public static bool IgnoreVerticeMax = false;
        public const int MaximumParticles = 2048;
        public static Random Random = new Random();

        internal float[] ShaderFloats = new float[] {};
        internal float CurrentTime;

        public Matrix4 Matrix { get; private set; }
        public Matrix4? ParentMatrix = null;

        public Model Mesh = Meshes.DefaultModel;
        public Material Material = new Material();

        public Position Position = new Position(0,0);
        public Size ObjectSize = new Size(1);
        public Rotation Rotation = new Rotation(0);

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

        public float RenderPosition { get; set; }
        public SMItemCollection Parent { get; set; }

        public void Prepare(double delta)
        {
            Matrix = (Matrix4)ObjectSize * Rotation * Position;
            if (ParentMatrix.HasValue) Matrix *= ParentMatrix.Value;

            
            CurrentTime += (float)delta;
            TimeLeft = (float)Length.TotalSeconds - CurrentTime;

            CurrentFade = FadeStart >= TimeLeft ? TimeLeft : 1;
            if (TimeLeft <= 0) Parent.Remove(this);
        }

        public void Draw(double delta)
        {
            ParticleRenderer.program.Draw(Matrix, this);
        }

        public bool AddTrigger(SMItemCollection collection)
        {
            Reload();
            return true;
        }

        public bool RemoveTrigger(SMItemCollection collection)
        {
            return !PrettyRemove || TimeLeft <= 0;
        }

        public void Reload()
        {
            if (Amount > MaximumParticles)
                throw new LogException("Your Particle-Amount excedes the maximum.\nThe program is stopped, because the shader will not handle it.");
            if (Mesh.VertexCount * Amount > MaximumVertices)
            {
                if (IgnoreVerticeMax)
                    Log.Write(LogWriteType.Warning, $"Your Vertice Amount exceds the set maximum.\n\nVertice Amount: {Mesh.VertexCount * Amount}");
                else
                    throw new LogException($"Your Vertice Amount exceds the set maximum.\n\nVertice Amount: {Mesh.VertexCount * Amount}\nMaximum Vertices: {MaximumVertices}");
            }

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
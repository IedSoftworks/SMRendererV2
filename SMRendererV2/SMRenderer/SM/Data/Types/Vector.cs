using System;
using System.Runtime.CompilerServices;
using System.Threading;
using OpenTK;
using SM.Animations;
using SM.Core;
using SM.Core.Enums;

namespace SM.Data.Types
{
    [Serializable]
    public class Vector : BaseType
    {
        private float _x = 0;
        private float _y = 0;
        private float _z = 0;
        private float _w = 0;

        #region Value Properties
        public float X
        {
            get => _x;
            set
            {
                if (Lock)
                {
                    Log.Write(LogWriteType.Warning, "Tried to set value 'X' from locked instance.", LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                    return;
                }
                RaiseChangeEvent(0, _x, value);
                _x = value;
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                if (Lock)
                {
                    Log.Write(LogWriteType.Warning, "Tried to set value 'Y' from locked instance.", LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                    return;
                }
                RaiseChangeEvent(1, _y, value);
                _y = value;
            }
        }
        public float Z
        {
            get => _z;
            set
            {
                if (Lock)
                {
                    Log.Write(LogWriteType.Warning, "Tried to set value 'Z' from locked instance.", LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                    return;
                }
                RaiseChangeEvent(2, _z, value);
                _z = value;
            }
        }

        public float W
        {
            get => _w;
            set
            {
                if (Lock)
                {
                    Log.Write(LogWriteType.Warning, "Tried to set value 'W' from locked instance.", LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                    return;
                }
                RaiseChangeEvent(3, _w, value);
                _w = value;
            }
        }


        #endregion

        public event Action<Vector, int, float, float> Change;

        public bool Lock { get; set; } = false;

        public float Length => (float) Math.Sqrt(LengthSquared);
        public float LengthSquared => (_x * _x) + (_y * _y) + (_z * _z) + (_w * _w);

        #region Constuctors

        public Vector() {}
        public Vector(Vector2 vector, float z = 0, float w = 0) : this(vector.X, vector.Y, z, w) {}
        public Vector(Vector3 vector, float w = 0) : this(vector.X, vector.Y, vector.Z, w) {}
        public Vector(Vector4 vector) : this(vector.X, vector.Y, vector.Z, vector.W) {}
        
        public Vector(float uniform) : this(uniform, uniform, uniform, uniform){}

        public Vector(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        #endregion

        public void Set(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void Set(Vector vec) => Set(vec.X, vec.Y, vec.Z, vec.W);
        public void Set(AnimationVector vec) => Set(vec.X, vec.Y, vec.Z, vec.W);

        #region Instance Calculations
        public void Add(Vector vt)
        {
            X += vt.X;
            Y += vt.Y;
            Z += vt.Z;
            W += vt.W;
        }
        public void Sub(Vector vt)
        {
            X -= vt.X;
            Y -= vt.Y;
            Z -= vt.Z;
            W -= vt.W;
        }
        public void Mul(Vector vt)
        {
            X *= vt.X;
            Y *= vt.Y;
            Z *= vt.Z;
            W *= vt.W;
        }
        public void Div(Vector vt)
        {
            X /= vt.X;
            Y /= vt.Y;
            Z /= vt.Z;
            W /= vt.W;
        }
        public void Add(float vt)
        {
            X += vt;
            Y += vt;
            Z += vt;
            W += vt;
        }
        public void Sub(float vt)
        {
            X -= vt;
            Y -= vt;
            Z -= vt;
            W -= vt;
        }
        public void Mul(float vt)
        {
            X *= vt;
            Y *= vt;
            Z *= vt;
            W *= vt;
        }
        public void Div(float vt)
        {
            X /= vt;
            Y /= vt;
            Z /= vt;
            W /= vt;
        }
        #endregion

        private void RaiseChangeEvent(int id, float old, float newVal) => Change?.Invoke(this, id, old, newVal);

        public void Normalize()
        {
            float scale = 1.0f / Length;
            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }

        public Vector Normalized()
        {
            Vector clone = Clone();
            clone.Normalize();
            return clone;
        }
        public Vector Clone() => new Vector { X = X, Y = Y, Z = Z, W = W };

        public override string ToString() => $"X:{_x}, Y:{_y}, Z:{_z}, W:{_w}";

        #region Animations
        public Animation Animate(TimeSpan time, bool repeat, params AnimationVector[] values)
        {
            AnimationStruct ani = new AnimationStruct(time, values);
            Animation animation = new Animation(this, ani, repeat);
            animation.Start();
            return animation;
        }

        public Animation Animate(AnimationStruct animationStruct, bool repeat = false)
        {
            Animation animation = new Animation(this, animationStruct, repeat);
            animation.Start();
            return animation;
        }

        #endregion

        #region Operators

        #region Addition

        public static Vector Add(Vector vt1, Vector vt2)
        {
            return new Vector()
            {
                _x = vt1._x + vt2._x,
                _y = vt1._y + vt2._y,
                _z = vt1._z + vt2._z,
                _w = vt1._w + vt2._w
            };
        }
        public static Vector Add(Vector vt1, float x)
        {
            return new Vector()
            {
                _x = vt1._x + x,
                _y = vt1._y + x,
                _z = vt1._z + x,
                _w = vt1._w + x
            };
        }
        public static Vector operator +(Vector vt1, Vector vt2) => Add(vt1, vt2);
        public static Vector operator +(Vector vt1, float vt2) => Add(vt1, vt2);

        #endregion

        #region Subtraction

        public static Vector Sub(Vector vt1, Vector vt2)
        {
            return new Vector()
            {
                _x = vt1._x - vt2._x,
                _y = vt1._y - vt2._y,
                _z = vt1._z - vt2._z,
                _w = vt1._w - vt2._w
            };
        }
        public static Vector Sub(Vector vt1, float x)
        {
            return new Vector()
            {
                _x = vt1._x - x,
                _y = vt1._y - x,
                _z = vt1._z - x,
                _w = vt1._w - x
            };
        }
        public static Vector operator -(Vector vt1, Vector vt2) => Sub(vt1, vt2);
        public static Vector operator -(Vector vt1, float vt2) => Sub(vt1, vt2);

        #endregion

        #region Multiplication
        public static Vector Mul(Vector vt1, Vector vt2)
        {
            return new Vector()
            {
                _x = vt1._x * vt2._x,
                _y = vt1._y * vt2._y,
                _z = vt1._z * vt2._z,
                _w = vt1._w * vt2._w
            };
        }
        public static Vector Mul(Vector vt1, float x)
        {
            return new Vector()
            {
                _x = vt1._x * x,
                _y = vt1._y * x,
                _z = vt1._z * x,
                _w = vt1._w * x
            };
        }
        public static Vector operator *(Vector vt1, Vector vt2) => Mul(vt1, vt2);
        public static Vector operator *(Vector vt1, float vt2) => Mul(vt1, vt2);
        #endregion

        #region Divition

        public static Vector Div(Vector vt1, Vector vt2)
        {
            return new Vector()
            {
                _x = vt1._x / vt2._x,
                _y = vt1._y / vt2._y,
                _z = vt1._z / vt2._z,
                _w = vt1._w / vt2._w
            };
        }
        public static Vector Div(Vector vt1, float x)
        {
            return new Vector()
            {
                _x = vt1._x / x,
                _y = vt1._y / x,
                _z = vt1._z / x,
                _w = vt1._w / x
            };
        }


        public static Vector operator /(Vector vt1, Vector vt2) => Div(vt1, vt2);
        public static Vector operator /(Vector vt1, float vt2) => Div(vt1, vt2);

        #endregion


        public static implicit operator Vector(Vector2 v) => new Vector { X = v.X, Y = v.Y };
        public static implicit operator Vector(Vector3 v) => new Vector { X = v.X, Y = v.Y, Z = v.Z };
        public static implicit operator Vector(Vector4 v) => new Vector { X = v.X, Y = v.Y, Z = v.Z, W = v.W };

        public static implicit operator Vector2(Vector at) => new Vector2(at.X, at.Y);
        public static implicit operator Vector3(Vector at) => new Vector3(at.X, at.Y, at.Z);
        public static implicit operator Vector4(Vector at) => new Vector4(at.X, at.Y, at.Z, at.W);
        public static implicit operator AnimationVector(Vector at) => new AnimationVector(at.X, at.Y, at.Z, at.W);

        #endregion
    }
}
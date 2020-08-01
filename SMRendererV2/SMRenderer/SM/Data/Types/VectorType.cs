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
    public class VectorType : BaseType
    {
        private float _x = 0;
        private float _y = 0;
        private float _z = 0;
        private float _w = 0;

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

        public event Action<VectorType, int, float, float> Change;

        public bool Lock { get; set; } = false;

        public float Length => (float) Math.Sqrt(LengthSquared);
        public float LengthSquared => (_x * _x) + (_y * _y) + (_z * _z) + (_w * _w);

        private void RaiseChangeEvent(int id, float old, float newVal) => Change?.Invoke(this, id, old, newVal);
        public void Normalize() => Normalize(this);

        public void Add(VectorType vt)
        {
            _x += vt.X;
            _y += vt.Y;
            _z += vt.Z;
            _w += vt.W;
        }
        public void Sub(VectorType vt)
        {
            _x -= vt.X;
            _y -= vt.Y;
            _z -= vt.Z;
            _w -= vt.W;
        }
        public void Mul(VectorType vt)
        {
            _x *= vt.X;
            _y *= vt.Y;
            _z *= vt.Z;
            _w *= vt.W;
        }
        public void Div(VectorType vt)
        {
            _x /= vt.X;
            _y /= vt.Y;
            _z /= vt.Z;
            _w /= vt.W;
        }
        public void Add(float vt)
        {
            _x += vt;
            _y += vt;
            _z += vt;
            _w += vt;
        }
        public void Sub(float vt)
        {
            _x -= vt;
            _y -= vt;
            _z -= vt;
            _w -= vt;
        }
        public void Mul(float vt)
        {
            _x *= vt;
            _y *= vt;
            _z *= vt;
            _w *= vt;
        }
        public void Div(float vt)
        {
            _x /= vt;
            _y /= vt;
            _z /= vt;
            _w /= vt;
        }

        public virtual OpenTK.Matrix2 CalcMatrix2() => new OpenTK.Matrix2();
        public virtual OpenTK.Matrix3 CalcMatrix3() => new OpenTK.Matrix3();
        public virtual OpenTK.Matrix4 CalcMatrix4() => new OpenTK.Matrix4();

        public override string ToString() => $"X:{_x}, Y:{_y}, Z:{_z}, W:{_w}";

        public static implicit operator VectorType(Vector2 v) => new VectorType{X = v.X, Y = v.Y };
        public static implicit operator VectorType(Vector3 v) => new VectorType{X = v.X, Y = v.Y, Z = v.Z };
        public static implicit operator VectorType(Vector4 v) => new VectorType{X = v.X, Y = v.Y, Z = v.Z, W = v.W};

        public static implicit operator Vector2(VectorType at) => new Vector2(at.X, at.Y);
        public static implicit operator Vector3(VectorType at) => new Vector3(at.X, at.Y, at.Z);
        public static implicit operator Vector4(VectorType at) => new Vector4(at.X, at.Y, at.Z, at.W);
        public static implicit operator AnimationVector(VectorType at) => new AnimationVector(at.X, at.Y, at.Z, at.W);

        public static implicit operator Matrix2(VectorType at) => at.CalcMatrix2();
        public static implicit operator Matrix3(VectorType at) => at.CalcMatrix3();
        public static implicit operator Matrix4(VectorType at) => at.CalcMatrix4();

        public static VectorType operator +(VectorType vt1, VectorType vt2) => Add(vt1, vt2);
        public static VectorType operator +(VectorType vt1, float vt2) => Add(vt1, vt2);


        public static VectorType operator -(VectorType vt1, VectorType vt2) => Sub(vt1, vt2);
        public static VectorType operator -(VectorType vt1, float vt2) => Sub(vt1, vt2);


        public static VectorType operator *(VectorType vt1, VectorType vt2) => Mul(vt1, vt2);
        public static VectorType operator *(VectorType vt1, float vt2) => Mul(vt1, vt2);


        public static VectorType operator /(VectorType vt1, VectorType vt2) => Div(vt1, vt2);
        public static VectorType operator /(VectorType vt1, float vt2) => Div(vt1, vt2);

        public static T Clone<T>(T obj)
        {
            VectorType source = Unsafe.As<T, VectorType>(ref obj);
            VectorType target = new VectorType {X = source.X, Y = source.Y, Z = source.Z, W = source.W};

            return Unsafe.As<VectorType, T>(ref target);
        }

        public static void Normalize(VectorType vector)
        {
            float scale = 1.0f / vector.Length;
            vector.X *= scale;
            vector.Y *= scale;
            vector.Z *= scale;
            vector.W *= scale;
        }

        public static T Normalize<T>(VectorType vector)
        {
            Normalize(vector);
            return Unsafe.As<VectorType, T>(ref vector);
        }

        public static VectorType Add(VectorType vt1, VectorType vt2)
        {
            return new VectorType()
            {
                _x = vt1._x + vt2._x,
                _y = vt1._y + vt2._y,
                _z = vt1._z + vt2._z,
                _w = vt1._w + vt2._w
            };
        }
        public static VectorType Add(VectorType vt1, float x)
        {
            return new VectorType()
            {
                _x = vt1._x + x,
                _y = vt1._y + x,
                _z = vt1._z + x,
                _w = vt1._w + x
            };
        }


        public static VectorType Sub(VectorType vt1, VectorType vt2)
        {
            return new VectorType()
            {
                _x = vt1._x - vt2._x,
                _y = vt1._y - vt2._y,
                _z = vt1._z - vt2._z,
                _w = vt1._w - vt2._w
            };
        }
        public static VectorType Sub(VectorType vt1, float x)
        {
            return new VectorType()
            {
                _x = vt1._x - x,
                _y = vt1._y - x,
                _z = vt1._z - x,
                _w = vt1._w - x
            };
        }

        public static VectorType Mul(VectorType vt1, VectorType vt2)
        {
            return new VectorType()
            {
                _x = vt1._x * vt2._x,
                _y = vt1._y * vt2._y,
                _z = vt1._z * vt2._z,
                _w = vt1._w * vt2._w
            };
        }
        public static VectorType Mul(VectorType vt1, float x)
        {
            return new VectorType()
            {
                _x = vt1._x * x,
                _y = vt1._y * x,
                _z = vt1._z * x,
                _w = vt1._w * x
            };
        }

        public static VectorType Div(VectorType vt1, VectorType vt2)
        {
            return new VectorType()
            {
                _x = vt1._x / vt2._x,
                _y = vt1._y / vt2._y,
                _z = vt1._z / vt2._z,
                _w = vt1._w / vt2._w
            };
        }
        public static VectorType Div(VectorType vt1, float x)
        {
            return new VectorType()
            {
                _x = vt1._x / x,
                _y = vt1._y / x,
                _z = vt1._z / x,
                _w = vt1._w / x
            };
        }
    }
}
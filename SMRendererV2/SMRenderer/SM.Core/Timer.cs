using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.Core
{
    /// <summary>
    /// Timer...
    /// </summary>
    [Serializable]
    public class Timer
    {
        /// <summary>
        /// The currently active timers
        /// </summary>
        public static List<Timer> ActiveTimers = new List<Timer>();

        public virtual bool AutoStop { get; } = true;
        /// <summary>
        /// The current time in seconds
        /// </summary>
        public double CurrentTime;
        /// <summary>
        /// The target time in seconds
        /// </summary>
        public double TargetTime;
        /// <summary>
        /// Triggers every time the timer ticks
        /// </summary>
        public event Action<Timer, double> Tick;
        /// <summary>
        /// Triggers at the end of the timer or when Repeat is active at each repeat.
        /// </summary>
        public event Action<Timer> End;
        /// <summary>
        /// Triggers when the timer has been stop by the programer.
        /// </summary>
        public event Action<Timer> Cancel;
        /// <summary>
        /// Determent if the timer should repeat itself
        /// </summary>
        public bool Repeat = false;

        public bool Active => ActiveTimers.Contains(this);

        /// <summary>
        /// Create a new timer.
        /// </summary>
        /// <param name="time">The target time</param>
        /// <param name="repeat">Repeating?</param>
        public Timer(TimeSpan time, bool repeat = false)
        {
            TargetTime = time.TotalSeconds;
            Repeat = repeat;
        }

        /// <summary>
        /// Create a new timer
        /// </summary>
        /// <param name="seconds">The target time in seconds</param>
        /// <param name="repeat">Repeating?</param>
        public Timer(double seconds, bool repeat = false)
        {
            TargetTime = seconds;
            Repeat = repeat;
        }

        /// <summary>
        /// Start the timer
        /// </summary>
        /// <param name="repeater">The parameter is used by the system to determinant if the timer has been repeating.</param>
        public virtual void Start(bool repeater = false)
        {
            if (Active) return;

            CurrentTime = 0;
            if (!repeater) ActiveTimers.Add(this);
        }

        /// <summary>
        /// Performs a Tick... (wow...)
        /// <para>Runs at the Window update event. Better never call this.</para>
        /// </summary>
        /// <param name="delta">Deltatime from the update event</param>
        public virtual void PerformTick(double delta)
        {
            CurrentTime += delta;
            if (AutoStop && CurrentTime >= TargetTime) Stop(false);
            Tick?.Invoke(this, delta);
        }

        /// <summary>
        /// Stops (or repeats) the timer.
        /// </summary>
        /// <param name="userCalled"></param>
        public virtual void Stop(bool userCalled = true)
        {
            if (!Active) return;

            CurrentTime = 0;
            if (Repeat && !userCalled) Start(true);
            else ActiveTimers.Remove(this);

            if (userCalled) Cancel?.Invoke(this);
            else End?.Invoke(this);
        }

        internal static void RunTick(double deltaSeconds)
        {
            ActiveTimers.ToList().ForEach(a => a.PerformTick(deltaSeconds));
        }
    }
}
namespace MomoEngine.Core.Source.Runtime
{
    public static class Time
    {
        /// <summary>
        /// 此帧开始时的时间
        /// </summary>
        public static float time { get; set; }

        /// <summary>
        /// 此帧开始时的双精度时间
        /// 开始后的时间（秒)
        /// </summary>
        public static double timeAsDouble { get; set; }

        /// <summary>
        /// 从最后一帧到当前帧的间隔（以秒为单位
        /// </summary>
        public static float deltaTime { get; set; }

        /// <summary>
        /// 自上次行为以来的时间 FixeUpdate已启动
        /// 这是开始后的秒数
        /// </summary>
        public static float fixedTime { get; set; }

        /// <summary>
        /// 自上次单行为以来的双精度时间 FixeUpdate已启动
        /// 这是开始后的秒数
        /// </summary>
        public static double fixedTimeAsDouble { get; set; }

        /// <summary>
        /// 此帧的独立于时间刻度的时间
        /// 这是开始后的秒数
        /// </summary>
        public static float unscaledTime { get; set; }

        /// <summary>
        /// 此帧的双精度时间刻度独立时间
        /// 这是开始后的秒数
        /// </summary>
        public static double unscaledTimeAsDouble { get; set; }

        /// <summary>
        /// 最后一个单粒子行为开始时的时间标度无关时间 固定更新阶段
        /// </summary>
        public static float fixedUnscaledTime { get; set; }

        public static double fixedUnscaledTimeAsDouble { get; set; }

        /// <summary>
        /// 从最后一帧到当前帧的与时间刻度无关的间隔（秒）
        /// </summary>
        public static float unscaledDeltaTime { get; set; }

        public static float fixedUnscaledDeltaTime { get; set; }

        public static float fixedDeltaTime { get; set; }

        public static float maximumDeltaTime { get; set; }

        public static float smoothDeltaTime { get; set; }

        public static float maximumParticleDeltaTime { get; set; }

        public static float timeScale { get; set; }

        public static int frameCount { get; set; }

        public static int renderedFrameCount { get; set; }

        public static float realtimeSinceStartup { get; set; }

        public static double realtimeSinceStartupAsDouble { get; set; }

        public static float captureDeltaTime { get; set; }

        public static int captureFramerate
        {
            get
            {
                return (captureDeltaTime != 0f) ? ((int)MathF.Round(1f / captureDeltaTime)) : 0;
            }
            set
            {
                captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
            }
        }

        public static bool inFixedTimeStep { get; set; }
    }
}

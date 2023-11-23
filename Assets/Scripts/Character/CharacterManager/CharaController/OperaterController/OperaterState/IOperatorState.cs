namespace Character.OperaterState
{
    public interface IOperatorState
    {
        /// <summary>
        /// State開始時に実行される
        /// </summary>
        public void HandleStart();
        /// <summary>
        /// フレーム単位で実行される、新しい状態に移行するための条件も書く
        /// </summary>
        public void HandleUpdate();
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd();
    }
}

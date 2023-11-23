namespace Character.OperaterState
{
    public interface IOperatorState
    {
        /// <summary>
        /// State�J�n���Ɏ��s�����
        /// </summary>
        public void HandleStart();
        /// <summary>
        /// �t���[���P�ʂŎ��s�����A�V������ԂɈڍs���邽�߂̏���������
        /// </summary>
        public void HandleUpdate();
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public void HandleEnd();
    }
}

using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "LogEffect", menuName = "AbilitySystem/Effects/LogEffect")]
    public class Effect_Log : Effect
    {
        [SerializeField] private LogLevel _logLevel;
        [SerializeField] private string _message;

        public override void Cancel()
        {
        }

        public override void End()
        {
        }

        public override void Execute(Unit source, Unit target)
        {
            ILogr _logger = new ConsoleLogger();
            _logger.Log(_message, _logLevel);

            if (target != null)
            {
                _logger.Log(target.ToString(), _logLevel);
            }
        }
    }
}
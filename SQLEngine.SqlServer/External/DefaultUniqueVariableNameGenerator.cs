using System.Threading;

namespace SQLEngine.SqlServer
{
    public class DefaultUniqueVariableNameGenerator : IUniqueVariableNameGenerator
    {
        private int _last;
        public string New()
        {
            Interlocked.Increment(ref _last);
            return "v" + _last;
        }

        public void Reset()
        {
            _last = 0;
        }
    }
}
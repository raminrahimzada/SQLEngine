using System.Threading;

namespace SQLEngine.SqlServer
{
    public class SqlEscapeStrategy : IEscapeStrategy
    {
        public string Escape(string name)
        {
            if (name.Contains(' ')) return $"[{name}]";
            if (!char.IsLetter(name[0]))
            {
                return $"[{name}]";
            }
            return name;
        }
    }
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
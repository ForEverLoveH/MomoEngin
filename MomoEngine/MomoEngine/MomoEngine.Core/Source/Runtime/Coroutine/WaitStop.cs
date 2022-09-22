using System.Collections;

namespace MomoEngine.Core
{
    public class WaitStop : IEnumerator
    {
        public object Current => null;

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
            
        }
    }
}

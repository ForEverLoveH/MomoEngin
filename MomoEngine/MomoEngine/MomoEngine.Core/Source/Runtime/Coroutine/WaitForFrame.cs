using System.Collections;

namespace MomoEngine.Core
{
    public class WaitForFrame : IEnumerator
    {
        public int waitFrame = 0;

        public WaitForFrame(int frame)
        {
            waitFrame = frame;
        }

        public object Current => null;

        public bool MoveNext()
        {
            waitFrame--;
            return waitFrame >= 0;
        }

        public void Reset()
        {
        }
    }
}

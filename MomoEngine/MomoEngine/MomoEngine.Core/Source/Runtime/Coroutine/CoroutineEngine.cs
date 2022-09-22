using System.Collections;
using System.Collections.Generic;

namespace MomoEngine.Core
{
    public class CoroutineEngine
    {
        private static CoroutineEngine _instance = null;

        public static CoroutineEngine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CoroutineEngine();

                return _instance;
            }
        }

        List<(string, Coroutine)> coroutines = new List<(string, Coroutine)>();

        public void CoroutineUpdate()
        {
            lock (coroutines)
            {
                for (int i = 0; i < coroutines.Count; i++)
                {
                    (string methodName, Coroutine coroutine) coroutineValueTuple = coroutines[i];
                    if (coroutineValueTuple.coroutine.MoveNext() == false)
                    {
                        coroutines.RemoveAt(i);
                        i--;
                    }
                }
            }
        }


        public Coroutine StartCoroutine(string methodName, IEnumerator enumerator)
        {
            lock (coroutines)
            {
                Coroutine coroutine = new Coroutine(enumerator);
                coroutines.Add((methodName, coroutine));
                return coroutine;
            }

        }

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            lock (coroutines)
            {
                Coroutine coroutine = new Coroutine(enumerator);
                coroutines.Add((enumerator.GetType().FullName, coroutine));
                return coroutine;
            }

        }


        public void StopCoroutine(Coroutine coroutine)
        {
            lock (coroutines)
            {
                for (int i = 0; i < coroutines.Count; i++)
                {
                    (string methodName, Coroutine coroutine) coroutineValueTuple = coroutines[i];
                    if (coroutineValueTuple.methodName == coroutine.FullName)
                    {
                        coroutines.RemoveAt(i);
                        i--;
                    }
                }
            }

        }

        public void StopCoroutine(IEnumerator coroutine)
        {
            lock (coroutines)
            {
                for (int i = 0; i < coroutines.Count; i++)
                {
                    (string methodName, Coroutine coroutine) coroutineValueTuple = coroutines[i];
                    if (coroutineValueTuple.methodName == coroutine.GetType().FullName)
                    {
                        coroutines.RemoveAt(i);
                        i--;
                    }
                }
            }

        }

        public void StopAllCoroutines()
        {
            lock (coroutines)
            {
                coroutines.Clear();
            }
        }

    }
}

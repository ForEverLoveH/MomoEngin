using System.Reflection;


namespace MomoEngine.Core
{
    public class Engine
    {
        public delegate void BehavioursLoadedDelegate(ref List<(object, List<BehaviourBase.BehaviourType>)> values);
        private static List<(object, List<BehaviourBase.BehaviourType>)> BehaviourObjects = new List<(object, List<BehaviourBase.BehaviourType>)>();
        const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public static float fixedDeltaTime = 0.01f;

        public void Run(BehavioursLoadedDelegate behavioursLoadedDelegate = null)
        {
            Initialize();
            if (behavioursLoadedDelegate != null)
            {
                behavioursLoadedDelegate(ref BehaviourObjects);
            }
            StartEngine();
        }


        public void RemoveBehaviour(Type type)
        {
            lock (BehaviourObjects)
            {
                for (int i = 0; i < BehaviourObjects.Count; i++)
                {
                    (object obj, List<BehaviourBase.BehaviourType> behaviourTypes) BehaviourObject = BehaviourObjects[i];
                    if (BehaviourObject.obj.GetType() == type)
                    {
                        BehaviourInvoking(i, BehaviourBase.BehaviourType.OnDestory);
                        BehaviourObjects.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void RemoveBehaviour(String typeName)
        {
            lock (BehaviourObjects)
            {
                for (int i = 0; i < BehaviourObjects.Count; i++)
                {
                    (object obj, List<BehaviourBase.BehaviourType> behaviourTypes) BehaviourObject = BehaviourObjects[i];
                    if (BehaviourObject.obj.GetType().Name == typeName)
                    {
                        BehaviourInvoking(i, BehaviourBase.BehaviourType.OnDestory);
                        BehaviourObjects.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void ApplicationQuit()
        {
            BehaviourInvoking(BehaviourBase.BehaviourType.OnApplicationQuit);
        }

        private void Initialize()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type childType in assembly.GetTypes())
                {
                    Type type = childType;
                    while (type.BaseType != null)
                    {
                        if (type.BaseType == typeof(Behaviour))
                        {
                            object obj = Activator.CreateInstance(childType);
                            PropertyInfo property = null;
                            while (type != null)
                            {
                                property = type.GetProperty("Behaviours", InstanceBindFlags);
                                if (property != null)
                                {
                                    break;
                                }
                                type = type.BaseType;
                            }
                            List<BehaviourBase.BehaviourType> Behaviours = null;
                            if (property != null)
                            {
                                Behaviours = (List<BehaviourBase.BehaviourType>)property.GetValue(obj);
                            }
                            BehaviourObjects.Add((obj, Behaviours));
                        }
                        type = type.BaseType;
                    }
                }
            }
        }

        private void StartEngine()
        {
            BehaviourInvoking(BehaviourBase.BehaviourType.Awake);
            BehaviourInvoking(BehaviourBase.BehaviourType.Start);
            Task.Run(() =>
            {
                while (true)
                {
                    DateTime current = DateTime.Now;
                    while (current.AddMilliseconds(fixedDeltaTime * 1000) > DateTime.Now) { }
                    BehaviourInvoking(BehaviourBase.BehaviourType.FixedUpdate);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    BehaviourInvoking(BehaviourBase.BehaviourType.Update);
                    CoroutineEngine.Instance.CoroutineUpdate();
                    BehaviourInvoking(BehaviourBase.BehaviourType.LateUpdate);
                }
            });
        }


        private void BehaviourInvoking(BehaviourBase.BehaviourType behaviourType)
        {
            for (int i = 0; i < BehaviourObjects.Count; i++)
            {
                BehaviourInvoking(i, behaviourType);
            }
        }

        private void BehaviourInvoking(int index, BehaviourBase.BehaviourType behaviourType)
        {
            lock (BehaviourObjects)
            {
                (object obj, List<BehaviourBase.BehaviourType> behaviourTypes) BehaviourObject = BehaviourObjects[index];
                dynamic obj = BehaviourObject.obj;
                if (BehaviourObject.behaviourTypes != null)
                {
                    if (BehaviourObject.behaviourTypes.Contains(behaviourType))
                    {
                        Type type = obj.GetType();
                        MethodInfo Method = type.GetMethod(behaviourType.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
                        if (Method != null)
                        {
                            if (Method.IsStatic)
                            {
                                Method.Invoke(null, null);
                            }
                            else
                            {
                                Method.Invoke(obj, null);
                            }
                        }
                    }
                }
            }
        }
    }
}

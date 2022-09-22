using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MomoEngine.Core
{
    public class Behaviour : BehaviourBase
    {
        private List<BehaviourType> Behaviours { get; set; } = new List<BehaviourType>();

        private List<string> InvokeMethods = new List<string>();

        public Behaviour()
        {
            ConstructorCheck(this);
        }

        /// <summary>
        /// 是否调用
        /// </summary>
        /// <returns></returns>
        public bool IsInvoking()
        {
            return Internal_IsInvokingAll(this);
        }

        /// <summary>
        /// 取消调用
        /// </summary>
        public void CancelInvoke()
        {
            Internal_CancelInvokeAll(this);
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="time">时间</param>
        public void Invoke(string methodName, float time)
        {
            InvokeDelayed(this, methodName, time);
        }


        /// <summary>
        /// 取消调用
        /// </summary>
        /// <param name="methodName">方法名称</param>
        public void CancelInvoke(string methodName)
        {
            CancelInvoke(this, methodName);
        }

        /// <summary>
        /// 是否调用
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <returns></returns>
        public bool IsInvoking(string methodName)
        {
            return IsInvoking(this, methodName);
        }


        /// <summary>
        /// 启动协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                throw new NullReferenceException("routine is null");
            }

            if (!IsObjectBehaviour(this))
            {
                throw new ArgumentException("Coroutines can only be stopped on a Behaviour");
            }

            return StartCoroutineManaged(routine);
        }


        /// <summary>
        /// 停止协程
        /// </summary>
        /// <param name="routine"></param>
        public void StopCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                throw new NullReferenceException("routine is null");
            }

            if (!IsObjectBehaviour(this))
            {
                throw new ArgumentException("Coroutines can only be stopped on a Behaviour");
            }

            StopCoroutineFromEnumeratorManaged(routine);
        }

        /// <summary>
        /// 停止协程
        /// </summary>
        /// <param name="routine"></param>
        public void StopCoroutine(Coroutine routine)
        {
            if (routine == null)
            {
                throw new NullReferenceException("routine is null");
            }

            if (!IsObjectBehaviour(this))
            {
                throw new ArgumentException("Coroutines can only be stopped on a Behaviour");
            }

            StopCoroutineManaged(routine);
        }

        /// <summary>
        /// 停止所有协程
        /// </summary>
        public void StopAllCoroutines()
        {
            CoroutineEngine.Instance.StopAllCoroutines();
        }


        /// <summary>
        /// 构造函数检查
        /// </summary>
        /// <param name="self"></param>
        private static void ConstructorCheck(Object self)
        {
            dynamic obj = self;
            Type type = obj.GetType();
            MethodInfo AwakeMethod = type.GetMethod(BehaviourType.Awake.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (AwakeMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.Awake);
            }
            MethodInfo StartMethod = type.GetMethod(BehaviourType.Start.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (StartMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.Start);
            }
            MethodInfo FixedUpdateMethod = type.GetMethod(BehaviourType.FixedUpdate.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (FixedUpdateMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.FixedUpdate);
            }
            MethodInfo UpdateMethod = type.GetMethod(BehaviourType.Update.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (UpdateMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.Update);
            }
            MethodInfo LateUpdateMethod = type.GetMethod(BehaviourType.LateUpdate.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (LateUpdateMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.LateUpdate);
            }
            MethodInfo OnDestoryMethod = type.GetMethod(BehaviourType.OnDestory.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (OnDestoryMethod != null)
            {
                obj.Behaviours.Add(BehaviourType.OnDestory);
            }
            MethodInfo OnApplicationQuit = type.GetMethod(BehaviourType.OnApplicationQuit.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (OnApplicationQuit != null)
            {
                obj.Behaviours.Add(BehaviourType.OnApplicationQuit);
            }
        }

        /// <summary>
        /// 内部 取消全部调用
        /// </summary>
        /// <param name="self"></param>
        private static void Internal_CancelInvokeAll(Behaviour self)
        {
            dynamic obj = self;
            obj.InvokeMethods.Clear();
        }

        /// <summary>
        /// 内部 是否全部调用
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        private static bool Internal_IsInvokingAll(Behaviour self)
        {
            dynamic obj = self;
            return obj.InvokeMethods.Count != 0;
        }

        /// <summary>
        /// 调用延迟
        /// </summary>
        /// <param name="self"></param>
        /// <param name="methodName">方法名称</param>
        /// <param name="time">时间</param>
        /// <param name="repeatRate">重复率</param>
        private static void InvokeDelayed(Behaviour self, string methodName, float time)
        {
            dynamic obj = self;
            obj.InvokeMethods.Add(methodName);
            Type type = obj.GetType();
            MethodInfo Method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, new Type[0]);
            if (Method != null)
            {
                Task.Run(() =>
                {
                    DateTime current = DateTime.Now;
                    while (current.AddMilliseconds(time * 1000) > DateTime.Now){}
                    if (obj.InvokeMethods.Contains(methodName))
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
                });
            }
        }

        /// <summary>
        /// 取消调用
        /// </summary>
        /// <param name="self"></param>
        /// <param name="methodName">方法名称</param>
        private static void CancelInvoke(Behaviour self, string methodName)
        {
            dynamic obj = self;
            obj.InvokeMethods.Remove(methodName);
        }

        /// <summary>
        /// 是否调用
        /// </summary>
        /// <param name="self"></param>
        /// <param name="methodName">方法名称</param>
        /// <returns></returns>
        private static bool IsInvoking(Behaviour self, string methodName)
        {
            dynamic obj = self;
            return obj.InvokeMethods.Contains(methodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsObjectBehaviour(Object self)
        {
            dynamic obj = self;
            Type type = obj.GetType();
            while (type.BaseType != null)
            {
                if (type.BaseType.Name == "Behaviour")
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }


        /// <summary>
        /// 启动协程管理
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        private Coroutine StartCoroutineManaged(IEnumerator enumerator)
        {
            return CoroutineEngine.Instance.StartCoroutine(enumerator);
        }

        /// <summary>
        /// 停止协程管理
        /// </summary>
        /// <param name="routine"></param>
        private void StopCoroutineManaged(Coroutine routine)
        {
            CoroutineEngine.Instance.StopCoroutine(routine);
        }

        /// <summary>
        /// 停止托管枚举器中的协程
        /// </summary>
        /// <param name="routine"></param>
        private void StopCoroutineFromEnumeratorManaged(IEnumerator routine)
        {
            CoroutineEngine.Instance.StopCoroutine(routine);
        }

    }
}

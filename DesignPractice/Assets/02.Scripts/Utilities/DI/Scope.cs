/*
 * 의존성 주입 패턴 방식은 크게 두가지
 * 
 * 1. 의존을 필요로 하는 객체가 컨테이너를 직접 검색하여 Resolve -> 성능 good
 * 2. Scope 객체가 현재 Scope 의 모든 객체를 탐색하며 의존을 필요로 하는 필드를 찾고 Resolve -> 생산성 good
 */

using System;
using DP.Contexts;
using UnityEngine;
using System.Reflection;

namespace DP.Utilities.DI
{
    /// <summary>
    /// 인스턴스들의 생명 주기 범위
    /// 의존성 주입에 사용할 인스턴스들이 언제 생성돼서 언제 파괴되는지까지의 범위
    /// </summary>
    public abstract class Scope : MonoBehaviour
    {
        protected Container Container;

        private void Awake()
        {
            Container = new Container();
            Register();
            InjectAll();
        }

        /// <summary>
        /// 주입에 사용할 객체들을 미리 준비해놓는 로직
        /// </summary>
        public virtual void Register()
        {
        }

        void InjectAll()
        {
            MonoBehaviour[] monoBehaviours = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                Inject(monoBehaviour);
            }
        }

        /// <summary>
        /// [inject] attribute 가 붙은 필드를 탐색하여 의존성을 주입
        /// </summary>
        /// <param name="obj"> 주입할 대상 </param>
        void Inject(object obj)
        {
            Type type = obj.GetType(); // 주입할 대상의 type 데이터

            // 대상의 타입 정보에서 모든 멤버변수 정보 받아옴
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            // 모든 멤버 변수 정보들 중에 Inject Attribute 가 붙어있는 멤버 변수에 대해서 객체 참조 연결함
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.GetCustomAttribute<InjectAttribute>() != null)
                {
                    // FieldInfo.SetValue(object target, object value)
                    // 멤버 변수에 값을 쓰는데, 대상이 target 값이 value
                    fieldInfo.SetValue(obj, Container.Resolve(fieldInfo.FieldType));
                }
            }
        }
    }
}
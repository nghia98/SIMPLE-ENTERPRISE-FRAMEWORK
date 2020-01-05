using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework
{
    public class IoCContainer
    {
        // Chứa các Interface và Class tương ứng
        private static readonly Dictionary<Type, object> ResgisteredClasses = new Dictionary<Type, object>();

        public static void RegisterType<TInterface, TModule>()
        {
            SetClass(typeof(TInterface), typeof(TModule));
        }

        private static void SetClass(Type interfaceType, Type classType)
        {
            //Kiểm tra class implement interface chưa
            if (!interfaceType.IsAssignableFrom(classType))
            {
                throw new Exception("Class chưa implement Interface !");
            }

            //Tìm contructor đầu tiên
            var firstConstructor = classType.GetConstructors()[0];
            object obj = null;
            //Nếu không có tham số
            if (!firstConstructor.GetParameters().Any())
            {
                //Khởi tạo Class
                obj = firstConstructor.Invoke(null);
            }
            else
            {
                //Lấy các tham số của contructor
                var constructorParameters = firstConstructor.GetParameters();
                var classDependecies = new List<object>();
                foreach (var parameter in constructorParameters)
                {
                    var dependency = GetClass(parameter.ParameterType);
                    classDependecies.Add(dependency);
                }
                //Inject các dependency vào contructor của class
                obj = firstConstructor.Invoke(classDependecies.ToArray());
            }
            ResgisteredClasses.Add(interfaceType, obj);
        }

        public static T Resolve<T>()
        {
            return (T)GetClass(typeof(T));
        }

        private static object GetClass(Type interfaceType)
        {
            if (ResgisteredClasses.ContainsKey(interfaceType))
            {
                return ResgisteredClasses[interfaceType];
            }
            throw new Exception("Class chưa được đăng kí");
        }
    }
}

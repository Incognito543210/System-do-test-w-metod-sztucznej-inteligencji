using System.Reflection;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllReader
    {
        public void Run(object targetClass, string testFunction, object[] parameters);
        public object GetClassObject();
    }
}

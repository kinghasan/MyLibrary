using System;
using System.Reflection;

namespace Aya.Types
{
    /// <summary>
    /// –Ú¡–ªØ¿‡
    /// </summary>
    [Serializable]
    public class TypeReference
    {
        public string AssemblyName;
        public string TypeName;

        #region Construct
        public TypeReference() { }

        public TypeReference(string assemblyName, string typeName)
        {
            AssemblyName = assemblyName;
            TypeName = typeName;
        }
        #endregion

        #region Cache
        public Assembly Assembly
        {
            get
            {
                return CacheAssembly();
            }
        }

        public Type Type
        {
            get
            {
                return CacheType();
            }
        }

        private Assembly _cacheAssembly;
        private string _cacheAssemblyName;
        public Assembly CacheAssembly()
        {
            if (_cacheAssembly != null && _cacheAssemblyName == AssemblyName) return _cacheAssembly;
            _cacheAssemblyName = AssemblyName;
            _cacheAssembly = TypeCaches.GetAssemblyByName(_cacheAssemblyName);
            return _cacheAssembly;
        }

        private Type _cacheTypes;
        private string _cacheTypeName;
        public Type CacheType()
        {
            if (_cacheTypes != null && _cacheTypeName == TypeName) return _cacheTypes;
            _cacheTypeName = TypeName;
            _cacheTypes = Assembly.GetType(_cacheTypeName);
            return _cacheTypes;
        }
        #endregion

        #region Override Operator

        public static implicit operator Type(TypeReference typeReference) => typeReference.Type;
        public static implicit operator TypeReference(Type type) => new TypeReference(type.Assembly.FullName, type.Name);

        #endregion
    }
}

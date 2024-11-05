// https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types

namespace Core
{
    [Serializable()]
    public abstract class Enumeration : IComparable
    {
        public string Name { get; set; }
        public int Id { get; set; }

        protected Enumeration(int iID, string sName)
        {
            Id = iID;
            Name = sName;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object? obj)
        {
            Enumeration? otherValue = obj as Enumeration;
            if (otherValue is null)
                return false;

            return GetType().Equals(obj?.GetType()) && Id.Equals(otherValue.Id);
        }

        public static bool operator ==(Enumeration o1, Enumeration o2)
        {
            if (o1 is null || o2 is null)
                return false;

            return o1.Equals(o2);
        }

        public static bool operator !=(Enumeration o1, Enumeration o2)
        {
            if (o1 is null || o2 is null)
                return true;

            return !o1.Equals(o2);
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration)other).Id);
        }

        //public static implicit operator int(Enumeration e)
        //{
        //    return e.Id;
        //}

        //public static explicit operator Enumeration(int id)
        //{
        //    return GetById(id);
        //}

        //public static implicit operator string(Enumeration e)
        //{
        //    return e.Name;
        //}

        //public static implicit operator Enumeration(string name)
        //{
        //    return GetByName(name);
        //}

    }

    [Serializable()]
    public abstract class Enumeration<T> : IComparable where T : Enumeration<T>
    {

        // Private fields for caching enumeration enumerations
        private static ISet<T> m_oEnumerations = null;
        private static IDictionary<int, T> m_dctEnumerations = null;

        public string Name { get; set; }
        public int Id { get; set; }

        protected Enumeration(int iID, string sName)
        {
            Id = iID;
            Name = sName;
        }

        public override string ToString()
        {
            return Name;
        }

        public static ISet<T> GetAll()
        {
            if (m_oEnumerations == null)
            {
                HashSet<T> hs = new HashSet<T>();
                GetAllInternal().ForEach(t => hs.Add(t));
                m_oEnumerations = hs;
            }
            else
            {
                m_oEnumerations.GetEnumerator().Reset();// TODO check if this is necessary?  do subsequent calls need this or is the enumerator regenerated every time the list is enumerated?
            }

            return m_oEnumerations;
        }

        /// <summary>
        /// This is used internally to build an enumerable of all static Enumeration members
        /// of the derived Enumeration class.  This only builds the list on the first call;
        /// Subsequent calls use a cached list from the initial call.  Because the static members
        /// by definition can't be added or removed during runtime, this is safe to cache.
        /// </summary>
        /// <returns>An enumeration of all static enum members in the derived-most Enumeration class</returns>
        protected static IEnumerable<T> GetAllInternal()
        {
            foreach (System.Reflection.FieldInfo info in typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly).Where(field => field.ReflectedType == typeof(T)))
            {
                T locatedValue = info.GetValue(null) as T;
                if (locatedValue != null)
                    yield return locatedValue;
            }
        }

        public static T GetById(int iID)
        {
            return GetAll().FirstOrDefault(item => item.Id == iID);
        }

        public static T GetById(int? iID)
        {
            if (iID == null)
                return null;

            return GetById(iID.Value);
        }

        public static T GetByName(string name)
        {
            return GetAll().FirstOrDefault(item => item.Name == name);
        }

        public static IDictionary<int, T> GetDictionary()
        {
            if (m_dctEnumerations == null)
            {
                m_dctEnumerations = new Dictionary<int, T>();
                GetAll().ForEach((item) => { if (item != null) m_dctEnumerations.Add(item.Id, item); });
            }

            return m_dctEnumerations;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object? obj)
        {
            Enumeration<T>? otherValue = obj as Enumeration<T>;
            if (otherValue is null)
                return false;

            return GetType().Equals(obj?.GetType()) && Id.Equals(otherValue.Id);
        }

        public static bool operator ==(Enumeration<T> o1, Enumeration<T> o2)
        {
            if (o1 is null || o2 is null)
                return false;

            return o1.Equals(o2);
        }

        public static bool operator !=(Enumeration<T> o1, Enumeration<T> o2)
        {
            if (o1 is null || o2 is null)
                return true;

            return !o1.Equals(o2);
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration<T>)other).Id);
        }

        public static implicit operator int(Enumeration<T> e)
        {
            return e.Id;
        }

        public static explicit operator Enumeration<T>(int id)
        {
            return GetById(id);
        }

        public static implicit operator string(Enumeration<T> e)
        {
            return e.Name;
        }

        public static implicit operator Enumeration<T>(string name)
        {
            return GetByName(name);
        }

    }

}

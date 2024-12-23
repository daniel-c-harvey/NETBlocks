using System.ComponentModel;

namespace NetBlocks.Utilities.Binding
{
    public class BindingRepo
    {
        private Dictionary<Type, Dictionary<string, BindingHelper<object>>> bindings = new();

        public void Add<T>(BindingHelper<T> datum)
            where T : class
        {
            Dictionary<string, BindingHelper<object>>? olist = null;

            if (bindings.TryGetValue(typeof(T), out olist))
            {
                BindingHelper<object>? obind = null;
                
                if (!olist.TryGetValue(datum.Key, out obind))
                {
                    olist.Add(datum.Key, datum);
                }
            }
        }

        public BindingHelper<T>? Get<T>(string key)
        {
            Dictionary<string, BindingHelper<object>>? olist = null;
            BindingHelper<T>? bind = null;

            if (bindings.TryGetValue(typeof(T), out olist))
            {
                Dictionary<string, BindingHelper<T>> blist = new(olist.Select(b => new KeyValuePair<string, BindingHelper<T>>(b.Key, (BindingHelper<T>)b.Value)));
                blist.TryGetValue(key, out bind);
            }
                
            return bind;
        }
    }
    
    public class BindingHelper<T>
    {
        private readonly Binding source;
        private readonly Binding target;

        public BindingHelper(INotifyPropertyChanged source, 
                             Func<T> sourceGetter,
                             Action<T> sourceSetter, 
                             string sourcePropertyName, 
                             INotifyPropertyChanged target,
                             Func<T> targetGetter,
                             Action<T> targetSetter, 
                             string targetPropertyName)
        {
            this.source = new(sourceSetter, source, sourceGetter, sourcePropertyName);
            this.target = new(targetSetter, target, targetGetter, targetPropertyName);

            source.PropertyChanged += HandleSourcePropertyChanged;
            target.PropertyChanged += HandleTargetPropertyChanged;
        }

        private void HandleSourcePropertyChanged(object? sender, PropertyChangedEventArgs e) =>
            UpdateTargetIfPropertyMatches(e.PropertyName, source, target);
        private void HandleTargetPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
            UpdateTargetIfPropertyMatches(e.PropertyName, target, source);

        private void UpdateTargetIfPropertyMatches(string? updatedProperty, Binding source, Binding target)
        {
            if (updatedProperty == source.propertyName)
            {
                target.setter(source.getter());
            }
        }

        public void Unbind()
        {
            source.datasource.PropertyChanged -= HandleSourcePropertyChanged;
            target.datasource.PropertyChanged -= HandleTargetPropertyChanged;
        }

        public string Key => $"{source.propertyName}::{target.propertyName}";

        private class Binding
        {
            public readonly Action<T> setter;
            public readonly INotifyPropertyChanged datasource;
            public readonly Func<T> getter;
            public readonly string propertyName;

            public Binding(Action<T> setter, INotifyPropertyChanged datasource, Func<T> getter, string propertyName)
            {
                this.setter = setter;
                this.datasource = datasource;
                this.getter = getter;
                this.propertyName = propertyName;
            }
        }

        public static implicit operator BindingHelper<T>(BindingHelper<object> v)
        {
             return new BindingHelper<T>(v.source.datasource, 
                                         () => (T)v.source.getter(), 
                                         (value) => v.source.setter(value), 
                                         v.source.propertyName, 
                                         v.target.datasource, 
                                         () => (T)v.target.getter(), 
                                         (value) => v.target.setter(value), 
                                         v.target.propertyName);
        }

        public static implicit operator BindingHelper<object>(BindingHelper<T> v)
        {
            return new BindingHelper<object>(v.source.datasource,
                                        () => v.source.getter(),
                                        (value) => v.source.setter((T)value),
                                        v.source.propertyName,
                                        v.target.datasource,
                                        () => v.target.getter(),
                                        (value) => v.target.setter((T)value),
                                        v.target.propertyName);
        }
    }
}
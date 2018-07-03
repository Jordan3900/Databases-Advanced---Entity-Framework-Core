namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    internal class ChangeTracker<T>
        where T: class, new()
    {
        private readonly List<T> allEntites;

        private readonly List<T> added;

        private readonly List<T> removed;

        public ChangeTracker(IEnumerable<T> entites)
        {
            this.added = new List<T>();
            this.removed = new List<T>();

            this.allEntites = CloneEntites(entites);
        }

        public IReadOnlyCollection<T> AllEntites => this.allEntites.AsReadOnly();

        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();

        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();

        public void Add(T item) => this.added.Add(item);

        public void Remove(T item) => this.removed.Add(item);

        public IEnumerable<T> GetModifiedEntites(DbSet<T> dbSet)
        {
            var modifiedEntites = new List<T>();

            var primaryKeys = typeof(T).GetProperties().Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();

            foreach (var proxyEntity in this.AllEntites)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                var entity = dbSet.Entities.Single(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));

                var isModified = IsModified(proxyEntity, entity);
                if (isModified)
                {
                    modifiedEntites.Add(entity);
                }
            }

            return modifiedEntites;
        }

        private bool IsModified(T proxyEntity, object entity)
        {
            var monitoredProperties = typeof(T).GetProperties().Where(pi => DbContex.AllowedTypes.Contains(pi.PropertyType));

            var modifiedProperties = monitoredProperties.Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)));

            var isModified = modifiedProperties.Any();

            return isModified;
        }

        private IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        {
            return primaryKeys.Select(pk => pk.GetValue(entity));
        }

        private List<T> CloneEntites(IEnumerable<T> entites)
        {
            var clonedEntities = new List<T>();

            var propertiesToClone = typeof(T).GetProperties().Where(pi => DbContex.AllowedTypes.Contains(pi.PropertyType)).ToArray();

            foreach (var entity in entites)
            {
                var clonedEntity = Activator.CreateInstance<T>();

                foreach (var property in propertiesToClone)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(clonedEntity, value);
                }
                
            }
            return clonedEntities;
        }
    }
}
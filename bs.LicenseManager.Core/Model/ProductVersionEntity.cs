using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace bs.LicensesManager.Core.Model
{
    public class ProductVersionEntity : IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Version { get; set; }
        public virtual bool Active { get; set; }
        public virtual ProductEntity Product { get; set; }

        public class Map : ClassMapping<ProductVersionEntity>
        {
            public Map()
            {
                Table("Versions");

                Id(x => x.Id, x =>
                {
                    x.Generator(Generators.Guid);
                    x.Type(NHibernateUtil.Guid);
                    x.Column("Id");
                    x.UnsavedValue(Guid.Empty);
                });

                Property(p => p.Version, map =>
                {
                    map.UniqueKey("UQ__Versions_Product");
                    map.NotNullable(true);
                });

                ManyToOne(x => x.Product, map =>
                {
                    map.Column("ProductId");
                    map.ForeignKey("FK__Versions_Product");
                    map.UniqueKey("UQ__Versions_Product");
                    map.NotNullable(true);
                });

                Property(p => p.Active);
            }
        }
    }
}

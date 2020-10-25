using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace bs.LicenseManager.Core.Model
{
    public class ProductFeatureEntity : IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual ProductEntity Product { get; set; }
        public virtual IList<LicenseTokenFeatureEntity> LicenseTokenFeatures { get; set; }
        public virtual string Key { get; set; }
        public virtual bool Active { get; set; }

        public override string ToString()
        {
            return $"Feature: {Key}";
        }
        public class Map : ClassMapping<ProductFeatureEntity>
        {
            public Map()
            {
                Table("Features");

                Id(x => x.Id, x =>
                {
                    x.Generator(Generators.Guid);
                    x.Type(NHibernateUtil.Guid);
                    x.Column("Id");
                    x.UnsavedValue(Guid.Empty);
                });

                Property(p => p.Key, map =>
                {
                    map.UniqueKey("UQ_Features_Product");
                    map.NotNullable(true);
                    map.Length(32);
                    map.Type(NHibernateUtil.StringClob);
                });
                ManyToOne(x => x.Product, map =>
                {
                    map.Column("ProductId");
                    map.ForeignKey("FK__Features_Product");
                    map.UniqueKey("UQ_Features_Product");
                    map.NotNullable(true);
                });

                Bag(p => p.LicenseTokenFeatures, b =>
                {
                    b.Key(k => k.Column("ProductFeatureId"));
                },
                map => map.OneToMany(a => a.Class(typeof(LicenseTokenFeatureEntity))));


                Property(p => p.Active);
            }
        }
    }
}

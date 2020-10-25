using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace bs.LicenseManager.Core.Model
{
    public class ProductEntity : IPersistentEntity
    {
        public ProductEntity()
        {
            Versions = new List<ProductVersionEntity>();
            Features = new List<ProductFeatureEntity>();
            LicenseTokens = new List<LicenseTokenEntity>();
        }
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Active { get; set; }
        public virtual IList<ProductVersionEntity> Versions { get; set; }
        public virtual IList<ProductFeatureEntity> Features { get; set; }
        public virtual IList<LicenseTokenEntity> LicenseTokens { get; set; }

        public class Map : ClassMapping<ProductEntity>
        {
            public Map()
            {
                Table("Products");
                Id(x => x.Id, x =>
                {
                    x.Generator(Generators.Guid);
                    x.Type(NHibernateUtil.Guid);
                    x.Column("Id");
                    x.UnsavedValue(Guid.Empty);
                });

                Property(p => p.Name, map =>
                {
                    map.Unique(true);
                    map.NotNullable(true);
                    map.Length(50);

                });
                Property(p => p.Description, map =>
                {
                    map.Length(500);
                    map.Type(NHibernateUtil.StringClob);
                });
                Property(p => p.Active);

                Bag(p => p.Versions, b =>
                {
                    b.Key(k => k.Column("ProductId"));
                },
                map => map.OneToMany(a => a.Class(typeof(ProductVersionEntity))));


                Bag(p => p.Features, b =>
                {
                    b.Key(k => k.Column("ProductId"));
                },
                map => map.OneToMany(a => a.Class(typeof(ProductFeatureEntity))));

                Bag(p => p.LicenseTokens, b =>
                {
                    b.Key(k => k.Column("ProductId"));
                },
                map => map.OneToMany(a => a.Class(typeof(LicenseTokenEntity))));
            }
        }
    }
}

using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace bs.LicenseManager.Core.Model
{
    public class LicenseTokenFeatureEntity : IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual ProductFeatureEntity ProductFeature { get; set; }
        public virtual LicenseTokenEntity LicenseToken { get; set; }
        public virtual string Value { get; set; }
        public virtual bool Active { get; set; }

        public class Map : ClassMapping<LicenseTokenFeatureEntity>
        {
            public Map()
            {
                Table("LicenseTokenFeatures");

                Id(x => x.Id, x =>
                {
                    x.Generator(Generators.Guid);
                    x.Type(NHibernateUtil.Guid);
                    x.Column("Id");
                    x.UnsavedValue(Guid.Empty);
                });

                Property(p => p.Value, map =>
                {
                    map.NotNullable(true);
                    map.Length(64);
                    map.Type(NHibernateUtil.StringClob);
                });

                ManyToOne(x => x.ProductFeature, map =>
                {
                    map.Column("ProductFeatureId");
                    map.ForeignKey("FK__LicenseTokenFeatures_ProductFeature");
                    map.UniqueKey("UQ_LicenseTokenFeatures");
                    map.NotNullable(true);
                });

                ManyToOne(x => x.LicenseToken, map =>
                {
                    map.Column("LicenseTokenId");
                    map.ForeignKey("FK__LicenseTokenFeatures_LicenseToken");
                    map.UniqueKey("UQ_LicenseTokenFeatures");
                    map.NotNullable(true);
                });

                Property(p => p.Active);
            }
        }
    }
}

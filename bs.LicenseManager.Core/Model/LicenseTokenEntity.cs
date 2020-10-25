using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Standard.Licensing;
using System;
using System.Collections.Generic;

namespace bs.LicensesManager.Core.Model
{
    public class LicenseTokenEntity : IPersistentEntity
    {
        public LicenseTokenEntity()
        {
            LicenseTokenFeatures = new List<LicenseTokenFeatureEntity>();
        }
        public virtual Guid Id { get; set; }
        //public virtual ProductEntity Product { get; set; }
        public virtual ProductVersionEntity Version { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public virtual IList<LicenseTokenFeatureEntity> LicenseTokenFeatures { get; set; }

        public virtual string PrivateKey { get; set; }
        public virtual string PublicKey { get; set; }
        public virtual string PassPhrase { get; set; }
        public virtual string LicFileContent { get; set; }
        public virtual bool Active { get; set; }
        public virtual LicenseType LicenseType { get; set; }
        public virtual DateTime EmitDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }

        public override string ToString()
        {
            return $"Licese ID: {Id} (expire: {ExpireDate.ToString("dd-MM-yy")})";
        }

        public class Map : ClassMapping<LicenseTokenEntity>
        {
            public Map()
            {
                Table("LicenseTokens");

                Id(x => x.Id, x =>
                {
                    x.Generator(Generators.Guid);
                    x.Type(NHibernateUtil.Guid);
                    x.Column("Id");
                    x.UnsavedValue(Guid.Empty);
                });

                Property(p => p.LicFileContent, map =>
                {
                    map.Unique(true);
                    map.NotNullable(true);
                    map.Length(4096);
                    map.Type(NHibernateUtil.StringClob);
                });

                Bag(p => p.LicenseTokenFeatures, b =>
                {
                    b.Key(k => k.Column("LicenseTokenId"));
                },
                map => map.OneToMany(a => a.Class(typeof(LicenseTokenFeatureEntity))));

                Property(p => p.PrivateKey, map =>
                {
                    map.NotNullable(true);
                    map.Length(2048);
                });
                Property(p => p.PublicKey, map =>
                {
                    map.NotNullable(true);
                    map.Length(2048);
                });
                Property(p => p.PassPhrase, map =>
                {
                    map.NotNullable(true);
                    map.Length(256);
                });

                //ManyToOne(x => x.Product, map =>
                //{
                //    map.Column("ProductId");
                //    map.ForeignKey("FK__LicenseTokens_Product");
                //    map.NotNullable(true);
                //});

                Property(p => p.Active);

                ManyToOne(x => x.Version, map =>
                {
                    map.Column("VersionId");
                    map.ForeignKey("FK__LicenseTokens_Version");
                    map.NotNullable(true);
                });

                ManyToOne(x => x.Customer, map =>
                {
                    map.Column("CustomerId");
                    map.ForeignKey("FK__LicenseTokens_Customer");
                    map.NotNullable(true);
                });

                Property(p => p.LicenseType, map => map.NotNullable(true));
                Property(p => p.EmitDate, map => map.NotNullable(true));
                Property(p => p.ExpireDate, map => map.NotNullable(true));
            }
        }
    }
}

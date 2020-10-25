using bs.Data.Interfaces;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace bs.LicenseManager.Core.Model
{
    public class CustomerEntity : IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailContact { get; set; }
        public virtual bool Active { get; set; }
        public virtual IEnumerable<LicenseTokenEntity> LicenseTokens { get; set; }

        public class Map : ClassMapping<CustomerEntity>
        {
            public Map()
            {
                Table("Customers");

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

                Property(p => p.EmailContact, map => map.NotNullable(true));

                Property(p => p.Active);

                Bag(p => p.LicenseTokens, b =>
                {
                    b.Key(k => k.Column("CustomerId"));
                },
                map => map.OneToMany(a => a.Class(typeof(LicenseTokenEntity))));
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadanie_1_UP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Histori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Histori()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public Nullable<System.DateTime> dataZ { get; set; }
        public Nullable<System.DateTime> blok { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CybersportTournament
{
    using System;
    using System.Collections.Generic;
    
    public partial class Players
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Players()
        {
            this.PlayersList = new HashSet<PlayersList>();
        }
    
        public int ID { get; set; }
        public int IDPerson { get; set; }
        public string Nickname { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual Persons Persons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayersList> PlayersList { get; set; }
    }
}

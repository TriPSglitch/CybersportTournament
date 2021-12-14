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
    
    public partial class Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Match()
        {
            this.MatchList = new HashSet<MatchList>();
            this.RoundsList = new HashSet<RoundsList>();
        }
        public Match(DateTime date)
        {
            this.Time = date;
            this.MatchList = new HashSet<MatchList>();
            this.RoundsList = new HashSet<RoundsList>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime Time { get; set; }
        public Nullable<System.TimeSpan> Period { get; set; }
        public string Result { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchList> MatchList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoundsList> RoundsList { get; set; }
    }
}

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
    
    public partial class RoundsList
    {
        public int ID { get; set; }
        public int IDRound { get; set; }
        public int IDMatch { get; set; }
    
        public virtual Match Match { get; set; }
        public virtual Rounds Rounds { get; set; }
    }
}
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
    
    public partial class MapsGame
    {
        public MapsGame(int IDGame, string Name)
        {
            this.IDGame = IDGame;
            this.Name = Name;
        }

        public int ID { get; set; }
        public int IDGame { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    
        public virtual Games Games { get; set; }
    }
}

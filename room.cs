//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class room
    {
        public room()
        {
            this.reservations = new HashSet<reservation>();
            this.service_reservation = new HashSet<service_reservation>();
        }
    
        public int id { get; set; }
        public string number { get; set; }
        public decimal price { get; set; }
        public int hotel_id { get; set; }
        public int person { get; set; }
    
        public virtual hotel hotel { get; set; }
        public virtual ICollection<reservation> reservations { get; set; }
        public virtual ICollection<service_reservation> service_reservation { get; set; }
    }
}
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
    
    public partial class user
    {
        public user()
        {
            this.customers = new HashSet<customer>();
            this.employees = new HashSet<employee>();
        }
    
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Nullable<int> role { get; set; }
    
        public virtual ICollection<customer> customers { get; set; }
        public virtual ICollection<employee> employees { get; set; }
    }
}
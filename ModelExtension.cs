namespace Prakt17
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class DB_KPRAKT17Entities : DbContext
    {
        
        
        private static DB_KPRAKT17Entities context;
        
        public static DB_KPRAKT17Entities GetContext()
        {
            if (context == null) context = new DB_KPRAKT17Entities();
            return context;
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Utils
{
    public class Utils
    {

    }

    public static class IdGenerator
    {
        private static int id_ = 1000;

        public static int Seed { set { id_ = value; } }
        
        public static int UniqueId 
        { 
            get { return id_++; }
        }        
    }
}


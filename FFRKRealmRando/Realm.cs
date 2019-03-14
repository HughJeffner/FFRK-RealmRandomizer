using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKRealmRando
{
    class Realm
    {
        public string Name { get; set; }
        public Bitmap Image { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public Realm(String Name)
        {
            this.Name = Name;
            this.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(Name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Genres")]
    internal class Genre
    {
        private string _Name;

        [Key]
        public int GenreID { get; set; }

        public string Name
        {
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }


        //one to many direction (parent to child)
        public virtual ICollection<Track> Tracks { get; set; }
    }
}

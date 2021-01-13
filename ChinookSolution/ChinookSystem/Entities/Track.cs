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
    [Table("Tracks")]
    internal class Track
    {
        private string _Composer;

        public int TrackId { get; set; }

        [Required(ErrorMessage ="The Track requires a title.")]
        [StringLength(200, ErrorMessage ="The Track Name cannot be longer than 200 characters")]
        public string Name { get; set; }

        public int AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }
        public string Composer
        {
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal UnitPrice { get; set; }


        //one to many direction (parent to child)
        public virtual Album Tracks { get; set; }

    }
}

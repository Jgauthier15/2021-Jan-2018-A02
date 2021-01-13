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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;
        [Key]
        public int AlbumId { get; set; }

        //title is required
        [Required(ErrorMessage ="Album Title is required.")]
        [StringLength(160, ErrorMessage = "Album Title is limited to 160 characters.")]
        public string Title { get; set; }

        //while AristId is required, it is a FK and does not require the [Required] annotation field.
        //The default value for year/boolean have a value. Strings default to null. That's why you [Required] for strings and not ints.
        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Release Release Label is limited to 50 characters.")]

        //ReleaseLabel can be nullable, so let's fully implement it. (private string _ReleaseLabel )
        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //[NotMapped] annotations are also allowed


        //Navigational Properties
        //many to one direction (child to parent)
        public virtual Artist Artist { get; set; }

        //one to many direction (parent to child)
        public virtual ICollection<Track> Tracks { get; set; }

    }
}

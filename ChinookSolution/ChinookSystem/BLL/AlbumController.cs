using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;   //for Sql and are internal
using ChinookSystem.ViewModels; //for data class to transfer data from BLL to web app
using System.ComponentModel;    //for ODS wizard
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using(var context = new ChinookSystemContext())
            {
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetAlbumsForArtist(int artistid)
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    where x.ArtistId == artistid
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name,
                                                        ArtistId = x.ArtistId
                                                    };
                return results.ToList();
            }
        }

        //  Query to return all data of the Album table
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                    select new AlbumItem
                                                    {
                                                        AlbumId = x.AlbumId,
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistId=x.ArtistId,
                                                        ReleaseLabel = x.ReleaseLabel
                                                    };
                return results.ToList();
            }
        }


        //  Query to look up an Album record by pkey

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Albums_FindById(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                // (...).FirstOrDefault will return either
                //  a)  the first record matching the where condition
                //  b)  a null value
                var results = (from x in context.Albums
                              where x.AlbumId == albumid
                                                 select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ArtistId = x.ArtistId,
                                                     ReleaseLabel = x.ReleaseLabel
                                                 }).FirstOrDefault();
                return results;
            }
        }

        #endregion

        #region Add,Update and Delete CRUD

        //Add
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //Due to the fact that we have seperated the handling of our entities, from the data transfer between WebApp and Class library
                //  using the ViewModel classes, we MUST create an instance of the entity and move the data from the ViewModel class
                //  to the entity instance.
                Album addItem = new Album
                {
                    //Why no pkey set?
                    //pkey is an identity pkey, no value is needed.
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //Staging
                //Setup in Local Memory
                //At this point you will NOT have sent anything to the database.
                //     ****** therefore, you will NOT have your new pkey as yet. ******
                context.Albums.Add(addItem);

                //Commit to database
                //On this command, you 
                //  a) Execute entity validation annotation
                //  b) Send your local memory staging to the database for execution
                //After a successful execution, your entity instance will have the new pkey (Identity) value.
                context.SaveChanges();

                //at this point, your entity instance has the new pkey value
                return addItem.AlbumId;

            }
        }

        //Update
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //Due to the fact that we have seperated the handling of our entities, from the data transfer between WebApp and Class library
                //  using the ViewModel classes, we MUST create an instance of the entity and move the data from the ViewModel class
                //  to the entity instance.
                Album updateItem = new Album
                {
                    //for an update, you need to supply your pkey value
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                //Staging
                //Setup in Local Memory

                context.Entry(updateItem).State = System.Data.Entity.EntityState.Modified;

                //Commit to database
                //On this command, you 
                //  a) Execute entity validation annotation
                //  b) Send your local memory staging to the database for execution

                context.SaveChanges();

            }
        }

        //Delete

        //When we do an ODS CRID on the DELETE, the ODS sends in the entire instance record, not just the pkey value.

        //overload the Album_Delete method so it receives a whole instance then call the actual delete method passing just the pkey
        //  value to the actual delete method.

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Album_Delete(AlbumItem item)
        {
            Album_Delete(item.AlbumId);
        }
        public void Album_Delete(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                //Example of a Physical Delete
                //retrieve the current entity instance based on the incoming parameter
                var exists = context.Albums.Find(albumid);
                //stage the remove
                context.Albums.Remove(exists);
                //commit the remove
                context.SaveChanges();

                //a logical delete is actually an update of the instance
            }
        }
        #endregion
    }
}

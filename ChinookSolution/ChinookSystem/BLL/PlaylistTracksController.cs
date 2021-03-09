using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using FreeCode.Exceptions;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        //class level variable that will hold multiple string, each representing an error message.

        List<Exception> brokenRules = new List<Exception>();

        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {

                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname) &&
                                    x.Playlist.UserName.Equals(username)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };
                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid, string song)
        {

            Playlist playlistExists = null;
            PlaylistTrack playlisttrackExists = null;
            int tracknumber = 0;

            using (var context = new ChinookSystemContext())
            {
                //this class is in what is called the "Business Logic Layer"
                //Business Logic is the rules of your business.
                //Business Logic ensures that rules and data are what is expected.
                //Rules:
                //  Rule:A track can only exist once in a Playlist.
                //  Rule:Playlist names can only be used once for a user, different users may have the same playlist name.
                //  Rule:Each track on a playlist is assigned a continious track number
                //
                //The BLL method should also ensure that data exists for the processing of the transaction

                if (string.IsNullOrEmpty(playlistname))
                {
                    //there is a data error
                    //setting up an error message:
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is missing. Unable to add track.",
                        nameof(playlistname), playlistname));
                }
                if (string.IsNullOrEmpty(username))
                {
                    //there is a data error
                    //setting up an error message:
                    brokenRules.Add(new BusinessRuleException<string>("User name is missing. Unable to add track.",
                        nameof(username), username));
                }

                //does the playlist already exist?
                playlistExists = (from x in context.Playlists
                                  where x.Name.Equals(playlistname) &&
                                        x.UserName.Equals(username)
                                  select x).FirstOrDefault();

                if (playlistExists == null)
                {
                    //new playlist
                    ///Tasks:
                    //// Create a new instance of the playlist class
                    //// Load the instance with data
                    //// Stage the add of the new instnace
                    //// Set the tracknumber to 1

                    //Create and Load
                    playlistExists = new Playlist()
                    {
                        Name = playlistname,
                        UserName = username
                    };

                    //Stage
                    context.Playlists.Add(playlistExists);  //parent is STAGED

                    tracknumber = 1;
                }
                else
                {
                    //existing playlist
                    ///Tasks:
                    //// Does the track already exist on the playlist?  If yes, error.
                    //// If No, find the highest current tracknumber, then increment by 1.
                    playlisttrackExists = (from x in context.PlaylistTracks
                                           where x.Playlist.Name.Equals(playlistname) &&
                                                 x.Playlist.UserName.Equals(username) &&
                                                 x.TrackId == trackid
                                           select x).FirstOrDefault();
                    if (playlisttrackExists == null)
                    {
                        //track does not exist on the desired playlist.
                        tracknumber = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname) &&
                                                 x.Playlist.UserName.Equals(username)
                                       select x.TrackNumber).Max();

                        tracknumber++;
                    }
                    else
                    {
                        //business rule broken. track DOES exist already on the desired playlist.
                        brokenRules.Add(new BusinessRuleException<string>("Track already on playlist",
                                         nameof(song), song));
                    }
                }
                //add the ttrack to the playlist in PlaylistTracks
                //create an instance
                playlisttrackExists = new PlaylistTrack();
                //load the instance
                playlisttrackExists.TrackId = trackid;
                playlisttrackExists.TrackNumber = tracknumber;


                //add the isntance

                //???????????????
                //What is the playlist id?
                ///if the playlist exists then we know the id
                ///BUT if the playlist is NEW, we DO NOT know the id.

                //in one case the id is known BUT in the second case where the new record is ONLY STAGED, NO primary key
                //      value has been generated yet.  <--------------Problem.
                //if you access the new playlist record the pkey would be 0.
                //      (default numeric value)

                //the solution to BOTH of these scenarios is to use navigational properties
                //      during the ACTUAL  .Add command
                //      for the new playlisttrack record.
                //the entityframwork will, on your behalf, ensure that the adding of records to the 
                //  database will be done in the appropriate order AND will add the missing compound pkey
                //  value (PlaylistId) to the new playlisttrack record.

                //!!!!!!NOT LIKE THIS !!!!!!!   THIS IS  WRONG!!!!!
                //context.PlaylistTracks.Add(playlisttrackExists);

                //INSTEAD,  do the STAGING using the parent.navproperty.Add(xxxx);
                playlistExists.PlaylistTracks.Add(playlisttrackExists);

                //do the commit
                //check to see if ANY business rule exceptions occurred.
                //
                if (brokenRules.Count() > 0)
                {
                    //at least one error was recorded during the processing of the transaction
                    throw new BusinessRuleCollectionException("Add Playlist Track Concerns", brokenRules);
                }
                else
                {
                    //COMMIT THE TRANSACTION
                    //ALL the staged record will be sent to SQL for processing
                    //the transaction is complete
                    //NOTE: A transaction has ONE and ONLY ONE .SaveChanges()
                    context.SaveChanges();
                }


            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here


            }
        }//eom
    }
}

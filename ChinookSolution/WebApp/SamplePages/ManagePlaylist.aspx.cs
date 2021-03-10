using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        #region MessageUserControl Error Handling for ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        #endregion



        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Artist";

            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("You did not supply an Artist Name.");
                SearchArg.Value = "asdzxc";
            }
            else
            {
                //the HiddenField content access is .Value  NOT   .Text
                SearchArg.Value = ArtistName.Text;
            }
            //to force the re-execution of an ODS attached to a display control
            //      rebind the display control
            TracksSelectionList.DataBind();
        }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Genre";
            //if you had a prompt on the dropdownlist, you would verify that a selection
            //  was made.

            // //you could use the value field off the dropdownlist.
            //SearchArg.Value = GenreDDL.SelectedValue;

            // /Can i use something else from the dropdownlist instead of the value field?
            // // There is the display field.
            // / / / /WARNING: Using the display field for the local, in this example, is possible because
            //                  EACH description is unique!!!!!
            SearchArg.Value = GenreDDL.SelectedItem.Text;

            //to force the re-execution of an ODS attached to a display control
            //      rebind the display control
            TracksSelectionList.DataBind();
        }



        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Album";

            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("You did not supply an Albume Title.");
                SearchArg.Value = "asdzsxc";
            }
            else
            {
                //the HiddenField content access is .Value  NOT   .Text
                SearchArg.Value = AlbumTitle.Text;
            }
            //to force the re-execution of an ODS attached to a display control
            //      rebind the display control
            TracksSelectionList.DataBind();

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //username is coming from the system via security
            //since security has yet to be installed, a default will be setup for the
            //  username value
            string username = "HansenB";

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Playlist Search", "No playlist name was supplied.");
            }
            else
            {
                //use some user friendly error handling
                //the way we are doing the error handling is using MessageUserControl instead of
                //  try/catch.
                //MessageUserControl has the try/catch embedded within the control logic.
                //Within in the MessageUserControl, there exists a method called ".TryRun()"
                //syntax
                //  MessageUserControl.TryRun( () => {
                //
                //      your coding logic
                //
                //   }[,"Message Title","Success Message"]);    ( [] = optional  )
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    RefreshPlayList(sysmgr, username);
                }, "Playlist Search", "View the requested playlist below.");


            }

        }
        protected void RefreshPlayList(PlaylistTracksController sysmgr, string username)
        {

            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
            PlayList.DataSource = info;
            PlayList.DataBind();
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //form event validation: presence (do i have data?)
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a play list visible to choose tracks for movement. Select fromt he displayed playlist.");
                }
                else
                {
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //traverse the gridview control PlayList
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackID = int.Parse((trackSelection.FindControl("TrackId") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((trackSelection.FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    //was a single song selected?
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select one song to remove.");
                                break;
                            }
                        case 1:
                            {
                                //rule: do not move if last song
                                if (moveTrack.TrackNumber==PlayList.Rows.Count)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Song selected is already the last song. Moving down not necessary.");
                                }
                                else
                                {
                                    moveTrack.Direction = "down";
                                    MoveTrack(moveTrack);
                                }
                                
                                break;
                            }
                        default:
                            {
                                //more than 1
                                MessageUserControl.ShowInfo("Track Movement", "You must select only one song to remove.");
                                break;
                            }
                    }
                }
            }

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a play list visible to choose tracks for movement. Select fromt he displayed playlist.");
                }
                else
                {
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //traverse the gridview control PlayList
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackID = int.Parse((trackSelection.FindControl("TrackId") as Label).Text);
                            moveTrack.TrackNumber = int.Parse((trackSelection.FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    //was a single song selected?
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select one song to remove.");
                                break;
                            }
                        case 1:
                            {
                                //rule: do not move if last song
                                if (moveTrack.TrackNumber == 1)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Song selected is already the first song. Moving up not necessary.");
                                }
                                else
                                {
                                    moveTrack.Direction = "up";
                                    MoveTrack(moveTrack);
                                }

                                break;
                            }
                        default:
                            {
                                //more than 1
                                MessageUserControl.ShowInfo("Track Movement", "You must select only one song to move.");
                                break;
                            }
                    }
                }
            }

        }

        protected void MoveTrack(MoveTrackItem movetrack)
        {
            string username = "HansenB";  //until security is implemented
            movetrack.UserName = username;
            movetrack.PlaylistName = PlaylistName.Text;

            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(movetrack);
                RefreshPlayList(sysmgr,username);

            }, "Track Movement", "Track has been moved.");

        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            string username = "HansenB";  //until security is implemented

            //form event validation: presence (do i have data?)
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Removal", "You must have a play list visible to choose removals. Select fromt he displayed playlist.");
                }
                else
                {
                    //collect the tracks indicated on the playlist for removal
                    List<int> trackids = new List<int>();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //traverse the gridview control PlayList
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            trackids.Add(int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text));
                        }
                    }

                    //was a song selected?
                    if (rowsSelected == 0)
                    {
                        MessageUserControl.ShowInfo("Missing Data", "You must select at least one song to remove.");
                    }
                    else
                    {
                        //data collected, send for processing
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks(username, PlaylistName.Text, trackids);
                            RefreshPlayList(sysmgr, username);
                        }, "Track removal", "Selected track(s) have been removed from the playlist");
                    }
                }
            }

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";  //until security is implemented

            //form event validation: presence (do i have data?)
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                //Reminder: MessageUserControl will do the error handling
                MessageUserControl.TryRun(() =>
                {
                    //logic for your coding block
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //access a specific field on the selected ListView row.
                    string song = (e.Item.FindControl("NameLabel") as Label).Text;

                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()), song);
                    RefreshPlayList(sysmgr, username);


                }, "Add Track to Playlist", "Track has been added to the playlist.");
            }
        }
    }
}
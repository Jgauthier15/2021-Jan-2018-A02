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
                MessageUserControl.TryRun(() => {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    RefreshPlayList(sysmgr, username);
                },"Playlist Search","View the requested playlist below.");


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
            //code to go here

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track

        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";  //until security is implemented

            //form event validation: presence (do i have data?)
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                //Reminder: MessageUserControl will do the error handling
                MessageUserControl.TryRun(() => {
                    //logic for your coding block
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //access a specific field on the selected ListView row.
                    string song = (e.Item.FindControl("NameLabel") as Label).Text;

                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()),song);


                },"Add Track to Playlist","Track has been added to the playlist.");
            }

        }

    }
}
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

            //code to go here

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
            //code to go here

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
            //code to go here

        }

    }
}
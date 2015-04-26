using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    ShowTrackerEntities showentities = new ShowTrackerEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var arts = from a in showentities.Artists
                       orderby a.ArtistName
                       select new { a.ArtistName, a.ArtistKey };

            DropDownList1.DataSource = arts.ToList();
            DropDownList1.DataTextField = "ArtistName";
            DropDownList1.DataValueField = "ArtistKey";
            DropDownList1.DataBind();

        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var shs = from sd in showentities.ShowDetails
                  join a in showentities.Artists on sd.ArtistKey equals a.ArtistKey
                  join s in showentities.Shows on sd.ShowKey equals s.ShowKey
                  where a.ArtistName == DropDownList1.SelectedItem.Text
                  select new { s.ShowName, a.ArtistName, sd.ShowDetailArtistStartTime };
        GridView1.DataSource = shs.ToList();
        GridView1.DataBind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
}
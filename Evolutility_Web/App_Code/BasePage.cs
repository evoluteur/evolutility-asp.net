/* ********************************************** 
 * Page directive extender - base page class    *
 *  by Jim Azar - http://www.rhinoback.com      *
 * **********************************************/

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;  


/// <SUMMARY>
/// Base class with properties for meta tags for content pages 
/// </SUMMARY>
public class BasePage : Page
    {
    private string _keywords;
    private string _description;
    private string _menus;
    private int  _submenuID;
    // Constructor
    // Add an event handler to Init event for the control
    // so we can execute code when a server control (page)
    // that inherits from this base class is initialized.
    public BasePage()
        {
        Init += new EventHandler(BasePage_Init);
        }

    // Whenever a page that uses this base class is initialized
    // add meta keywords and descriptions if available
    void BasePage_Init(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(_keywords))
            {
            HtmlMeta tag = new HtmlMeta();
            tag.Name = "keywords";
            tag.Content = _keywords;
            Header.Controls.Add(tag);
            }

            if (!String.IsNullOrEmpty(_description))
            {
            HtmlMeta tag = new HtmlMeta();
            tag.Name = "description";
            tag.Content = _description;
            Header.Controls.Add(tag);
            }

            if (!String.IsNullOrEmpty(_menus))
            {  
			  Evolutility.SideBar.SideBar t = (Evolutility.SideBar.SideBar)Master.FindControl("EvoMenu1");
                //EvolutilityToolbar.EvoHeader t = (EvolutilityToolbar.EvoHeader)Master.FindControl("EvoHead1");
                if (t != null)
                {
                    t.M1 = _menus;
                    t.MID = _submenuID;
                } 
            }
        }

    /// <SUMMARY>
    /// Gets or sets the Meta Keywords tag for the page
    /// </SUMMARY>
    public string Meta_Keywords
        {
        get 
            { 
            return _keywords; 
            }
        set 
            { 
            // strip out any excessive white-space, newlines and linefeeds
            _keywords = Regex.Replace(value, "\\s+", " "); 
            }
        }

    /// <SUMMARY>
    /// Gets or sets the Meta Description tag for the page
    /// </SUMMARY>
    public string Meta_Description
        {
        get 
            { 
            return _description; 
            }
        set 
            {
            // strip out any excessive white-space, newlines and linefeeds
            _description = Regex.Replace(value, "\\s+", " "); 
            }
        } 

    public string Menus
        {
        get 
            { 
            return _menus; 
            }
        set 
            {
                _menus = value;
            }
        }

    public int SubMenuID 
        {
        get 
            {
                return _submenuID; 
            }
        set 
            {
                _submenuID = value;
            }
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEd.BLL;
using HigherEd.ViewModels;
using HigherEd.ViewModels.Commands;
using Humanizer.Inflections;
using Newtonsoft.Json;

namespace WebApp.Admin
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SchoolProgramDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Call BLL to get the total credits for the school program.
        }

        // TODO: Clean the commented code out - won't work
        //List<CourseReference> ExtractPrerequisitesFromHiddenField()
        //{
        //    var result = JsonConvert.DeserializeObject<List<CourseReference>>(ListViewInsertPrerequisites.Value); // as List<CourseReference>;
        //    if (result == null)
        //        result = new List<CourseReference>();
        //    return result;
        //}
        //void PreservePrerequisitesInHiddenField(List<CourseReference> existingCourses)
        //{
        //    ListViewInsertPrerequisites.Value = JsonConvert.SerializeObject(existingCourses);
        //}

        protected void CoursesListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var controller = new CourseCatalogController();

            switch (e.CommandName)
            {
                case "AddPrerequisite":
                    #region AddPrerequisite
                    // Take the selected item from the DropDown: AvailableCourses
                    var dropDown = e.Item.FindControl("AvailableCourses") as DropDownList;
                    var newItem = dropDown.SelectedItem;
                    var newCourse = new CourseReference { Number = newItem.Text, CourseId = int.Parse(newItem.Value) };

                    // add it to the list of prerequisites in the Repeater: PrerequisitesRepeater
                    Repeater repeater;
                    List<CourseReference> existingCourses;
                    // TODO: Clean the commented code out - won't work
                    //if (e.CommandArgument.ToString() == "InsertTemplate")
                    //{
                    //    // Reaching outside of what's known in this method to grab the ListView
                    //    repeater = CoursesListView.InsertItem.FindControl("PrerequisitesRepeater") as Repeater;
                    //    existingCourses = ExtractPrerequisitesFromHiddenField();
                    //}
                    //else
                    {
                        repeater = e.Item.FindControl("PrerequisitesRepeater") as Repeater;
                        existingCourses = ExtractPrerequisites(repeater.Items);
                    }
                    // Only add if it does not already exist
                    if(existingCourses.SingleOrDefault(x=>x.CourseId == newCourse.CourseId) == null)
                    {
                        existingCourses.Add(newCourse);
                    }
                        repeater.DataSource = existingCourses.OrderBy(x => x.Number);
                        repeater.DataBind();
                    // TODO: Clean the commented code out - won't work
                    //if (e.CommandArgument.ToString() == "InsertTemplate")
                    //{
                    //    PreservePrerequisitesInHiddenField(existingCourses);
                    //}
                    e.Handled = true;
                    #endregion
                    break;
                case "Insert":
                    #region Insert
                    // TODO: Call BLL to create a new Course
                    // TODO: Try/Catch or MessageUserControl.TryRun()
                    var proposed = new ProposedCourse
                    {
                        Number = (e.Item.FindControl("NumberTextBox") as TextBox)?.Text,
                        Name = (e.Item.FindControl("NameTextBox") as TextBox)?.Text,
                        Hours = byte.Parse((e.Item.FindControl("HoursDropDown") as DropDownList).SelectedValue),
                        Credits = decimal.Parse((e.Item.FindControl("CreditsDropDown") as DropDownList).SelectedValue),
                        Term = byte.Parse((e.Item.FindControl("TermDropDown") as DropDownList).SelectedValue)
                    };
                    controller.AddCourse(int.Parse(SchoolProgramDropDown.SelectedValue), proposed);
                    e.Handled = true;
                    #endregion
                    break;
                case "Update":
                    #region Update
                    // Try/Catch or MessageUserControl.TryRun()
                    MessageUserControl.TryRun(() => 
                    {
                        var existing = new CourseSpecification
                        {
                            Number = (e.Item.FindControl("NumberTextBox") as TextBox)?.Text,
                            Name = (e.Item.FindControl("NameTextBox") as TextBox)?.Text,
                            Hours = byte.Parse((e.Item.FindControl("HoursDropDown") as DropDownList).SelectedValue),
                            Credits = decimal.Parse((e.Item.FindControl("CreditsDropDown") as DropDownList).SelectedValue),
                            Term = byte.Parse((e.Item.FindControl("TermDropDown") as DropDownList).SelectedValue),
                            IsElective = (e.Item.FindControl("IsElectiveCheckBox") as CheckBox).Checked,
                            CourseId = int.Parse((e.Item.FindControl("CourseIdHiddenField") as HiddenField).Value)
                        };
                        var existingRepeater = e.Item.FindControl("PrerequisitesRepeater") as Repeater;
                        var existingPrerequisites = ExtractPrerequisites(existingRepeater.Items);
                        var ids = new List<int>();
                        foreach (var course in existingPrerequisites)
                            ids.Add(course.CourseId);
                        existing.Prerequisites = ids;
                        // Call BLL to update existing Course
                        controller.UpdateCourse(int.Parse(SchoolProgramDropDown.SelectedValue), existing);
                    }, "Course Updated", "Your course details have been successfully updated");
                    //  Success Title  ,  Success Message
                    e.Handled = true;
                    #endregion
                    break;
            }
        }

        protected void PrerequisitesRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "RemovePrerequisite")
            {
                var repeater = source as Repeater;
                if(repeater != null)
                {
                    List<CourseReference> courses;
                    // TODO: Clean the commented code out - won't work
                    //if(e.CommandArgument.ToString() == "InsertTemplate")
                    //    courses = ExtractPrerequisitesFromHiddenField();
                    //else
                        courses = ExtractPrerequisites(repeater.Items);

                    // Remove the one item in the link button's CommandArgument
                    var hidden = e.Item.FindControl("CourseId") as HiddenField;
                    var id = int.Parse(hidden.Value);
                    if(courses.Remove(courses.SingleOrDefault(x => x.CourseId == id)))
                    {
                        repeater.DataSource = courses;
                        repeater.DataBind();
                        // TODO: Clean the commented code out - won't work
                        //if (e.CommandArgument.ToString() == "InsertTemplate")
                        //    PreservePrerequisitesInHiddenField(courses);
                    }
                }
            }
        }
        List<CourseReference> ExtractPrerequisites(RepeaterItemCollection items)
        {
            var result = new List<CourseReference>();
            foreach(RepeaterItem item in items)
            {
                var label = item.FindControl("CourseNumber") as Label;
                var hidden = item.FindControl("CourseId") as HiddenField;
                result.Add(new CourseReference { Number = label.Text, CourseId = int.Parse(hidden.Value) });
            }
            return result;
        }


        protected void CoursesListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(e.Item.DataItemIndex == CoursesListView.EditIndex)
            {
                // Programmatically bind the data for the nested repeater.
                var repeater = e.Item.FindControl("PrerequisitesRepeater") as Repeater;
                if(repeater != null)
                {
                    repeater.DataSource = (e.Item.DataItem as SchoolCourse)?.Prerequisites;
                    repeater.DataBind();
                }
            }
        }
    }
}
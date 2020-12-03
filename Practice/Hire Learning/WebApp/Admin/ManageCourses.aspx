<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="WebApp.Admin.ManageCourses" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Course Catalog</h1>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="Label1" runat="server" AssociatedControlID="SchoolProgramDropDown">School Program</asp:Label>
            <asp:DropDownList ID="SchoolProgramDropDown" runat="server"
                AppendDataBoundItems="true" AutoPostBack="true"
                OnSelectedIndexChanged="SchoolProgramDropDown_SelectedIndexChanged" DataSourceID="SchoolProgramDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                <asp:ListItem Value="0">[Select a Program of Study]</asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="SchoolProgramDataSource" runat="server"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="ListSchoolPrograms" TypeName="HigherEd.BLL.CourseCatalogController"></asp:ObjectDataSource>

            <asp:Label ID="Label2" runat="server" AssociatedControlID="ProgramCredits">Program Credit Total</asp:Label>
            <asp:TextBox ID="ProgramCredits" runat="server" Enabled="false" />

            <div>
                <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            </div>

            <asp:HiddenField ID="ListViewInsertPrerequisites" runat="server" />
            <asp:ListView ID="CoursesListView" runat="server"
                DataSourceID="CoursesDataSource" DataKeyNames="CourseId"
                InsertItemPosition="FirstItem"
                OnItemCommand="CoursesListView_ItemCommand"
                OnItemDataBound="CoursesListView_ItemDataBound"
                ItemType="HigherEd.ViewModels.SchoolCourse">
                <EditItemTemplate>
                    <tr class="bg-secondary text-white">
                        <td>
                            <asp:HiddenField Value='<%# Bind("CourseId") %>' runat="server" ID="CourseIdHiddenField" />
                            <asp:TextBox Text='<%# Bind("Number") %>' runat="server" ID="NumberTextBox" Enabled="false" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" />
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Hours") %>' runat="server" ID="HoursDropDown" ToolTip="Course Hours" DataSourceID="HoursDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value=""><i>Hr</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Credits") %>' runat="server" ID="CreditsDropDown" ToolTip="Credits" DataSourceID="CreditsDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value=""><i>Cr</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Term") %>' runat="server" ID="TermDropDown" ToolTip="Term" DataSourceID="TermDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value=""><i>#</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr class="bg-secondary text-white">
                        <td>Pre-requisites</td>
                        <td>
                            <asp:DropDownList ID="AvailableCourses" runat="server" AppendDataBoundItems="true" DataSourceID="PreRequisiteCoursesDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value="0">[Select a Course]</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="AddPrerequisite" runat="server" CommandName="AddPrerequisite" CssClass="btn btn-info btn-sm">Add</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:CheckBox Checked='<%# Bind("IsElective") %>' runat="server" ID="IsElectiveCheckBox" Text="Elective" /></td>
                        <td colspan="4" style="text-align: right;">
                            <asp:LinkButton runat="server" CommandName="Update" Text="Update Course" ID="UpdateButton" CssClass="btn btn-primary btn-sm" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" CssClass="btn btn-secondary btn-sm" />
                        </td>
                    </tr>
                    <tr class="bg-secondary text-white">
                        <td></td>
                        <td colspan="5">
                            <asp:Repeater ID="PrerequisitesRepeater" runat="server"
                                ItemType="HigherEd.ViewModels.CourseReference" OnItemCommand="PrerequisitesRepeater_ItemCommand">
                                <ItemTemplate>
                                    <span class="bg-warning">
                                        <asp:Label ID="CourseNumber" runat="server" Text="<%# Item.Number %>" />
                                        <asp:HiddenField ID="CourseId" runat="server" Value="<%# Item.CourseId %>" />
                                        <asp:LinkButton ID="RemovePrerequisite" runat="server" CssClass="badge"
                                            CommandName="RemovePrerequisite" CommandArgument="<%# Item.CourseId %>">
                                            <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-x-circle-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                              <path fill-rule="evenodd" d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.354 4.646a.5.5 0 1 0-.708.708L7.293 8l-2.647 2.646a.5.5 0 0 0 .708.708L8 8.707l2.646 2.647a.5.5 0 0 0 .708-.708L8.707 8l2.647-2.646a.5.5 0 0 0-.708-.708L8 7.293 5.354 4.646z"/>
                                            </svg>
                                        </asp:LinkButton>
                                    </span>
                                </ItemTemplate>
                                <SeparatorTemplate>&nbsp;&nbsp;</SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <tr class="bg-info text-white">
                        <td>
                            <asp:TextBox Text='<%# Bind("Number") %>' runat="server" ID="NumberTextBox" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" />
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Hours") %>' runat="server" ID="HoursDropDown" ToolTip="Course Hours" DataSourceID="HoursDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value="0"><i>Hr</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Credits") %>' runat="server" ID="CreditsDropDown" ToolTip="Credits" DataSourceID="CreditsDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value="0"><i>Cr</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList SelectedValue='<%# Bind("Term") %>' runat="server" ID="TermDropDown" ToolTip="Term" DataSourceID="TermDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value=""><i>#</i></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="4" style="text-align: right;">
                            <asp:LinkButton runat="server" CommandName="Insert" Text="Create Course" ID="LinkButton1" CssClass="btn btn-success btn-sm" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="LinkButton2" CssClass="btn btn-secondary btn-sm" />
                        </td>
                    </tr>
                    <%--<tr class="bg-info text-white">
                        <td>Pre-requisites</td>
                        <td>
                            <asp:DropDownList ID="AvailableCourses" runat="server" AppendDataBoundItems="true" DataSourceID="PreRequisiteCoursesDataSource" DataTextField="DisplayText" DataValueField="DisplayValue">
                                <asp:ListItem Value="0">[Select a Course]</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="AddPrerequisite" runat="server" CommandName="AddPrerequisite" CommandArgument="InsertTemplate" CssClass="btn btn-primary btn-sm">Add</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:CheckBox Checked='<%# Bind("IsElective") %>' runat="server" ID="IsElectiveCheckBox" Text="Elective" /></td>
                        <td colspan="4" style="text-align: right;">
                            <asp:LinkButton runat="server" CommandName="Insert" Text="Create Course" ID="UpdateButton" CssClass="btn btn-success btn-sm" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" CssClass="btn btn-secondary btn-sm" />
                        </td>
                    </tr>--%>
                    <%--<tr class="bg-secondary text-white">
                        <td></td>
                        <td colspan="5">
                            <asp:Repeater ID="PrerequisitesRepeater" runat="server"
                                ItemType="HigherEd.ViewModels.CourseReference" OnItemCommand="PrerequisitesRepeater_ItemCommand">
                                <ItemTemplate>
                                    <span class="bg-warning">
                                        <asp:Label ID="CourseNumber" runat="server" Text="<%# Item.Number %>" />
                                        <asp:HiddenField ID="CourseId" runat="server" Value="<%# Item.CourseId %>" />
                                        <asp:LinkButton ID="RemovePrerequisite" runat="server" CssClass="badge"
                                            CommandName="RemovePrerequisite" CommandArgument="InsertTemplate">
                                            <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-x-circle-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                              <path fill-rule="evenodd" d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM5.354 4.646a.5.5 0 1 0-.708.708L7.293 8l-2.647 2.646a.5.5 0 0 0 .708.708L8 8.707l2.646 2.647a.5.5 0 0 0 .708-.708L8.707 8l2.647-2.646a.5.5 0 0 0-.708-.708L8 7.293 5.354 4.646z"/>
                                            </svg>
                                        </asp:LinkButton>
                                    </span>
                                </ItemTemplate>
                                <SeparatorTemplate>&nbsp;&nbsp;</SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>--%>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("Number") %>' runat="server" ID="NumberLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Hours") %>' runat="server" ID="HoursLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Credits") %>' runat="server" ID="CreditsLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("TermName") %>' runat="server" ID="TermLabel" /></td>
                        <td>
                            <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" CssClass="btn btn-secondary btn-sm">Edit</asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" class="table table-hover">
                                    <tr runat="server" style="">
                                        <th runat="server">Number</th>
                                        <th runat="server">Name</th>
                                        <th runat="server">Hours</th>
                                        <th runat="server">Credits</th>
                                        <th runat="server">Term</th>
                                        <th runat="server">Action</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="CoursesDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListSchoolCourses" TypeName="HigherEd.BLL.CourseCatalogController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SchoolProgramDropDown" PropertyName="SelectedValue" Name="programId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="HoursDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCourseHours" TypeName="HigherEd.BLL.CourseCatalogController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SchoolProgramDropDown" PropertyName="SelectedValue" Name="programId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="CreditsDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCourseCredits" TypeName="HigherEd.BLL.CourseCatalogController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SchoolProgramDropDown" PropertyName="SelectedValue" Name="programId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="TermDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListTerms" TypeName="HigherEd.BLL.CourseCatalogController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SchoolProgramDropDown" PropertyName="SelectedValue" Name="programId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="PreRequisiteCoursesDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListSchoolCourseSummaries" TypeName="HigherEd.BLL.CourseCatalogController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SchoolProgramDropDown" PropertyName="SelectedValue" Name="programId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

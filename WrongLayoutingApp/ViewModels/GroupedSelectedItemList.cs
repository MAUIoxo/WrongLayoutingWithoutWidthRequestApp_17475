using WrongLayoutingApp.DatabaseModels;

namespace WrongLayoutingApp.ViewModels
{
    public partial class GroupedSelectedItemList : List<SelectedItem>
    {
        public string GroupName { get; set; }

        public GroupedSelectedItemList(string groupName, List<SelectedItem> item) : base(item)
        {
            GroupName = groupName;
        }
    }
}

using WrongLayoutingApp.DatabaseModels;

namespace WrongLayoutingApp.ViewModels
{
    public partial class GroupedItemList : List<Item>
    {
        public string GroupName { get; set; }

        public GroupedItemList(string groupName, List<Item> item) : base(item)
        {
            GroupName = groupName;
        }
    }
}

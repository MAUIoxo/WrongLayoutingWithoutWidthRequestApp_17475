using WrongLayoutingApp.DatabaseModels;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace WrongLayoutingApp.ViewModels
{
    public partial class View1ViewModel : ViewModelBase
    {
        public ObservableRangeCollection<SelectedItem> AvailableItems { get; set; }
        public ObservableRangeCollection<SelectedItem> SelectedItems { get; set; }

        public AsyncCommand RefreshDatabaseCommand { get; }
        


        public View1ViewModel()
        {
            AvailableItems = new ObservableRangeCollection<SelectedItem>();
            SelectedItems = new ObservableRangeCollection<SelectedItem>();
                        
            RefreshDatabaseCommand = new AsyncCommand(RefreshDatabaseCommandHandler);
            RefreshDatabaseCommand.ExecuteAsync();
        }

        

        #region Command Handler
              

        private async Task RefreshDatabaseCommandHandler()
        {
            IsBusy = true;

            List<Item> items = SetupItemsList();

            if (items.Any() && AvailableItems.Count == 0)
            {
                AvailableItems.AddRange(items.Select(item => new SelectedItem() { Item = item }));
                AvailableItems[0].IsSelected = true;
            }

            GroupItemsWithSearchFilter();       // Start with empty Search-Filter (=> get all Items)
            await FilterSelectedItems();


            IsBusy = false;

            await Task.CompletedTask;
        }

        private static List<Item> SetupItemsList()
        {
            List<Item> itemList = new List<Item>();

            Item item = new Item();
            item.Name = "A";
            item.A = 545;
            item.C = 454;
            item.B = 131;
            item.Brand = "B";
            item.Id = 0;
            item.ItemSelections = new();

            itemList.Add(item);

            return itemList;
        }

        #endregion


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;

                    OnPropertyChanged(nameof(SearchText));
                    GroupItemsWithSearchFilter();
                }
            }
        }

        #region Grouping of Items

        private ObservableRangeCollection<GroupedSelectedItemList> _groupedSelectedItems { get; set; }
        public ObservableRangeCollection<GroupedSelectedItemList> GroupedSelectedItems
        {
            get => _groupedSelectedItems != null ? _groupedSelectedItems : _groupedSelectedItems = new ObservableRangeCollection<GroupedSelectedItemList>();
            set
            {
                if (_groupedSelectedItems != value)
                {
                    _groupedSelectedItems = value;

                    OnPropertyChanged(nameof(GroupedSelectedItems));
                }
            }
        }

        private void GroupItemsWithSearchFilter()
        {
            IOrderedEnumerable<KeyValuePair<string, List<SelectedItem>>> groupedItemsDictionary;
            if (string.IsNullOrEmpty(SearchText))
            {
                groupedItemsDictionary = AvailableItems
                    .Where(selectedItem => selectedItem.Item != null && selectedItem.Item.Name != null)
                    .OrderBy(selectedItem => selectedItem.Item.Name)
                    .GroupBy(selectedItem => selectedItem.Item.Name.ToUpperInvariant().Substring(0, 1))
                    .ToDictionary(group => group.Key, group => group.ToList()).OrderBy(group => group.Key);
            }
            else
            {
                groupedItemsDictionary = AvailableItems
                    .Where(selectedItem => selectedItem.Item != null && selectedItem.Item.Name != null && selectedItem.Item.Name.ToLower().StartsWith(SearchText.ToLower()))
                    .OrderBy(selectedItem => selectedItem.Item.Name)
                    .GroupBy(selectedItem => selectedItem.Item.Name.ToUpperInvariant().Substring(0, 1))
                    .ToDictionary(group => group.Key, group => group.ToList()).OrderBy(group => group.Key);
            }

            _groupedSelectedItems = new ObservableRangeCollection<GroupedSelectedItemList>();

            foreach (KeyValuePair<string, List<SelectedItem>> item in groupedItemsDictionary)
            {
                _groupedSelectedItems.Add(new GroupedSelectedItemList(item.Key, new List<SelectedItem>(item.Value)));
            }

            OnPropertyChanged(nameof(GroupedSelectedItems));
        }

        #endregion

        private async Task FilterSelectedItems()
        {
            var selectedItems = GroupedSelectedItems.SelectMany(groupedSelectedItem => groupedSelectedItem).Where(item => item.IsSelected == true).ToList();

            SelectedItems.Clear();
            SelectedItems.AddRange(selectedItems);

            OnPropertyChanged(nameof(SelectedItems));

            await Task.CompletedTask;
        }

        
        private string _selectedItemName;
        public string SelectedItemName
        {
            get => _selectedItemName;
            set
            {
                if (value != _selectedItemName)
                {
                    _selectedItemName = value;
                    OnPropertyChanged(nameof(SelectedItemName));
                }
            }
        }      
    }
}

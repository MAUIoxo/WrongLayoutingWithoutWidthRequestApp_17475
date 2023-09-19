namespace WrongLayoutingApp.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        
        private byte _selectedViewModelIndex;
        public byte SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set
            {
                _selectedViewModelIndex = value;
            }
        }


        public MainPageViewModel()
        {
            SelectedViewModelIndex = 0;
        }
    }
}

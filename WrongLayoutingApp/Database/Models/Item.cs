namespace WrongLayoutingApp.DatabaseModels
{
    public class Item
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Brand { get; set; }


        public double A { get; set; }

        public double B { get; set; }

        public double C { get; set; }


        public List<SelectedItem> ItemSelections { get; set; }
    }
}

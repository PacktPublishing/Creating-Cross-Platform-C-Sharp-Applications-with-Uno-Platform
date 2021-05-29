namespace Dashboard.Views
{
    public class PersonCount
    {
        public PersonCount(string hour, double child, double adult, double senior)
        {
            Hour = hour;
            Children = child;
            Adults = adult;
            Seniors = senior;
        }

        public string Hour { get; set; }
        public double Children { get; set; }
        public double Adults { get; set; }
        public double Seniors { get; set; }
    }
}

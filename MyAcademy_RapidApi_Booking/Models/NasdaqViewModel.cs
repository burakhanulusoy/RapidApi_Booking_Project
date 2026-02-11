namespace MyAcademy_RapidApi_Booking.Models
{
    public class NasdaqViewModel
    {

            public Class1[] Property1 { get; set; }
        

        public class Class1
        {
            public string symbol { get; set; }
            public string name { get; set; }
            public float change { get; set; }
            public float price { get; set; }
            public float changesPercentage { get; set; }
        }

    }
}

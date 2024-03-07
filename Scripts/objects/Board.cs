namespace GameLibrary.Objects
{
    public class Board
    {
        public Card BuffRow1 { get; set; }
        public Card BuffRow2 { get; set; }
        public Card BuffRow3 { get; set; }
        public Card FieldRow1 { get; set; }
        public Card FieldRow1_1 { get; set; }
        public Card FieldRow2 { get; set; }
        public Card FieldRow2_1 { get; set; }
        public Card FieldRow3 { get; set; }
        public Card FieldRow3_1 { get; set; }
        public Card Leader { get; set; }
        public List<Card> Cementery { get; set; }
        public List<Card> Deck { get; set; }
        public Card[] Melee = new Card[10];
        public Card[] Distance = new Card[10];
        public Card[] Asedio = new Card[10];
    }
}

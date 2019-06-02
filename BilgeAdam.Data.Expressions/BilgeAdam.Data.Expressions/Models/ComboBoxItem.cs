namespace BilgeAdam.Data.Expressions.Models
{
    class ComboBoxItem<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

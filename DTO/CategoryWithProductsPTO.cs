namespace task1.DTO
{
    public class CategoryWithProductsPTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> productsNames  { get; set; }   = new List<string>();
    }
}

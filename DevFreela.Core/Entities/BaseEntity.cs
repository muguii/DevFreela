namespace DevFreela.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }

        protected BaseEntity()
        {
            // Para suportar o Entity Framework Core
        }
    }
}
